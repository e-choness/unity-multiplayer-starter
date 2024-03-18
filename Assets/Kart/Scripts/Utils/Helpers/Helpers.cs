namespace kart.Kart.Scripts.Utils.Helpers {
    public static class Helpers {
        public static void QuitGame() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }
    }
}