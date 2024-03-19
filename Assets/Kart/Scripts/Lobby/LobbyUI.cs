using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.UI;

namespace kart.Kart.Scripts.Lobby {
    public class LobbyUI : MonoBehaviour {
        [SerializeField] Button createLobbyButton;
        [SerializeField] Button joinLobbyButton;
        [SerializeField] SceneReference gameScene;

        void Awake() {
            createLobbyButton.onClick.AddListener(CreateGame);
            joinLobbyButton.onClick.AddListener(JoinGame);
        }

        async void CreateGame() {
            Debug.Log("LobbyUI - CreateGame clicked.");
            await LobbyHandler.Instance.CreateLobby();
            
            Debug.Log("LobbyUI - Done Create Lobby.");
            Loader.LoadNetwork(gameScene);
        }

        async void JoinGame() {
            Debug.Log("LobbyUI - QuickJoinLobby clicked.");
            await LobbyHandler.Instance.QuickJoinLobby();
        }
    }
}