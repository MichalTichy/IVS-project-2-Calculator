using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Nodes.Functions.Binary
{
    public class LogNode : IBinaryOperationNode
    {
        public INode RightNode { get; set; }
        public INode LeftNode { get; set; }

        public INode Parent { get; set; }

        public decimal Evaluate()
        {
            decimal NumberNodeValue = RightNode.Evaluate();
            decimal ArgumentNodeValue = LeftNode.Evaluate();

            CheckIfItsPossibleToCalculateLog(NumberNodeValue, ArgumentNodeValue);

            return (decimal)System.Math.Log((double)NumberNodeValue, (double)ArgumentNodeValue);
        }

        void CheckIfItsPossibleToCalculateLog(decimal Numbervalue, decimal ArgumentValue)
        {
            if (Numbervalue < 1 | ArgumentValue < 1)
            {
                throw new ArgumentException("Value in logarithm have to be bigger than zero.");
            }
        }
    }
}
