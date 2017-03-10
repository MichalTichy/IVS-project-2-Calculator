namespace Math
{
    public interface IUnaryOperationNode : INode
    {
        INode ChildNode { get; set; }
    }
}