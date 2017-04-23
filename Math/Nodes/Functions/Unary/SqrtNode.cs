using System;

namespace Math.Nodes.Functions.Unary
{
    /// <summary>
    /// Node used to calculate square root.
    /// </summary>
    public class SqrtNode : IPrecedingUnaryOperationNode
    {
        /// <inheritdoc />
        public INode Parent { get; set; }

        /// <inheritdoc />
        public INode ChildNode { get; set; }

        /// <inheritdoc />
        public decimal Evaluate()
        {
            var nodeValue = ChildNode.Evaluate();

            CheckIfItsPossibleToCalculateSqrt(nodeValue);

            return (decimal) System.Math.Sqrt((double) nodeValue);
        }

        void CheckIfItsPossibleToCalculateSqrt(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("Number in square root cannot be smaller than zero!");
        }
    }
}
