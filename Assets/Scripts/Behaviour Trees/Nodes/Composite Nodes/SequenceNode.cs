using System;
using System.Linq;
using PGSauce.Core.Strings;
using UnityEngine;

namespace ESGI.BehaviourTrees
{
    /// <summary>
    /// And logic gate node. The node succeeds only if all the children succeed;
    /// </summary>
    public class SequenceNode<TAgent> : CompositeNode<TAgent> where TAgent : MonoBehaviour
    {
        protected override NodeState OnUpdate()
        {
            var childRunning = false;
            foreach (var child in Children)
            {
                if (child.State == NodeState.NotExecuted)
                {
                    child.OnBeforeExecute();
                }
                var status = child.Update();
                switch (status)
                {
                    case NodeState.Failure:
                        return NodeState.Failure;
                    case NodeState.Success:
                        continue;
                    case NodeState.Running:
                        childRunning = true;
                        continue;
                    case NodeState.NotExecuted:
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var state = childRunning ? NodeState.Running : NodeState.Success;
            return state;
        }
    }
}