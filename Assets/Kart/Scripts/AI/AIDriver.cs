using System;
using kart.Kart.Scripts.Controls;
using kart.Kart.Scripts.System;
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
        
        // Physics
        // Kart yaw from previous frame
        private float _previousYaw;
        
        // Timer
        private CountdownTimer _driftTimer;
        
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
        
        
        
    }
}