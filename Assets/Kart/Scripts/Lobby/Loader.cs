using Eflatun.SceneReference;
using Unity.Netcode;
using UnityEngine.SceneManagement;

namespace kart.Kart.Scripts.Lobby {
    public static class Loader {
        public static void LoadNetwork(SceneReference scene) {
            NetworkManager.Singleton.SceneManager.LoadScene(scene.Name, LoadSceneMode.Single);
        }
    }
}