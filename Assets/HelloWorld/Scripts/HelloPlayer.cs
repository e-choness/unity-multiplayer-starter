using kart.Utilities.Extensions;
using Unity.Netcode;
using UnityEngine;

namespace kart.HelloWorld.Scripts
{
    public class HelloPlayer : NetworkBehaviour
    {
        [SerializeField] private float speed;
        // Inputs
        private InputController _inputController;
        
        // Physics
        private Rigidbody _rigidbody;

        public override void OnNetworkSpawn()
        {
            _inputController = gameObject.GetOrAdd<InputController>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _rigidbody.velocity = 
        }
    }
}