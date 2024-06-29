using kart.RPGMonster.Scripts.Backend.Models;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace kart.RPGMonster.Scripts.Backend.Services
{
    public class PlayFabAuth : MonoBehaviour
    {
        private static string _displayName;

        private static AuthSettings _settings;
        private void Awake()
        {
            _settings = new AuthSettings();
        }

        private void OnEnable()
        {
            PlayFabResultHandler<LoginResult>.OnGetResult += OnLoginSuccess;
            PlayFabResultHandler<RegisterPlayFabUserResult>.OnGetResult += OnRegisterSuccess;
        }

        private void OnDisable()
        {
            PlayFabResultHandler<LoginResult>.OnGetResult -= OnLoginSuccess;
            PlayFabResultHandler<RegisterPlayFabUserResult>.OnGetResult -= OnRegisterSuccess;
        }

        public void LoginWithCustomId(string displayName)
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true,
                InfoRequestParameters = 
                    new GetPlayerCombinedInfoRequestParams {GetPlayerProfile =  true}
            };
            
            _displayName = displayName;
            
            PlayFabClientAPI.LoginWithCustomID(
                request, PlayFabResultHandler<LoginResult>.Handle, PlayFabErrorHandler.Handle);
        }

        private static void OnLoginSuccess(LoginResult result)
        {
            Debug.Log("PlayFabAuth - Login succeeded.");
            
            _settings.EntityId = result.EntityToken.Entity.Id;
            var getName = result.InfoResultPayload.PlayerProfile.DisplayName;
            if (string.IsNullOrEmpty(getName))
            {
                _settings.DisplayName = _displayName;
                PlayFabProfile.UpdateDisplayName(_settings.DisplayName);
            }
            else
            {
                _settings.DisplayName = getName;
            }
        }

        public void LoginWithUsername(string username, string password, string displayName)
        {
            var request = new LoginWithPlayFabRequest();
            request.Username = username;
            request.Password = password;
            request.InfoRequestParameters = 
                new GetPlayerCombinedInfoRequestParams { GetPlayerProfile = true };

            _displayName = displayName;
            
            PlayFabClientAPI.LoginWithPlayFab(
                request, PlayFabResultHandler<LoginResult>.Handle, PlayFabErrorHandler.Handle);
        }

        public void RegisterWithUsername(string username, string password, string displayName)
        {
            var request = new RegisterPlayFabUserRequest();
            request.Username = username;
            request.Password = password;
            request.RequireBothUsernameAndEmail = false;

            _displayName = displayName;
            
            PlayFabClientAPI.RegisterPlayFabUser(request, PlayFabResultHandler<RegisterPlayFabUserResult>.Handle, PlayFabErrorHandler.Handle);
        }

        private static void OnRegisterSuccess(RegisterPlayFabUserResult result)
        {
            _settings.EntityId = result.EntityToken.Entity.Id;
            PlayFabProfile.UpdateDisplayName(_displayName);
        }
    }
}
