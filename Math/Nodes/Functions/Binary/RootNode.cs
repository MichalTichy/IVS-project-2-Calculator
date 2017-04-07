using System;

namespace Math.Nodes.Functions.Binary
{
    public class RootNode : IBinaryOperationNode
    {
        public INode RightNode { get; set; }
        public INode LeftNode { get; set; }

        public INode Parent { get; set; }

        public decimal Evaluate()
        {
            decimal LeftNodeValue = LeftNode.Evaluate();
            decimal RightNodeValue = RightNode.Evaluate();

            CheckIfItsPossibleToCalculateSqrt(LeftNodeValue, RightNodeValue);

            return (decimal)System.Math.Pow((double)RightNodeValue, 1.0 / (int)LeftNodeValue);
        }

        void CheckIfItsPossibleToCalculateSqrt(decimal LeftValue, decimal RightValue)
        {
            if (RightValue < 0)
            {
                throw new ArgumentException("Number in square root cannot be smaller than zero!");
            }
        }

    }
}
