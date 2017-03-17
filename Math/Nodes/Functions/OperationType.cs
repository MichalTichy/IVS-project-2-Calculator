namespace Math.Nodes.Functions
{
    /// <summary>
    /// FunctionCalls - Function call, scope : sqrt , root , factorial , ..
    /// HighPriorityOperation - multiplication, division, ...
    /// LowPriorityOperation - summation, substraction
    /// </summary>
    public enum OperationType
    {
        FunctionCalls,
        HighPriorityOperation,
        LowPriorityOperation
    }
}
