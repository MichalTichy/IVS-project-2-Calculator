namespace Math.Nodes.Functions.Binary
{
    public interface IBinaryOperationNode : IFunctionNode
    {
        INode RightNode { get; set; }
        INode LeftNode { get; set; }
    }
}