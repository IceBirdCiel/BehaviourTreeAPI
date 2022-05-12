using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ESGI.BehaviourTrees
{
    public abstract class BehaviourTreeBase : MonoBehaviour
    {
        public abstract NodeBase root { get; }
        [ShowInInspector]
        public abstract NodeBase CurrentNode { get; }
    }
}