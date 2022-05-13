namespace ESGI.BehaviourTrees
{
    /// <summary>
    /// The tree and the nodes can be in one of these states
    /// </summary>
    public enum NodeState
    {
        /// <summary>
        /// Node is in idle
        /// </summary>
        NotExecuted = 0,
        /// <summary>
        /// Execution has failed
        /// </summary>
        Failure = 1,
        /// <summary>
        /// Execution has succeeded
        /// </summary>
        Success = 2,
        /// <summary>
        /// Execution is going on
        /// </summary>
        Running = 3
    }
}