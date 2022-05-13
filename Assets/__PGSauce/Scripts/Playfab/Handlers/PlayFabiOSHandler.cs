using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace PGSauce.Playfab
{
    public class PlayFabiOSHandler : PlayFabHandlerBase
    {
        public override void UnlinkSilentAuth()
        {
            PlayFabClientAPI.UnlinkIOSDeviceID(new UnlinkIOSDeviceIDRequest()
            {
                DeviceId = SystemInfo.deviceUniqueIdentifier
            }, null, null);
        }

        public override void SilentlyAuthenticate(PlayFabStartup playFabStartup, Action<LoginResult> callback, Action<PlayFabError> errorCallback)
        {
            var request = new LoginWithIOSDeviceIDRequest()
            {
                TitleId = PlayFabSettings.TitleId,
                DeviceModel = SystemInfo.deviceModel,
                OS = SystemInfo.operatingSystem,
                DeviceId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true,
                InfoRequestParameters = playFabStartup.InfoRequestParameters
            };
            
            PlayFabClientAPI.LoginWithIOSDeviceID(request, callback, errorCallback);
        }
    }
}