using System;
using System.Numerics;
using Math.Nodes.Values;

namespace Math.Nodes.Functions.Unary
{
    public class FactorialNode : IFollowingUnaryOperationNode
    {
        public INode Parent { get; set; }
        public INode ChildNode { get; set; }

        public decimal Evaluate()
        {
            var childNodeValue = ChildNode.Evaluate();

            CheckIfItsPossibleToCalculateFactorial(childNodeValue);

            if (childNodeValue % 1 == 0)
                return Factorial(childNodeValue);

            var gammaNode = new GammaNode() {ChildNode = new NumberNode(childNodeValue)};

            return gammaNode.Evaluate();
        }

        protected static decimal Factorial(decimal childNodeValue)
        {
            decimal factorial = 1;
            for (decimal i = 1; i <= childNodeValue; i++)
            {
                factorial *= i;
            }

            return factorial;
        }

        void CheckIfItsPossibleToCalculateFactorial(decimal value)
        {

            if (value>27)
            {
                throw new OverflowException("Cannot calculate factorial of bigger number than 27.");
            }

            if (value<0)
            {
                throw new ArgumentException("Cannot calculate factorial of negative number");
            }
        }


    }
}