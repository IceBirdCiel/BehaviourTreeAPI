using System;
using Sirenix.OdinInspector;

namespace ESGI.BehaviourTrees
{
    /// <summary>
    /// The generic version of <see cref="NodeVariableBase"/> (NodeVariableBase)
    /// </summary>
    /// <typeparam name="T">The type of variable (bool, int, string etc.)</typeparam>
    public class NodeVariable<T> : NodeVariableBase
    {
        [ShowInInspector, ReadOnly]
        public T Value { get; set; }
    }
}