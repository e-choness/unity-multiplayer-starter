using UnityEngine;

namespace kart.Kart.Scripts.Controls
{
    [DisallowMultipleComponent]
    public class KartController : MonoBehaviour
    {
        [Header("Axle Information")]
        [SerializeField] private AxleInfo[] axleInfo;
        
        [Header("Physics")] 
        [SerializeField] private Transform centerOfMass;
        
        [Header("References")] 
        [SerializeField] private InputReader playerInput;

        // Input
        private IDrive _input;
        
        // Physics
        private Rigidbody _rigidbody;
        private Vector3 _originalCenterMass;

        #region Initialization
        
        private void Awake()
        {
            InitInput();
            InitPhysics();
            InitAxleInfo();
        }

        private void InitPhysics()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.centerOfMass = centerOfMass.localPosition;
            _originalCenterMass = centerOfMass.localPosition;
        }

        private void InitInput()
        {
            if (playerInput is IDrive driveInput)
            {
                _input = driveInput;
            }
            
            _input.Enable();
        }

        private void InitAxleInfo()
        {
            foreach (var axle in axleInfo)
            {
                axle.originalForwardFriction = axle.leftWheel.forwardFriction;
                axle.originalSidewayFriction = axle.leftWheel.sidewaysFriction;
            }
        }
        #endregion

        #region HelperMethods

        public AxleInfo[] GetAxleInfo() => axleInfo;

        public Vector2 GetMoveInput()
        {
            return new Vector2(
                NomalizeInput(_input.Move.x),
                NomalizeInput(_input.Move.y));
        }
        
        private float NomalizeInput(float value)
        {
            return value switch
            {
                >= 7.0f => 1.0f,
                <= -7.0f => -1.0f,
                _ => value
            };
        }
        public bool GetBreakInput() => _input.IsBreaking;

        #endregion
    }
}
