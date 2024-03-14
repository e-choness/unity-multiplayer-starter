using kart.Utilities.Extensions;
using Unity.Netcode;
using UnityEngine;

namespace kart.HelloWorld.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class HelloPlayer : NetworkBehaviour
    {
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private Vector3 spawnPosition = Vector3.up;
        // Inputs
        private InputController _inputController;
        
        // Physics
        private Rigidbody _rigidbody;
        
        // Network components
        private NetworkVariable<FireMessage> _message = new();

        public override void OnNetworkSpawn()
        {
            InitPhysics();
            InitControls();
        }

        private void InitPhysics()
        {
            _rigidbody = GetComponent<Rigidbody>();
            gameObject.transform.position = spawnPosition;
        }

        private void InitControls()
        {
            _inputController = gameObject.GetOrAdd<InputController>();
            _inputController.Fire += OnFire;
        }


        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if(IsOwner)
                _rigidbody.velocity = new Vector3(_inputController.Movememt.x, 0.0f, _inputController.Movememt.y) * speed;
        }

        private void OnFire()
        {
            _message.Value = new()
            {
                RandomNum = Random.Range(0, 10),
                Message = $"Hello Player - OnFire() {_message.Value.RandomNum}"
            };
            Debug.Log(_message.Value.Message);
        }
    }
}