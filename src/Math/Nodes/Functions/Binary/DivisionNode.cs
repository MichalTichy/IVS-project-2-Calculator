using System;

namespace Math.Nodes.Functions.Binary
{
    /// <summary>
    /// Node used to calculate division.
    /// </summary>
    public class DivisionNode : IBinaryOperationNode
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
            return decimal.Divide(LeftNode.Evaluate(), RightNode.Evaluate());
        }
    }
}