using System;
using System.Collections.Generic;
using UnityEngine;

namespace ESGI.BehaviourTrees
{
    public abstract class CompositeNode<TAgent> : Node<TAgent> where TAgent : MonoBehaviour
    {
        protected override void OnExecutionEnd()
        {
            base.OnExecutionEnd();
            foreach (var child in Children)
            {
                child.State = NodeState.NotExecuted;
            }
        }
    }
}