using UnityEngine;
using UnityEngine.InputSystem;

namespace kart.Kart.Scripts.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Kart/InputReader")]
    public class InputReader : ScriptableObject, PlayerInputActions.IPlayerActions
    {
        public Vector3 Move => _inputActions.Player.Move.ReadValue<Vector2>();
        public bool IsBreaking => _inputActions.Player.Brake.ReadValue<float>() > 0;
        
        private PlayerInputActions _inputActions;

        private void OnEnable()
        {
            if (_inputActions == null)
            {
                _inputActions = new();
                _inputActions.Player.SetCallbacks(this);
            }
        }

        public void Enable()
        {
            _inputActions.Enable();
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnBrake(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
