using UnityEngine;

namespace kart.Kart.Scripts.Controls
{
    [DisallowMultipleComponent]
    public class WheelAnimator : MonoBehaviour
    {
        // Component references
        private AxleInfo[] _axleInfo;

        // Transform references
        Vector3 _position;
        Quaternion _rotation;
        
        private void Awake()
        {
            var kartController = GetComponent<KartController>();
            _axleInfo = kartController.GetAxleInfo();
        }
        
        #region VisualUpdate

        private void LateUpdate()
        {
            foreach (var axle in _axleInfo)
            {
                UpdateWheelVisuals(axle.leftWheel);
                UpdateWheelVisuals(axle.rightWheel);
            }
        }

        private void UpdateWheelVisuals(WheelCollider wheel)
        {
            if (wheel.transform.childCount == 0) return;

            var visualWheel = wheel.transform.GetChild(0);
            
            wheel.GetWorldPose(out _position, out _rotation);

            visualWheel.position = _position;
            visualWheel.rotation = _rotation;
        }

        #endregion
    }
}