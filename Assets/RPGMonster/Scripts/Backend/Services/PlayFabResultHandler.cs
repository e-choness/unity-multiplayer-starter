using System;
using PlayFab.SharedModels;
using UnityEngine;

namespace kart.RPGMonster.Scripts.Backend.Services
{
    public static class PlayFabResultHandler<T>  where T : PlayFabResultCommon
    {
        public static event Action<T> OnGetResult;

        public static void Handle(T result)
        {
            if (result == null)
            {
                Debug.LogWarning("PlayFabResultHandler - no result.");
                return;
            }
            
            OnGetResult?.Invoke(result);
        }
    }
}
