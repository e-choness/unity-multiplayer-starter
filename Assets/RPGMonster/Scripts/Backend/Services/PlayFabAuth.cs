using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace kart.RPGMonster.Scripts.Backend.Models
{
    public class PlayFabAuth : MonoBehaviour
    {
        public static PlayFabSettings Settings;
        public string DisplayName;
        private void Awake()
        {
            Settings = new();
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
            
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginWithCustomIdSuccess, PlayFabErrorHandler.HandleError);
        }

        private void OnLoginWithCustomIdSuccess(LoginResult result)
        {
            if (result == null) return;
            
            Debug.Log("PlayFabAuth - Login with CustomID succeeded.");
            Settings.EntityId = result.EntityToken.Entity.Id;
            Settings.DisplayName = DisplayName;
            UpdateDisplayName(Settings.DisplayName);
        }

        private void UpdateDisplayName(string displayName)
        {
            var request = new UpdateUserTitleDisplayNameRequest { DisplayName = displayName };
            
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateDisplayNameSuccess, PlayFabErrorHandler.HandleError);
        }

        private void OnUpdateDisplayNameSuccess(UpdateUserTitleDisplayNameResult result)
        {
            if (result == null) return;
            
            Debug.Log("PlayFabAuth - Display name updated.");
        }
    }
}
