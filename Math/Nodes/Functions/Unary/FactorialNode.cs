using System;
using System.Numerics;
using Math.Nodes.Values;

namespace Math.Nodes.Functions.Unary
{
    /// <summary>
    /// Node used to calculate factorial.
    /// </summary>
    public class FactorialNode : IFollowingUnaryOperationNode
    {
        /// <inheritdoc />
        public INode Parent { get; set; }

        /// <inheritdoc />
        public INode ChildNode { get; set; }

        /// <summary>
        /// Calculates value of node.
        /// Cannot calculate for bigger numbers than 27.
        /// For decimal number is used gamma function. 
        /// </summary>
        /// <returns>Factorial of child node value.</returns>
        public decimal Evaluate()
        {
            var childNodeValue = ChildNode.Evaluate();

            CheckIfItsPossibleToCalculateFactorial(childNodeValue);

            if (childNodeValue % 1 == 0)
                return Factorial(childNodeValue);

            var gammaNode = new GammaNode() {ChildNode = new NumberNode(childNodeValue)};

            return gammaNode.Evaluate();
        }

        /// <summary>
        /// Calculates factorial
        /// </summary>
        /// <param name="childNodeValue">calculates factorial</param>
        /// <returns>factorial of value</returns>
        protected static decimal Factorial(decimal childNodeValue)
        {
            decimal factorial = 1;

            for (decimal i = 1; i <= childNodeValue; i++)
                factorial *= i;

            return factorial;
        }

        void CheckIfItsPossibleToCalculateFactorial(decimal value)
        {
            if (value > 27)
                throw new OverflowException("Cannot calculate factorial of bigger number than 27.");

            if (value < 0)
                throw new ArgumentException("Cannot calculate factorial of negative number");
        }
    }
}