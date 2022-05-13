using PGSauce.Core.Strings;
using PGSauce.Playfab;
using UnityEngine;

namespace PGSauce.Save
{
    [CreateAssetMenu(menuName = MenuPaths.SavedData + "PlayFab/ Auth Type")]
    public class SavedDataPlayFabAuthType : SavedData<PlayFabStartup.AuthTypes>
    {
    }
}