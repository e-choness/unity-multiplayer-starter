using UnityEngine;

namespace kart.Kart.Scripts.Controls
{
    [DisallowMultipleComponent]
    public class MotorController : MonoBehaviour
    {
        [Header("Motor Attributes")] 
        [SerializeField] private float maxMotorTorgue = 3000.0f;
        
        // Axle Info from Kart Controller
        private KartController _kartController;
        private AxleInfo[] _axleInfo;
        
        private void Awake()
        {
            _kartController = GetComponent<KartController>();
            _axleInfo = _kartController.GetAxleInfo();
        }

        private void FixedUpdate()
        {
           var motor = CalculateMotor(_kartController.GetMoveInput().y);
           HandleMotors(motor);
        }

        private float CalculateMotor(float verticalInput)
        {
            return maxMotorTorgue * verticalInput;
        }

        private void HandleMotors(float motor)
        {
            foreach (var axle in _axleInfo)
            {
                if (axle.motor)
                {
                    axle.leftWheel.motorTorque = motor;
                    axle.rightWheel.motorTorque = motor;
                }
            }
        }
    }
}