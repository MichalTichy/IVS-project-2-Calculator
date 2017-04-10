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

            int negative = 1;
            if (RightNodeValue < 0)
            {
                negative = -1;
            }

            return ((decimal)System.Math.Pow(System.Math.Abs((double)RightNodeValue), 1.0 / (int)LeftNodeValue)*negative);
        }

        void CheckIfItsPossibleToCalculateSqrt(decimal LeftValue, decimal RightValue)
        {
            if (RightValue < 0 && LeftValue%2 == 0)
            {
                throw new ArgumentException("Number cannot be smaller than zero!");
            }

            if (LeftValue == 0)
            {
                throw new ArgumentException("Root cannot be zero!");
            }
        }

    }
}
