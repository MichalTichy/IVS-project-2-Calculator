using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Nodes.Functions.Binary
{
    public class MultiplyNode : IBinaryOperationNode
    {
        public INode RightNode { get; set; }
        public INode LeftNode { get; set; }

        public decimal Evaluate()
        {
            return LeftNode.Evaluate() * RightNode.Evaluate();
        }
    }
}
