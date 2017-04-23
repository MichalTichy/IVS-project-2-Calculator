namespace Math.Nodes.Functions
{
    /// <summary>
    /// Operation type degfines order of operations.
    /// </summary>
    public enum OperationType
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