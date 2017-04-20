using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Nodes.Functions.Unary
{
    public class SinNode : IPrecedingUnaryOperationNode
    {
        public INode Parent { get; set; }
        public INode ChildNode { get; set; }
        public Guid Gid { get; set; }
        public decimal Evaluate()
        {
            decimal NodeValue = ChildNode.Evaluate();
            double sin = System.Math.Sin((double)NodeValue * (System.Math.PI / 180.0));

            return System.Math.Round((decimal)sin, 14, MidpointRounding.AwayFromZero);
        }
        public SinNode()
        {
            Gid = Guid.NewGuid();
        }
    }
}
