using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace PGSauce.Playfab
{
    public class PlayFabAndroidHandler : PlayFabHandlerBase
    {
        public override void UnlinkSilentAuth()
        {
            //Get the device id from native android
            var deviceId = GetDeviceId();

            //Fire and forget, unlink this android device.
            PlayFabClientAPI.UnlinkAndroidDeviceID(new UnlinkAndroidDeviceIDRequest() {
                AndroidDeviceId = deviceId
            }, null, null);
        }

        private static string GetDeviceId()
        {
            var up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
            var contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
            var secure = new AndroidJavaClass("android.provider.Settings$Secure");
            var deviceId = secure.CallStatic<string>("getString", contentResolver, "android_id");
            return deviceId;
        }

        public override void SilentlyAuthenticate(PlayFabStartup playFabStartup, Action<LoginResult> callback, Action<PlayFabError> errorCallback)
        {
            var deviceId = GetDeviceId();
            var request = new LoginWithAndroidDeviceIDRequest()
            {
                TitleId = PlayFabSettings.TitleId,
                AndroidDevice = SystemInfo.deviceModel,
                OS = SystemInfo.operatingSystem,
                AndroidDeviceId = deviceId,
                CreateAccount = true,
                InfoRequestParameters = playFabStartup.InfoRequestParameters
            };
            
            PlayFabClientAPI.LoginWithAndroidDeviceID(request, callback, errorCallback);
        }
    }
}