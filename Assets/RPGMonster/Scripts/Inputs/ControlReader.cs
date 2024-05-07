using UnityEngine;
using UnityEngine.InputSystem;
using static InputControls;

namespace kart.RPGMonster.Scripts.Inputs
{
    [CreateAssetMenu(fileName = "ControlReader", menuName = "RPG Monster/Control Reader")]
    public class ControlReader : ScriptableObject, IPlayerActions
    {
        public Vector2 Move => _input.Player.Move.ReadValue<Vector2>();

        private InputControls _input;

        private void OnEnable()
        {
            if (_input is not null) return;
            
            _input = new InputControls();
            _input.Player.SetCallbacks(this);
        }

        public void Enable()
        {
            _input.Enable();
        }

        public void Disable()
        {
            _input.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            //...
        }
    }
}
