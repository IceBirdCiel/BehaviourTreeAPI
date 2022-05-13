using PGSauce.Core.Strings;
using UnityEngine;

namespace ESGI.BehaviourTrees.Variables
{
    /// <summary>
    /// A transform that can be shared between nodes.
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.NodeVariables + "Transform")]
    public class SharedTransform : NodeVariable<Transform>
    {
        
    }
}