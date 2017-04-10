using System;

namespace Math.Nodes.Functions.Binary
{
    public class PowNode : IBinaryOperationNode
    {
        public INode RightNode { get; set; }
        public INode LeftNode { get; set; }

        public INode Parent { get; set; }

        public decimal Evaluate()
        {
            decimal LeftNodeValue = LeftNode.Evaluate();
            decimal RightNodeValue = RightNode.Evaluate();

            CheckIfItsPossibleToCalculatePow(LeftNodeValue, RightNodeValue);

            int negative = 1;
            if (LeftNodeValue < 0)
            {
                negative = -1;
            }

            return (decimal)System.Math.Pow(System.Math.Abs((double)LeftNodeValue), (double)RightNodeValue)*negative;
        }
        void CheckIfItsPossibleToCalculatePow(decimal LeftValue, decimal RightValue)
        {
            if (LeftValue < 0 && RightValue%1 != 0 && RightValue != 0)
            {
                RightValue = System.Math.Round(Decimal.Divide(1, RightValue), 10, MidpointRounding.AwayFromZero);
                if (RightValue%1 != 0)
                {
                    throw new ArgumentException("ErrorA!");
                }
            }

            if (LeftValue < 0 && RightValue % 2 == 0)
            {
                throw new ArgumentException("ErrorA!");
            }
         

        }
    }
}