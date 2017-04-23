using Math.Nodes.Functions.Binary;
using System;

namespace Math.Nodes.Functions.Unary
{
    /// <summary>
    /// Node used to calculate percentage
    /// </summary>
    public class PercentageNode : IFollowingUnaryOperationNode
    {
        /// <inheritdoc />
        public INode Parent { get; set; }

        /// <inheritdoc />
        public INode ChildNode { get; set; }


        /// <inheritdoc />
        public virtual decimal Evaluate()
        {
            if (Parent is IBinaryOperationNode binaryNode
                && binaryNode.RightNode == this
                && (binaryNode is SumNode || binaryNode is SubstractionNode))
            {
                var leftPart = binaryNode.LeftNode.Evaluate();
                return CovertPercentageToFraction(ChildNode.Evaluate()) * leftPart;
            }

            return CovertPercentageToFraction(ChildNode.Evaluate());
        }

        private decimal CovertPercentageToFraction(decimal percentage)
        {
            return percentage / 100;
        }
    }
}