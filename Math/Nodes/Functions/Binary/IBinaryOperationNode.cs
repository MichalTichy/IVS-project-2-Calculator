namespace Math.Nodes.Functions.Binary
{
    /// <summary>
    /// Function node that has two children.
    /// </summary>
    public interface IBinaryOperationNode : IFunctionNode
    {
        /// <summary>
        /// Right child node
        /// </summary>
        INode RightNode { get; set; }

        /// <summary>
        /// Left child node
        /// </summary>
        INode LeftNode { get; set; }
    }
}