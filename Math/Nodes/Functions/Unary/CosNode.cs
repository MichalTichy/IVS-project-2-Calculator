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
        public Guid Gid { get; set; }
        public decimal Evaluate()
        {
            decimal NodeValue = ChildNode.Evaluate() ;
            double cos = System.Math.Cos((double)NodeValue * (System.Math.PI / 180.0));

            return (decimal)cos;
        }
        public CosNode()
        {
            Gid = Guid.NewGuid();
        }

    }
}
