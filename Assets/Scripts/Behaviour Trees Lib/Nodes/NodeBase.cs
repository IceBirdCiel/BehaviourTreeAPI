using UnityEngine;
using System.Collections.Generic;

namespace ESGI.BehaviourTrees
{
    public abstract class NodeBase : ScriptableObject
    {
        public abstract List<NodeBase> _children { get;}
        public abstract NodeState states { get; }
    }
}