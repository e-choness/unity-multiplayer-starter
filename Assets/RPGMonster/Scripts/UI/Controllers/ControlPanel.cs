using System;
using kart.RPGMonster.Scripts.UI.Models;
using UnityEngine;

namespace kart.RPGMonster.Scripts.UI.Controllers
{
    [Serializable]
    public enum MenuSelection
    {
        RootMenu,
        PlayFabLogin,
        PlayFabLoginWithUserPass,
        AzureLogin
    }
    public class ControlPanel : MonoBehaviour
    {
        private MenuSelection _selection;

        [SerializeField] private PlayerAccount account;
        private GameObject _playFab;
        private GameObject _azure;

        void Start()
        {
            _playFab = GameObject.Find("PlayFab");
            _azure = GameObject.Find("Azure");
        }

        private void OnGUI()
        {
            switch (_selection)
            {
                case MenuSelection.RootMenu:
                    GUILayout.Window(0, new Rect(0, 0, 300, 0), OptionsWindow, "Options");
                    break;
                case MenuSelection.PlayFabLogin:
                    GUILayout.Window(0, new Rect(0, 0, 300, 0), LoginWithPlayFabWindow, "Login with PlayFab");
                    break;
                case MenuSelection.PlayFabLoginWithUserPass:
                    break;
                case MenuSelection.AzureLogin:
                    GUILayout.Window(0, new Rect(0, 0, 300, 0), LoginWithAzureWindow, "Login with Azure");
                    break;
                default:
                    Debug.LogWarning("ControlPanel - Not a valid login method.");
                    break;
            }
        }

        private void OptionsWindow(int windowID)
        {
            if (GUILayout.Button("Login with PlayFab"))
            {
                // TODO: button to login with PlayFab
                _selection = MenuSelection.PlayFabLogin;
            }
            
            if(GUILayout.Button("Login with PlayFab User Pass")){}
            {
                _selection = MenuSelection.PlayFabLoginWithUserPass;
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
            account.displayName = GUILayout.TextField(account.displayName, 20);
        }

        private void LoginWithUserPass(int windowID)
        {
            
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
