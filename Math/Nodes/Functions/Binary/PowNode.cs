using System;

namespace Math.Nodes.Functions.Binary
{
    /// <summary>
    /// Node used to calculate power of
    /// </summary>
    public class PowNode : IBinaryOperationNode
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

            CheckIfItsPossibleToCalculatePow(leftNodeValue, rightNodeValue);

            int negative = 1;
            if (leftNodeValue < 0)
            {
                negative = -1;
            }

            return (decimal) System.Math.Pow(System.Math.Abs((double) leftNodeValue), (double) rightNodeValue) *
                   negative;
        }

        void CheckIfItsPossibleToCalculatePow(decimal LeftValue, decimal RightValue)
        {
            if (LeftValue < 0 && RightValue % 1 != 0 && RightValue != 0)
            {
                RightValue = System.Math.Round(Decimal.Divide(1, RightValue), 10, MidpointRounding.AwayFromZero);
                if (RightValue % 1 != 0 && RightValue % 2 != 0)
                {
                    throw new ArgumentException("Negative number divided by even number cannot be smaller than zero!");
                }
            }

            if (LeftValue < 0 && RightValue % 2 == 0)
            {
                throw new ArgumentException("Negative number divided by even number cannot be smaller than zero!");
            }
        }
    }
}