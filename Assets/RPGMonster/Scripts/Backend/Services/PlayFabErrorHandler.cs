using System;
using PlayFab;
using UnityEngine;

namespace kart.RPGMonster.Scripts.Backend.Models
{
    public static class PlayFabErrorHandler
    {
        public static event Action<PlayFabError> GettingError;

        public static void Handle(PlayFabError error)
        {
            var errorMessage = $"PlayFab is getting error - http code: {error.HttpCode} detailed report: {error.GenerateErrorReport()}";
            Debug.LogError(errorMessage);
            GettingError?.Invoke(error);
        }
    }
}
