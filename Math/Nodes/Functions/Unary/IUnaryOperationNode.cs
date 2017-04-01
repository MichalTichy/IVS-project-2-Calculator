using System;

namespace Math.Nodes.Functions.Unary
{
    public interface IUnaryOperationNode : IFunctionNode
    {
        INode ChildNode { get; set; }
    }

    public class FactorialNode : IUnaryOperationNode
    {
        public INode Parent { get; set; }
        public INode ChildNode { get; set; }

        public decimal Evaluate()
        {
            var childNodeValue = ChildNode.Evaluate();

            CheckIfItsPossibleToCalculateFactorial(childNodeValue);

            decimal factorial = 1;
            for (decimal i = 1; i <= childNodeValue; i++)
            {
                factorial *= i;
            }

            return factorial;
        }

        void CheckIfItsPossibleToCalculateFactorial(decimal value)
        {
            if (value%1>0)
            {
                throw new ArithmeticException("It`s not possible to calculate Factorial of decimal number.");
            }

            if (value>27)
            {
                throw new ArithmeticException("Cannot calculate factorial of bigger number than 27/");
            }

            if (value<0)
            {
                throw new ArgumentException("Cannot calculate factorial of negative number");
            }
        }

    }
}