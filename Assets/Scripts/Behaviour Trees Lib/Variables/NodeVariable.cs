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
        /// <summary>
        /// The value of the data. It is advised to reset this value before the node execution.
        /// </summary>
        [ShowInInspector, ReadOnly]
        public T Value { get; set; }
        
        /// <summary>
        /// Used to reset the Data.
        /// </summary>
        public void ResetVariable()
        {
            Value = default;
        }
    }
}