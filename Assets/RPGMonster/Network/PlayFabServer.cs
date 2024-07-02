using System.Collections;
using Mirror;
using PlayFab;
using UnityEngine;

namespace kart.RPGMonster.Network
{
    public class PlayFabServer : NetworkManager
    {
        private int _connectedPlayerCount = 0;
        public override void Start()
        {
            StartPlayFabAPI();
            StartServer();
        }

        private void StartPlayFabAPI()
        {
            PlayFabMultiplayerAgentAPI.Start();
            StartCoroutine(ReadyForPlayers());
        }

        private IEnumerator ReadyForPlayers()
        {
            yield return new WaitForSeconds(0.5f);
            PlayFabMultiplayerAgentAPI.ReadyForPlayers();
        }

        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            base.OnServerConnect(conn);
            Debug.Log($"Connected client to server, Connection Id: {conn.connectionId}");
            _connectedPlayerCount++;
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            base.OnServerDisconnect(conn);
            Debug.Log($"Client disconnected from server, Connection Id: {conn.connectionId}");
            _connectedPlayerCount--;
            if (_connectedPlayerCount == 0)
            {
                StartCoroutine(Shutdown());
            }
        }

        private IEnumerator Shutdown()
        {
            yield return new WaitForSeconds(5f);
            Application.Quit();
        }
    }
}
