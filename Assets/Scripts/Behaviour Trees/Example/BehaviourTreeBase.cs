using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESGI.BehaviourTrees
{
    public abstract class BehaviourTreeBase : MonoBehaviour
    {
        public abstract NodeBase root { get; }
    }
}