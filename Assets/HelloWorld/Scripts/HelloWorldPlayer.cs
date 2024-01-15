using Unity.Netcode;
using UnityEngine;

namespace kart.HelloWorld.Scripts
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        const string Message = "Custom data message";
        [SerializeField] NetworkVariable<CustomData> position = new(default,
            NetworkVariableReadPermission.Everyone, 
            NetworkVariableWritePermission.Owner);

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                Move();
            }

            position.OnValueChanged += (_, _) =>
            {
                Debug.Log($"{OwnerClientId} - random number: {position.Value.Coordinate} " +
                          $"and it left a message {position.Value.Message}");
            };
        }
        public void Move()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                var randomPosition = GetRandomPositionOnPlane();
                transform.position = randomPosition.Coordinate;
                position.Value = randomPosition;
            }
            else
            {
                SubmitPositionRequestServerRpc();
            }
        }

        [ServerRpc]
        private void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            position.Value = GetRandomPositionOnPlane();
        }
        
        private CustomData GetRandomPositionOnPlane()
        {
            return new CustomData()
            {
                Coordinate = new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f)),
                Message = Message
            };
        }

        private void Update()
        {
            transform.position = position.Value.Coordinate;
        }
    }
}
