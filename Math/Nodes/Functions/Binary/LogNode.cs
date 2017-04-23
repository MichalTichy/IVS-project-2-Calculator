using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Nodes.Functions.Binary
{

    /// <summary>
    ///  Node used to calculate logarithm.
    /// </summary>
    public class LogNode : IBinaryOperationNode
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
            decimal numberNodeValue = RightNode.Evaluate();
            decimal argumentNodeValue = LeftNode.Evaluate();

            CheckIfItsPossibleToCalculateLog(numberNodeValue, argumentNodeValue);

            return (decimal)System.Math.Log((double)numberNodeValue, (double)argumentNodeValue);
        }

        void CheckIfItsPossibleToCalculateLog(decimal numbervalue, decimal argumentValue)
        {
            if (numbervalue < 1 | argumentValue < 1)
                throw new ArgumentException("Value in logarithm have to be bigger than zero.");
        }
    }
}
