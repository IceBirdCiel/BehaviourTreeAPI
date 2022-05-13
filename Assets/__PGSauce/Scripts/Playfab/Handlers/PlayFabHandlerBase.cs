using System;
using PlayFab;
using PlayFab.ClientModels;

namespace PGSauce.Playfab
{
    public abstract class PlayFabHandlerBase
    {
        public abstract void UnlinkSilentAuth();
        public abstract void SilentlyAuthenticate(PlayFabStartup playFabStartup, Action<LoginResult> callback,
            Action<PlayFabError> errorCallback);
    }
}