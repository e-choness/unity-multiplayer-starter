using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputActions;

namespace kart.Kart.Scripts.Controls
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Kart/Input Reader")]
    public class InputReader : ScriptableObject, IPlayerActions, IDrive
    {
        public Vector2 Move => _inputActions.Player.Move.ReadValue<Vector2>();
        public bool IsBreaking => _inputActions.Player.Brake.ReadValue<float>() > 0;
        
        private PlayerInputActions _inputActions;

        private void OnEnable()
        {
            if (_inputActions is null)
            {
                _inputActions = new PlayerInputActions();
                _inputActions.Player.SetCallbacks(this);
            }
        }

        public void Enable()
        {
            _inputActions.Enable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            //
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            //
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            //
        }

        public void OnBrake(InputAction.CallbackContext context)
        {
            //
        }
    }
}
