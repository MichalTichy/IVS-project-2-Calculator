using System;

namespace Math.Nodes.Functions.Binary
{
    /// <summary>
    /// Node used to calculate substraction.
    /// </summary>
    public class SubstractionNode : IBinaryOperationNode
    {
        /// <inheritdoc />
        public INode RightNode { get; set; }

        /// <inheritdoc />
        public INode LeftNode { get; set; }

        /// <inheritdoc />
        public INode Parent { get; set; }

        /// <inheritdoc />
        public decimal Evaluate()
        {
            return LeftNode.Evaluate() - RightNode.Evaluate();
        }
    }
}