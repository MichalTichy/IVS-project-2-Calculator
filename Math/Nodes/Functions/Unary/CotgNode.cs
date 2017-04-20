using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Nodes.Functions.Unary
{
    public class CotgNode : IPrecedingUnaryOperationNode
    {
        public INode Parent { get; set; }
        public INode ChildNode { get; set; }

        public decimal Evaluate()
        {
            decimal NodeValue = ChildNode.Evaluate();
            double tan = System.Math.Tan((double)NodeValue * (System.Math.PI / 180.0));

            return System.Math.Round((decimal)(1.0 / tan), 14, MidpointRounding.AwayFromZero);
        }

    }
}
