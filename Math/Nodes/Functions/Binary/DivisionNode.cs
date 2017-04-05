using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math.Nodes.Values;

namespace Math.Nodes.Functions.Binary
{
    public class DivisionNode : IBinaryOperationNode
    {
        public INode RightNode { get; set; }
        public INode LeftNode { get; set; }

        public INode Parent { get; set; }

        public decimal Evaluate()
        {
            decimal RightNodeValue = RightNode.Evaluate();
            decimal LeftNodeValue = LeftNode.Evaluate();

            CheckIfItsPossibleToCalculateDivision(RightNodeValue);

            return Decimal.Divide(LeftNode.Evaluate(), RightNode.Evaluate());
        }

        void CheckIfItsPossibleToCalculateDivision (decimal value)
        {
            if (value == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero!");
            }
        }
    }
}
