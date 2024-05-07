using kart.Utilities.Extensions;
using Unity.Netcode;
using Unity.VisualScripting;
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
        // private NetworkVariable<FireMessage> _message = new(default,  NetworkVariableReadPermission.Everyone, 
        //     NetworkVariableWritePermission.Owner);
        
        // Const names
        private const string Host = "Host";
        private const string Client = "Client";
        private const string Server = "Server";

        public override void OnNetworkSpawn()
        {
            InitPhysics();
            InitControls();
            SpawnNames();
        }

        private void InitPhysics()
        {
            _rigidbody = GetComponent<Rigidbody>();
            gameObject.transform.position = spawnPosition;
        }

        private void InitControls()
        {
            _inputController = gameObject.GetOrAdd<InputController>();
            _inputController.Fire += OnFireRpc;
        }

        private void SpawnNames()
        {
            var suffix = NetworkManager.Singleton.IsHost ?
                Host : NetworkManager.Singleton.IsServer ? Server : Client;
            gameObject.name += suffix;
        }


        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (!IsOwner) return;
            _rigidbody.linearVelocity = new Vector3(_inputController.Movememt.x, 0.0f, _inputController.Movememt.y) * speed;
        }

        [Rpc(SendTo.ClientsAndHost)]
        private void OnFireRpc()
        {
            // if (!IsOwner) return;
            // _message.Value = new()
            // {
            //     RandomNum = Random.Range(0, 10),
            //     Message = $"Hello Player - OnFire() {_message.Value.RandomNum}"
            // };
            // Debug.Log(_message.Value.Message);
            Debug.Log($"Fire {NetworkObjectId.ToString()}");
        }
    }
}