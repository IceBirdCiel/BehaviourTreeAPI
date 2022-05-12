using UnityEngine;

namespace ESGI.BehaviourTrees
{
    public abstract class ConditionNode<TAgent> : Node<TAgent>, ConditionNodeBase where TAgent : MonoBehaviour
    {
        
    }
}