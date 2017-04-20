using Math.Nodes.Functions.Binary;
using System;

namespace Math.Nodes.Functions.Unary
{
    public class PercentageNode : IFollowingUnaryOperationNode
    {
        public INode Parent { get; set; }

        public INode ChildNode { get; set; }
        public Guid Gid { get; set; }
        public virtual decimal Evaluate()
        {

            if (   Parent is IBinaryOperationNode binaryNode
                   && binaryNode.RightNode==this
                   && (binaryNode is SumNode || binaryNode is SubstractionNode ))
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
        public PercentageNode()
        {
            Gid = Guid.NewGuid();
        }
    }
}