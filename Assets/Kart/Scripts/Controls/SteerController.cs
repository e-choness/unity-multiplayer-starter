using UnityEngine;

namespace kart.Kart.Scripts.Controls
{
    public class SteerController : MonoBehaviour
    {
        [Header("Steering Attributes")] 
        [SerializeField] private float maxSteeringAngle = 30.0f;
        
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
            var steering = CalculateSteering(_kartController.GetMoveInput().x);
            HandleSteering(steering);
        }

        private float CalculateSteering(float horizontalInput)
        {
            return maxSteeringAngle * horizontalInput;
        }

        private void HandleSteering(float steering)
        {
            foreach (var axle in _axleInfo)
            {
                if (axle.steering)
                {
                    axle.leftWheel.steerAngle = steering;
                    axle.rightWheel.steerAngle = steering;
                }
            }
        }
    }
}