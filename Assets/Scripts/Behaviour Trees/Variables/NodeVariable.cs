using System;

namespace ESGI.BehaviourTrees
{
    /// <summary>
    /// The generic version of <see cref="NodeVariableBase"/> (NodeVariableBase)
    /// </summary>
    /// <typeparam name="T">The type of variable (bool, int, string etc.)</typeparam>
    public class NodeVariable<T> : NodeVariableBase
    {
        public T Value { get; set; }
    }
}