namespace Math.Nodes.Functions
{
    /// <summary>
    /// defines order of operations.
    /// </summary>
    public enum OperationPriority
    {
        /// <summary>
        /// subtractions, summations, ...
        /// </summary>
        LowPriorityOperation,

        /// <summary>
        /// multiplications, divisions,...
        /// </summary>
        HighPriorityOperation,

        /// <summary>
        /// sqrt, factorial ...
        /// </summary>
        FunctionCalls
    }
}