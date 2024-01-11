using UnityEngine;

namespace kart.Kart.Scripts.DebugLog
{
    public class DebugLogEnabler : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            Debug.developerConsoleEnabled = true;
            Debug.developerConsoleVisible = true;
        }
    }
}
