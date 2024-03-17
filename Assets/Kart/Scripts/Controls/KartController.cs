using UnityEngine;

namespace kart.Kart.Scripts.Controls
{
    [DisallowMultipleComponent]
    public class KartController : MonoBehaviour
    {
        [Header("Axle Information")]
        [SerializeField] private AxleInfo[] axleInfo;
        
        [Header("References")] 
        [SerializeField] private InputReader playerInput;

        // Input
        private IDrive _input;
        

        #region Initialization
        
        private void Awake()
        {
            InitInput();
            InitAxleInfo();
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

        #region Getters

        public AxleInfo[] GetAxleInfo() => axleInfo;

        public Vector2 GetMoveInput()
        {
            return new Vector2(
                NormalizeInput(_input.Move.x),
                NormalizeInput(_input.Move.y));
        }
        
        private float NormalizeInput(float value)
        {
            return value switch
            {
                >= 7.0f => 1.0f,
                <= -7.0f => -1.0f,
                _ => value
            };
        }
        public bool IsBreaking() => _input.IsBreaking;

        #endregion
    }
}
