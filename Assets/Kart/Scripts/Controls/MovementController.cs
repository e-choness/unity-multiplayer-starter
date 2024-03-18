using kart.Kart.Scripts.Utils.Extensions;
using UnityEngine;

namespace kart.Kart.Scripts.Controls
{
    [DisallowMultipleComponent]
    public class MovementController : MonoBehaviour
    {
        [Header("Speed Attributes")] 
        [SerializeField] private float maxSpeed = 100.0f;
        
        [Header("Turn Attributes")]
        [SerializeField] private AnimationCurve turnCurve;
        [SerializeField] private float turnStrength = 1500.0f;

        [Header("Physics Attribute")] 
        [SerializeField] private float downForce = 100.0f;
        [SerializeField] private float lateralGravityScale = 10.0f;
        [SerializeField] private Transform centerOfMass;
        
        // Physics attribute
        private const float CenterOfMassOffset = -0.5f;
        private Vector3 _originalCenterOfMass;
        private float _gravity => Physics.gravity.y; 
            
        // Ticks and frames attribute
        private const float ServerTick = 60.0f;
        
        // Velocity and speed
        private const float ThresholdSpeed = 10.0f;
        private Vector3 _kartVelocity; 
        
        // Component references
        private KartController _kartController;
        private Rigidbody _rigidbody;
        private DriftController _driftController;

        private void Awake()
        {
            InitComponents();
            InitPhysics();
        }

        private void InitComponents()
        {
            _kartController = GetComponent<KartController>();
            _rigidbody = GetComponent<Rigidbody>();
            _driftController = GetComponent<DriftController>();
        }

        private void InitPhysics()
        {
            _originalCenterOfMass = centerOfMass.localPosition;
            _rigidbody.centerOfMass = _originalCenterOfMass;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _kartVelocity = transform.InverseTransformDirection(_rigidbody.velocity);
            var input = _kartController.GetMoveInput();
            if (_driftController.IsGrounded())
            {
                HandleGroundedMovement(input);
            }
            else
            {
                HandleAirBornMovement();
            }
        }

        private void HandleGroundedMovement(Vector2 input)
        {
            Turn(input);
            Accelerate(input);
            ApplyDownForce();
            ShiftCenterOfMass(input.y);
        }

        private void Turn(Vector2 input)
        {
            if (Mathf.Abs(input.y) > 0.1f || Mathf.Abs(_kartVelocity.z) > 1.0f)
            {
                var turnMultiplier = Mathf.Clamp01(turnCurve.Evaluate(_kartVelocity.magnitude / maxSpeed));
                _rigidbody.AddTorque(
                    Vector3.up * 
                    (input.x * Mathf.Sign(_kartVelocity.z) * turnStrength * 100.0f * turnMultiplier)
                );
            }
        }

        private void Accelerate(Vector2 input)
        {
            if (_kartController.IsBreaking()) return;

            var targetSpeed = input.y * maxSpeed;
            var forwardWithoutY = transform.forward.With(y: 0).normalized;

            _rigidbody.velocity = Vector3.Lerp(
                _rigidbody.velocity,
                forwardWithoutY * targetSpeed,
                1 / ServerTick // Minimum time between server ticks
            );
        }

        private void ApplyDownForce()
        {
            var speedFactor = Mathf.Clamp01(_rigidbody.velocity.magnitude / maxSpeed);
            var lateralGravity = Mathf.Abs(Vector3.Dot(_rigidbody.velocity, transform.right));
            var downForceFactor = Mathf.Max(speedFactor, lateralGravity / lateralGravityScale);
            _rigidbody.AddForce(-transform.up * (downForce * _rigidbody.mass * downForceFactor ));
        }

        private void ShiftCenterOfMass(float verticalInput)
        {
            var centerOfMassChanges = 
                _rigidbody.velocity.magnitude > ThresholdSpeed
                ? new Vector3(0.0f, 0.0f, 
                    Mathf.Abs(verticalInput) > 0.1f
                    ? Mathf.Sign(verticalInput) * CenterOfMassOffset
                    : 0.0f)
                : Vector3.zero;
            
            _rigidbody.centerOfMass = _originalCenterOfMass + centerOfMassChanges;
        }

        private void HandleAirBornMovement()
        {
            var velocity = _rigidbody.velocity;
            _rigidbody.velocity = Vector3.Lerp(
                velocity,
                velocity + Vector3.down * _gravity,
                Time.deltaTime * _gravity
            );
        }
    }
}