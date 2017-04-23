using System;

namespace Math.Nodes.Functions.Binary
{
    /// <summary>
    /// Node used to calculate root
    /// </summary>
    public class RootNode : IBinaryOperationNode
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
            decimal leftNodeValue = LeftNode.Evaluate();
            decimal rightNodeValue = RightNode.Evaluate();

            CheckIfItsPossibleToCalculateSqrt(leftNodeValue, rightNodeValue);

            bool isNegative = rightNodeValue < 0;

            var value = (decimal) System.Math.Pow(System.Math.Abs((double) rightNodeValue), 1.0 / (int) leftNodeValue);

            return isNegative ? value * -1 : value;
        }

        void CheckIfItsPossibleToCalculateSqrt(decimal leftValue, decimal rightValue)
        {
            if (rightValue < 0 && leftValue % 2 == 0)
                throw new ArgumentException("Number cannot be smaller than zero!");

            if (leftValue == 0)
                throw new ArgumentException("Root cannot be zero!");
        }
    }
}