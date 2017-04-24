using System;

namespace Math.Nodes.Functions.Unary
{
    /// <summary>
    /// Node that has only one child
    /// </summary>
    public interface IUnaryOperationNode : IFunctionNode
    {
        /// <summary>
        /// Child node
        /// </summary>
        INode ChildNode { get; set; }
    }
}