﻿using System;
using System.Linq;
using UnityEngine;

namespace ESGI.BehaviourTrees
{
    /// <summary>
    /// OR logic gate : the node succeeds if one of the children succeeds
    /// </summary>
    public class SelectorNode<TAgent> : CompositeNode<TAgent> where TAgent : MonoBehaviour
    {
        protected sealed override NodeState OnUpdate()
        {
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
                        continue;
                    case NodeState.Success:
                        return NodeState.Success;
                    case NodeState.Running:
                        return NodeState.Running;
                    case NodeState.NotExecuted:
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return NodeState.Failure;
        }
    }
}