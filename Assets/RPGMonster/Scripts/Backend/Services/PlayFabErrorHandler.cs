using System;
using PlayFab;

namespace kart.RPGMonster.Scripts.Backend.Models
{
    public static class PlayFabErrorHandler
    {
        public static void Handle(PlayFabError error)
        {
            var errorMessage = $"PlayFab is getting error - http code: {error.HttpCode} detailed report: {error.GenerateErrorReport()}";
            throw new Exception(errorMessage);
        }
    }
}
