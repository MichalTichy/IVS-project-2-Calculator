namespace Math.Nodes.Functions
{
    /// <summary>
    /// LowPriorityOperation - summation, substraction
    /// HighPriorityOperation - multiplication, division, ... 
    /// FunctionCalls - Function call, scope : sqrt , root , factorial , ..
    /// </summary>
    public enum OperationType
    {
        LowPriorityOperation,
        HighPriorityOperation,
        FunctionCalls
    }
}
