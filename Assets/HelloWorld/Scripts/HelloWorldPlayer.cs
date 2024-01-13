using Unity.Netcode;
using UnityEngine;

namespace kart.HelloWorld.Scripts
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        [SerializeField] NetworkVariable<Vector3> Position = new();

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                Move();
            }
        }
        public void Move()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                var randomPosition = GetRandomPositionOnPlane();
                transform.position = randomPosition;
                Position.Value = randomPosition;
            }
            else
            {
                SubmitPositionRequestServerRpc();
            }
        }

        [ServerRpc]
        private void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            Position.Value = GetRandomPositionOnPlane();
        }
        
        private Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-3f, 3f), 1f,Random.Range(-3f, 3f));
        }

        private void Update()
        {
            transform.position = Position.Value;
        }
    }
}
