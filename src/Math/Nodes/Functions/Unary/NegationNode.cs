using System;

namespace Math.Nodes.Functions.Unary
{
    /// <summary>
    /// Node used to negate value.
    /// </summary>
    public class NegationNode : IPrecedingUnaryOperationNode
    {
        /// <inheritdoc />
        public INode Parent { get; set; }

        /// <inheritdoc />
        public INode ChildNode { get; set; }

        /// <inheritdoc />
        public decimal Evaluate()
        {
            return ChildNode.Evaluate() * -1;
        }
    }
}