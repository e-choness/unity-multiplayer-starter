using System.Linq;
using UnityEngine;

namespace kart.Kart.Scripts.Controls
{
    public class DriftController : MonoBehaviour
    {
        [Header("State")] 
        [SerializeField] private bool isGrounded = true;
        
        // Drift Velocity, used for SmoothDamp() - current velocity reference
        private float _driftVelocity;
        
        // Component references
        private KartController _kartController;
        private AxleInfo[] _axleInfo;
        
        private void Awake()
        {
            _kartController = GetComponent<KartController>();
            _axleInfo = _kartController.GetAxleInfo();
        }

        private void FixedUpdate()
        {
            HandleDrifts();
        }

        private void HandleDrifts()
        {
            var isBreaking = _kartController.GetBreakInput();
            foreach (var axle in _axleInfo)
            {
                if (!axle.motor) return;

                if (isBreaking)
                {
                    ApplyDriftFriction(axle.leftWheel);
                    ApplyDriftFriction(axle.rightWheel);
                }
                else
                {
                    ResetDriftFriction(axle.leftWheel);
                    ResetDriftFriction(axle.rightWheel);
                }
            }
            
        }
        
        private void ApplyDriftFriction(WheelCollider wheel)
        {
            if (wheel.GetGroundHit(out var ground))
            {
                wheel.forwardFriction = UpdateFriction(wheel.forwardFriction);
                wheel.sidewaysFriction = UpdateFriction(wheel.sidewaysFriction);
                isGrounded = true;
            }
        }

        private WheelFrictionCurve UpdateFriction(WheelFrictionCurve friction)
        {
            friction.stiffness = _kartController.GetBreakInput()
                ? Mathf.SmoothDamp(friction.stiffness, 0.5f, ref _driftVelocity, Time.deltaTime * 2.0f)
                : 1.0f;
            return friction;
        }
        
        private void ResetDriftFriction(WheelCollider wheel)
        {
            var axle = _axleInfo.FirstOrDefault(
                axle => axle.leftWheel == wheel || axle.rightWheel == wheel);
            if (axle is null) return;

            wheel.forwardFriction = axle.originalForwardFriction;
            wheel.sidewaysFriction = axle.originalSidewayFriction;
        }
    }
}