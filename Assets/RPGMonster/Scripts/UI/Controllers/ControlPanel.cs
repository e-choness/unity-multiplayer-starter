using kart.RPGMonster.Scripts.Backend.Services;
using kart.RPGMonster.Scripts.UI.Models;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

namespace kart.RPGMonster.Scripts.UI.Controllers
{
    public class ControlPanel : MonoBehaviour
    {
        private MenuSelection _selection = MenuSelection.RootMenu;

        private static readonly PlayerAccount Account = new();
        private PlayFabAuth _playFabAuth;
        private GameObject _azure;

        void Start()
        {
            _playFabAuth = GameObject.Find("PlayFabAuth").GetComponent<PlayFabAuth>();
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
                    GUILayout.Window(0, new Rect(0, 0, 300, 0), LoginWithUserPassWindow, "Login with Userpass");
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
                _selection = MenuSelection.PlayFabLogin;
            }
            
            // if (GUILayout.Button("Login with Azure"))
            // {
            //     _selection = MenuSelection.AzureLogin;
            // }
            
            GUILayout.Space(10);
            
            // TODO: Add additional buttons
        }

        private void LoginWithPlayFabWindow(int windowID)
        {
            GUILayout.Label("Display name:");
            Account.displayName = GUILayout.TextField(Account.displayName, 20);
            
            if (!Account.displayName.IsNullOrWhiteSpace())
            {
                if (GUILayout.Button("Login as Guest"))
                {
                    _playFabAuth.LoginWithCustomId(Account.displayName);
                    _selection = MenuSelection.RootMenu;
                }
                if (GUILayout.Button("Login with Username and Password"))
                {
                    _selection = MenuSelection.PlayFabLoginWithUserPass;
                }
            }
            
            if (GUILayout.Button("Cancel"))
            {
                _selection = MenuSelection.RootMenu;
            }
        }

        private void LoginWithUserPassWindow(int windowID)
        {
            GUILayout.Label("Username:");
            Account.username = GUILayout.TextField(Account.username, 25);
            GUILayout.Label("Password:");
            Account.password = GUILayout.PasswordField(Account.password, '*', 20);
        }
        
        private void LoginWithAzureWindow(int windowID)
        {
            if (GUILayout.Button("Cancel"))
            {
                _selection = MenuSelection.RootMenu;
            }
        }
    }
}
