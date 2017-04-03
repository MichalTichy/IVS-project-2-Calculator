using System;

namespace Math.Nodes.Functions.Unary
{
    public interface IUnaryOperationNode : IFunctionNode
    {
        INode ChildNode { get; set; }
    }
}