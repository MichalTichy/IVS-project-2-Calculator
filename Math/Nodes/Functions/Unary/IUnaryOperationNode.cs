namespace Math.Nodes.Functions.Unary
{
    public interface IUnaryOperationNode : INode
    {
        INode ChildNode { get; set; }
    }
}