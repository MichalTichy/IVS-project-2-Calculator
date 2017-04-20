using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Nodes.Functions.Unary
{
    public class CosNode : IPrecedingUnaryOperationNode
    {
        public INode Parent { get; set; }
        public INode ChildNode { get; set; }

        public decimal Evaluate()
        {
            decimal NodeValue = ChildNode.Evaluate() ;
            double cos = System.Math.Cos((double)NodeValue * (System.Math.PI / 180.0));

            return System.Math.Round((decimal)cos, 14, MidpointRounding.AwayFromZero);
        }

    }
}
