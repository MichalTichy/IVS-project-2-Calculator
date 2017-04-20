using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Nodes.Functions.Unary
{
    public class TanNode : IPrecedingUnaryOperationNode
    {
        public INode Parent { get; set; }
        public INode ChildNode { get; set; }
        public Guid Gid { get; set; }

        public decimal Evaluate()
        {
            decimal NodeValue = ChildNode.Evaluate();
            double tan = System.Math.Tan((double)NodeValue * (System.Math.PI / 180.0));

            return System.Math.Round((decimal)tan, 14, MidpointRounding.AwayFromZero);
        }

        public TanNode()
        {
            Gid = Guid.NewGuid();
        }

    }
}
