using UnityEngine;
using Utilities;

namespace kart.Kart.Scripts.Controls
{
    public class BrakeController : MonoBehaviour
    {
        [SerializeField] private float brakeTorgue = 100.0f;

        // Velocity
        private float _brakeVelocity;
        
        // Component references
        private Rigidbody _rigidbody;
        private KartController _kartController;
        private AxleInfo[] _axleInfo;

        private void Awake()
        {
            _kartController = GetComponent<KartController>();
            _rigidbody = GetComponent<Rigidbody>();
            _axleInfo = _kartController.GetAxleInfo();
        }
        private void FixedUpdate()
        {
            HandleBrakes();
        }

        private void HandleBrakes()
        {
            foreach (var axle in _axleInfo)
            {
                if (!axle.motor) return;

                var isBreaking = _kartController.GetBreakInput();
                if (isBreaking)
                {
                    _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;

                    // Gradually changes z to 0 overtime using current  
                    var newZ = Mathf.SmoothDamp(_rigidbody.velocity.z, 0, ref _brakeVelocity, 1.0f);
                    _rigidbody.velocity = _rigidbody.velocity.With(z: newZ);

                    axle.leftWheel.brakeTorque = brakeTorgue;
                    axle.rightWheel.brakeTorque = brakeTorgue;
                }
                else
                {
                    _rigidbody.constraints = RigidbodyConstraints.None;

                    axle.leftWheel.brakeTorque = 0.0f;
                    axle.rightWheel.brakeTorque = 0.0f;
                }
            }
        }
    }
}