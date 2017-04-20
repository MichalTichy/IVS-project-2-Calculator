using System;

namespace Math.Nodes.Functions.Unary
{
    public class SqrtNode : IPrecedingUnaryOperationNode
    {
        public INode Parent { get; set; }
        public INode ChildNode { get; set; }

        public Guid Gid { get; set; }
        public decimal Evaluate()
        {
            var NodeValue = ChildNode.Evaluate();

            CheckIfItsPossibleToCalculateSqrt(NodeValue);

            return (decimal) System.Math.Sqrt((double) NodeValue);
        }

        void CheckIfItsPossibleToCalculateSqrt(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Number in square root cannot be smaller than zero!");
            }
        }
        public SqrtNode()
        {
            Gid = Guid.NewGuid();
        }

    }
}
