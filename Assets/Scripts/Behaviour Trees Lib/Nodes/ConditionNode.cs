using UnityEngine;

namespace ESGI.BehaviourTrees
{
    /// <summary>
    /// Nodes do not modify the state of the world. They are used to check if something is true or not.
    /// They can take multiple frames to return the value if they return NodeState.Running.
    /// </summary>
    /// <typeparam name="TAgent">The type of Agent being controlled by the BT.</typeparam>
    public abstract class ConditionNode<TAgent> : Node<TAgent> where TAgent : MonoBehaviour
    {
        
    }
}