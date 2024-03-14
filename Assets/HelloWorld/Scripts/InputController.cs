using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace kart
{
    public class InputController : MonoBehaviour
    {
        // Inputs
        private HelloWorldInputs _inputs;
        private InputAction _moveAction;

        public event Action OnMove;

        private void OnEnable()
        {
            _inputs = new HelloWorldInputs();
            _moveAction = _inputs.Player.Move;
            _moveAction.Enable();
            _moveAction.performed += Move;

        }

        private void OnDisable()
        {
            _moveAction.performed -= Move;
            _moveAction.Disable();
        }

        private void Move(InputAction.CallbackContext callback) => OnMove?.Invoke();

    }
}
