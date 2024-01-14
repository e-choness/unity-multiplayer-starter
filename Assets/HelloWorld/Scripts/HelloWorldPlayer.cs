using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace kart.HelloWorld.Scripts
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        [SerializeField] NetworkVariable<Vector3> position = new(default,
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
                Debug.Log($"{OwnerClientId} - random number: {position.Value}");
            };
        }
        public void Move()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                var randomPosition = GetRandomPositionOnPlane();
                transform.position = randomPosition;
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
        
        private Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-3f, 3f), 1f,Random.Range(-3f, 3f));
        }

        private void Update()
        {
            transform.position = position.Value;
        }
    }
}
