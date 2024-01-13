using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace kart.HelloWorld.Scripts
{
    public class ClientConnectionHandler : NetworkBehaviour
    {
        public List<uint> AlternatePlayerPrefabs;

        public void SetClientPlayerPrefab(int index)
        {
            if (index > AlternatePlayerPrefabs.Count)
            {
                Debug.LogError($"Provided client index {index} is outside PreFab list count {AlternatePlayerPrefabs.Count}");
                return;
            }

            if (NetworkManager.IsListening || IsSpawned)
            {
                Debug.LogError("Network Manager is not set or listening.");
                return;
            }

            NetworkManager.NetworkConfig.ConnectionData = System.BitConverter.GetBytes(index);
        }

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                NetworkManager.ConnectionApprovalCallback += ConnectionApprovalCallback;
            }
        }

        private void ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
        {
            var playerPrefabIndex = BitConverter.ToInt32(request.Payload);
            if (AlternatePlayerPrefabs.Count > playerPrefabIndex)
            {
                response.PlayerPrefabHash = AlternatePlayerPrefabs[playerPrefabIndex];
            }
            else
            {
                Debug.LogError($"{request.ClientNetworkId} player prefab index is out of alternate player prefab list range.");
                return;
            }
            // TODO: add operations for the response.
        }
    }
}
