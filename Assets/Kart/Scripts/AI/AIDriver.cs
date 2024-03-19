using System;
using kart.Kart.Scripts.Controls;
using kart.Kart.Scripts.System;
using kart.Kart.Scripts.Utils.Extensions;
using kart.Kart.Scripts.Utils.Timers;
using UnityEngine;

namespace kart.Kart.Scripts.AI
{
    public class AIDriver : MonoBehaviour, IDrive
    {
        // IDrive properties
        public Vector2 Move { get; private set; }
        public bool IsBreaking { get; private set; }
        public void Enable()
        {
            
        }

        // Data Containers
        private WayPoints _wayPoints;
        private AIDriverData _driverData;
        
        // Timer
        private CountdownTimer _driftTimer;
        
        // Physics
        // Kart yaw from previous frame
        private float _previousYaw;
        
        // Indexes
        private int _currentWaypointIndex;
        private int _currentCornerIndex;
        
        
        // Data setters for AI Kart builder
        public void SetDriverData(AIDriverData driverData) => _driverData = driverData;
        public void SetWayPoints(WayPoints wayPoints) => _wayPoints = wayPoints;

        #region Initialization

        private void Start()
        {
            if (!HasValidData())
            {
                throw new ArgumentNullException(
                    $"AI driver: {GetInstanceID()} does not have waypoitns and AI dirver data.");
            }
            
            InitPhysics();
            InitCountdownTimer();
        }

        private bool HasValidData()
        {
            return _wayPoints is not null && _driverData is not null;
        }

        private void InitPhysics()
        {
            _previousYaw = transform.eulerAngles.y;
        }

        private void InitCountdownTimer()
        {
            _driftTimer = new CountdownTimer(_driverData.driftTime);
            _driftTimer.OnTimerStart += OnStartBreaking;
            _driftTimer.OnTimerStop += OnStopBreaking;
        }

        #endregion

        #region CleanUp
        private void OnDisable()
        {
            _driftTimer.OnTimerStart -= OnStartBreaking;
            _driftTimer.OnTimerStop -= OnStopBreaking;
        }

        private void OnStartBreaking()
        {
            IsBreaking = true;
        }

        private void OnStopBreaking()
        {
            IsBreaking = false;
        }
        #endregion

        #region PhysicsUpdates

        private void FixedUpdate()
        {
            _driftTimer.Tick(Time.deltaTime);
            if (_wayPoints.checkPoints.Length == 0) return;

            var angularVelocity = UpdateYaw();
            
            var nextWaypoint = UpdateWaypoint();
            
            UpdateCorner();
            
            UpdateMove(nextWaypoint);
            
            CounterSteer(angularVelocity);
        }

        private float UpdateYaw()
        {
            var currentYaw = transform.eulerAngles.y;
            var deltaYaw = Mathf.DeltaAngle(_previousYaw, currentYaw);
            var angularVelocity = deltaYaw / Time.deltaTime;
            _previousYaw = currentYaw;
            return angularVelocity;
        }

        private Vector3 UpdateWaypoint()
        {
            var nextWaypoint = _wayPoints.checkPoints[_currentWaypointIndex].position - transform.position;
            
            // Update to the next waypoint if current one is in range
            if (nextWaypoint.magnitude < _driverData.wayPointRange)
            {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _wayPoints.checkPoints.Length;
            }

            return nextWaypoint;
        }
        private void UpdateCorner()
        {
            var nextCorner = _wayPoints.checkPoints[_currentCornerIndex].position - transform.position;
            
            // Update to the next corner, also assign the corner to the next
            // _current index has already been updated from UpdateWaypoint() if condition is met
            if (nextCorner.magnitude < _driverData.cornerRange)
            {
                _currentCornerIndex = _currentWaypointIndex;
            }
            
            // Update drift if corner is in sight
            if (nextCorner.magnitude < _driverData.brakeRange && !_driftTimer.IsRunning)
            {
                _driftTimer.Start();
            }
        }

        private void UpdateMove(Vector3 nextWaypoint)
        {
            // Adjust speed
            Move = Move.With(y: _driftTimer.IsRunning ? _driverData.driftSpeed : 1.0f);
            
            // Adjust turning
            var targetForward = nextWaypoint.normalized;
            var currentForward = transform.forward;

            var turnAngle = Vector3.SignedAngle(currentForward, targetForward, Vector3.up);

            Move = turnAngle switch
            {
                > 5f => Move.With(x: 1.0f),
                < -5f => Move.With(x: -1f),
                _ => Move.With(x: 0f)
            };
        }
        

        private void CounterSteer(float angularVelocity)
        {
            if (Mathf.Abs(angularVelocity) > _driverData.spinAngleRange)
            {
                Move = Move.With(x: -Mathf.Sign(angularVelocity));
                IsBreaking = true;
            }
            else
            {
                IsBreaking = false;
            }
        }

        #endregion
        
    }
}