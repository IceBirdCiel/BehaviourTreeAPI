using UnityEngine;

namespace ESGI.BehaviourTrees
{
    /// <summary>
    /// A node that modifies the state of the world
    /// </summary>
    /// <typeparam name="TAgent">The type of agent owning this node</typeparam>
    public abstract class ActionNode<TAgent> : Node<TAgent> where TAgent : MonoBehaviour
    {
        
    }
}