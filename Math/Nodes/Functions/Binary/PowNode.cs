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
            CheckIfItsPossibleToCalculatePower(RightNodeValue);

            return (decimal)System.Math.Pow((double)LeftNode.Evaluate(), (double)RightNode.Evaluate());
        }

        void CheckIfItsPossibleToCalculatePower(decimal value)
        {
            if (value%1 != 0 || value < 1)
            {
                throw new ArgumentException("Argument is not natural number or is smaller than 1.");
            }
        }
    }
}
