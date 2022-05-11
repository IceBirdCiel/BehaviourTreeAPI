using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using PGSauce.Core.PGDebugging;
using PGSauce.Playfab;
using PGSauce.Save;
using PGSauce.Unity;
using PlayFab.ClientModels;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

namespace PGSauce.PGStartup
{
    public class Startup : PGMonoBehaviour
    {
        [SerializeField, BoxGroup(Modules)] private PlayFabStartup playfab;

        [SerializeField, BoxGroup(PlayFab)] private bool resetPlayFab;
        [SerializeField, BoxGroup(PlayFab)] private GetPlayerCombinedInfoRequestParams playfabPlayerInfo;

        private const string Modules = "Modules";
        private const string Settings = "Settings";
        private const string PlayFab = Settings + " PlayFab";
        
        private bool ResetPlayFab
        {
            get {
#if UNITY_EDITOR
                return resetPlayFab;
#endif
                return false;
            }
        }
        
#if UNITY_ASSERTIONS
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void DisableAnimancerWarnings()
        {
            Animancer.OptionalWarning.EndEventInterrupt.Disable();
    
            // You could disable OptionalWarning.All, but that is not recommended for obvious reasons.
        }
#endif

        async void Awake()
        {
            if (PgSave.Instance.IsGameLaunchedForTheFirstTime)
            {
                PGDebug.Message("Game Launched For the first time").Log();
                PgSave.Instance.GameLaunchedForTheFirstTime.SaveData(false);
            }
            else
            {
                PGDebug.Message("Game NOT Launched the first time").Log();
            }
            playfab.InitPlayFab(ResetPlayFab, playfabPlayerInfo);
            SceneManager.LoadScene(1);
        }
    }
}
