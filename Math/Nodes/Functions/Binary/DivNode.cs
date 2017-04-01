using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math.Nodes.Values;

namespace Math.Nodes.Functions.Binary
{
    public class DivNode : IBinaryOperationNode
    {
        public INode RightNode { get; set; }
        public INode LeftNode { get; set; }

        public INode Parent { get; set; }

        public decimal Evaluate()
        {
            return Decimal.Divide(LeftNode.Evaluate(), RightNode.Evaluate());
        }


    }
}
