using UnityEngine;

namespace ESGI.BehaviourTrees
{
    /// <summary>
    /// A node that modifies the state of the world
    /// </summary>
    public abstract class ActionNode<TAgent> : Node<TAgent> where TAgent : MonoBehaviour
    {
        
    }
}