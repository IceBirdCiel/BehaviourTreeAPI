using PGSauce.Core.Strings;
using UnityEngine;

namespace ESGI.BehaviourTrees.Example
{
    /// <summary>
    /// An Enemy that can be shared between nodes.
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.NodeVariables + "Example/Enemy")]
    public class SharedEnemy : NodeVariable<Enemy>
    {
        
    }
}