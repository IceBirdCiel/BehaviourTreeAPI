using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace PGSauce.Playfab
{
    public class PlayFabOtherHandler : PlayFabHandlerBase
    {
        public override void UnlinkSilentAuth()
        {
            PlayFabClientAPI.UnlinkCustomID(new UnlinkCustomIDRequest()
            {
                CustomId = SystemInfo.deviceUniqueIdentifier
            }, null, null);
        }

        public override void SilentlyAuthenticate(PlayFabStartup playFabStartup, Action<LoginResult> successCallback,
            Action<PlayFabError> errorCallback)
        {
            var request = new LoginWithCustomIDRequest()
            {
                TitleId = PlayFabSettings.TitleId,
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true,
                InfoRequestParameters = playFabStartup.InfoRequestParameters
            };
            PlayFabClientAPI.LoginWithCustomID(request,successCallback, errorCallback);
        }
    }
}