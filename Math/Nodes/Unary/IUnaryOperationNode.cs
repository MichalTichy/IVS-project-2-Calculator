namespace Math.Nodes.Unary
{
    public interface IUnaryOperationNode : INode
    {
        INode ChildNode { get; set; }
    }
}