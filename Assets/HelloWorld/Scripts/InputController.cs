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
        private InputAction _fireAction;

        public Vector2 Movememt => _moveAction.ReadValue<Vector2>();

        public event Action Fire;

        private void OnEnable()
        {
            // Enable controls
            _inputs = new HelloWorldInputs();
            _moveAction = _inputs.Player.Move;
            _fireAction = _inputs.Player.Fire;
            _moveAction.Enable();
            _fireAction.Enable();
            
            // Subscribe events
            _fireAction.performed += OnFire;
        }

        private void OnDisable()
        {
            // Unsubscribe events
            _fireAction.performed -= OnFire;
            
            // Disable controls
            _moveAction.Disable();
            _fireAction.Disable();
        }

        private void OnFire(InputAction.CallbackContext callback) => Fire?.Invoke();

    }
}
