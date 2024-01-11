using Unity.Netcode;
using UnityEngine;

namespace kart.Kart.Scripts
{
    public class RpcTest : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            if (!IsServer && IsOwner)
            {
                TestServerRpc(0, NetworkObjectId);
            }
        }

        [ClientRpc]
        private void TestClientRpc(int value, ulong sourceNetworkObjId)
        {
            Debug.Log($"Client Recevied the RPC #{value} on Network Object #{sourceNetworkObjId}");
            if (IsOwner)
            {
                TestServerRpc(value+1, sourceNetworkObjId);
            }
        }

        [ServerRpc]
        private void TestServerRpc(int value, ulong sourceNetworkOjbId)
        {
            Debug.Log($"Server Received the RPC #{value} on the Network Object #{sourceNetworkOjbId}");
            TestClientRpc(value, sourceNetworkOjbId);
        }
    }
}
