using kart.RPGMonster.Scripts.Backend.Models;
using PlayFab;
using PlayFab.ClientModels;

namespace kart.RPGMonster.Scripts.Backend.Services
{
    public static class PlayFabProfile
    {
        public static void UpdateDisplayName(string displayName)
        {
            var request = new UpdateUserTitleDisplayNameRequest { DisplayName = displayName };
            
            PlayFabClientAPI.UpdateUserTitleDisplayName(
                request, PlayFabResultHandler<UpdateUserTitleDisplayNameResult>.Handle, PlayFabErrorHandler.Handle);
        }
    }
}
