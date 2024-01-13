using UnityEngine;

namespace kart.Kart.Scripts.DebugLog
{
    public class DebugHelper : MonoBehaviour
    {
        private static string _myLog = "";
        private string _output;
        private string _stack;
// #if !UNITY_EDITOR
        private void OnEnable()
        {
            Application.logMessageReceived += Logging;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= Logging;
        }

        public void Logging(string logString, string stackTrace, LogType logType)
        {
            _output = logString;
            _stack = stackTrace;
            _myLog = _output + "/n" + _myLog;

            if (_myLog.Length > 5000)
            {
                _myLog = _myLog.Substring(0, 4000);
            }
        }

        private void OnGUI()
        {
            if (!Application.isEditor)
            {
                _myLog = GUI.TextArea(new Rect(10, 10, Screen.width - 10, Screen.height - 10), _myLog);
            }
        }
// #endif
    }
}
