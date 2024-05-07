using System;
using UnityEngine;

namespace kart.RPGMonster.Scripts.UI
{
    [Serializable]
    public enum MenuSelection
    {
        RootMenu,
        PlayFabLogin,
        AzureLogin
    }
    public class ControlPanel : MonoBehaviour
    {
        private MenuSelection _selection;

        private GameObject _playFab;
        private GameObject _azure;

        void Start()
        {
            _playFab = GameObject.Find("PlayFab");
            _azure = GameObject.Find("Azure");
        }

        private void OnGUI()
        {
            if (_selection == MenuSelection.RootMenu)
            {
                GUILayout.Window(0, new Rect(0, 0, 300, 0), OptionsWindow, "Options");
                
            }
        }

        private void OptionsWindow(int windowID)
        {
            if (GUILayout.Button("Login with PlayFab"))
            {
                // TODO: button to login with PlayFab
                _selection = MenuSelection.PlayFabLogin;
            }

            if (GUILayout.Button("Login with Azure"))
            {
                _selection = MenuSelection.AzureLogin;
            }
            
            GUILayout.Space(10);
            
            // TODO: Add additional buttons
        }

        private void LoginWithPlayFabWindow(int windowID)
        {
            GUILayout.Label("Display name:");
            {
                // TODO: button to each sub-window
            }
        }

        private void LoginWithAzureWindow(int windowID)
        {
            if (GUILayout.Button("Login with Azure"))
            {
                // TODO: button to login with Azure, something like _azure.GetComponent<AzureAuth>().LoginWithAzure();
            }

            if (GUILayout.Button("Cancel"))
            {
                _selection = MenuSelection.RootMenu;
            }
        }
    }
}
