using System;

namespace Math.Nodes.Functions.Binary
{
    /// <summary>
    /// Node used to calculate sum.
    /// </summary>
    public class SumNode : IBinaryOperationNode
    {
        /// <inheritdoc />
        public INode RightNode { get; set; }

        /// <inheritdoc />
        public INode LeftNode { get; set; }

        /// <inheritdoc />
        public INode Parent { get; set; }

        /// <inheritdoc />
        public virtual decimal Evaluate()
        {
            return decimal.Add(LeftNode.Evaluate(), RightNode.Evaluate());
        }
    }
}