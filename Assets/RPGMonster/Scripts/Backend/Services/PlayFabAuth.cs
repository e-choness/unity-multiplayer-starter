using kart.RPGMonster.Scripts.Backend.Models;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using PlayFabSettings = kart.RPGMonster.Scripts.Backend.Models.PlayFabSettings;

namespace kart.RPGMonster.Scripts.Backend.Services
{
    public class PlayFabAuth : MonoBehaviour
    {
        private string _displayName;
        
        public static PlayFabSettings Settings = new();
        private void Awake()
        {
            Settings = new PlayFabSettings();
        }

        private void OnDestroy()
        {
            Settings = null;
        }

        public void LoginWithCustomId(string displayName)
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true
            };
            
            _displayName = displayName;
            
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginWithCustomIdSuccess, PlayFabErrorHandler.Handle);
        }

        private void OnLoginWithCustomIdSuccess(LoginResult result)
        {
            if (result == null) return;
            
            Debug.Log("PlayFabAuth - Login with CustomID succeeded.");
            
            Settings.EntityId = result.EntityToken.Entity.Id;
            Settings.DisplayName = _displayName;
            UpdateDisplayName(Settings.DisplayName);
        }

        private void UpdateDisplayName(string displayName)
        {
            var request = new UpdateUserTitleDisplayNameRequest { DisplayName = displayName };
            
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateDisplayNameSuccess, PlayFabErrorHandler.Handle);
        }

        private void OnUpdateDisplayNameSuccess(UpdateUserTitleDisplayNameResult result)
        {
            if (result == null) return;
            
            Debug.Log("PlayFabAuth - Display name updated.");
        }
    }
}
