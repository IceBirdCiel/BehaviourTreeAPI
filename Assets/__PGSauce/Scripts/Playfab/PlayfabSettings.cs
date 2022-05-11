using PGSauce.Core.Strings;
using PGSauce.Core.Utilities;
using PlayFab;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.Playfab
{
    /// <summary>
    /// The Playfab settings such as title id
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.PlayFab + "Settings")]
    public class PlayfabSettings : SOSingleton<PlayfabSettings>
    {
        [SerializeField] private PlayFabSharedSettings baseSettings;
        [SerializeField] private string titleId;
        [SerializeField] private string developerSecreteKey;
        
        [Button]
        public void UpdatePlayfabSettings()
        {
            baseSettings.TitleId = titleId;
            baseSettings.DeveloperSecretKey = developerSecreteKey;
            baseSettings.RequestType = WebRequestType.UnityWebRequest;
        }
    }
}