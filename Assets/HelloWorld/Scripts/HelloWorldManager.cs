using Unity.Netcode;
using UnityEngine;

namespace kart.HelloWorld.Scripts
{
    public class HelloWorldManager : MonoBehaviour
    {
        private const string StringHost = "Host";
        private const string StringClient = "Client";
        private const string StringServer = "Server";

        public static string Mode => GetMode();

        public static string GetMode()
        {
            return NetworkManager.Singleton.IsHost ? StringHost :
                NetworkManager.Singleton.IsServer ? StringServer : StringClient;
        }
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();
                // SubmitNewPosition();
            }
            
            GUILayout.EndArea();
        }

        private static void StartButtons()
        {
            if (GUILayout.Button(StringHost)) NetworkManager.Singleton.StartHost();
            if (GUILayout.Button(StringClient)) NetworkManager.Singleton.StartClient();
            if (GUILayout.Button(StringServer)) NetworkManager.Singleton.StartServer();
            
        }

        private static void StatusLabels()
        {
            GUILayout.Label("Transport: " + NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            
            GUILayout.Label("Mode: " + Mode);
        }

        private static void SubmitNewPosition()
        {
            if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
            {
                if (NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient)
                {
                    foreach (var uid in NetworkManager.Singleton.ConnectedClientsIds)
                    {
                        NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<HelloWorldPlayer>().Move();
                    }
                }
                else
                {
                    var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                    var player = playerObject.GetComponent<HelloWorldPlayer>();
                    player.Move();
                }
            }
        }
    }
}
