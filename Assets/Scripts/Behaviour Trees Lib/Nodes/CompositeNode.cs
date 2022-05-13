using System;
using System.Collections.Generic;
using UnityEngine;

namespace ESGI.BehaviourTrees
{
    /// <summary>
    /// Sequences and Selectors inherits this class.
    /// </summary>
    /// <typeparam name="TAgent">The type of Agent being controlled by the BT.</typeparam>
    public abstract class CompositeNode<TAgent> : Node<TAgent> where TAgent : MonoBehaviour
    {
    }
}