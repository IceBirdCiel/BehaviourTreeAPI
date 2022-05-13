using PGSauce.Core.PGEditor;
using PGSauce.Core.Strings;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace ESGI.BehaviourTrees.Editor
{
    public class BehaviourTreeEditorWindow : OdinEditorWindow
    {
        [MenuItem(MenuPaths.MenuBase + "Behaviour Tree Editor")]
        public static void OpenWindow()
        {
            GetWindow<BehaviourTreeEditorWindow>("Behaviour Tree Editor").Show();
        }
    }
}