using System;

namespace Math.Nodes.Functions.Binary
{
    public class DivisionNode : IBinaryOperationNode
    {
        public INode RightNode { get; set; }
        public INode LeftNode { get; set; }

        public INode Parent { get; set; }
        public Guid Gid { get; set; }

        public decimal Evaluate()
        {
            return Decimal.Divide(LeftNode.Evaluate(), RightNode.Evaluate());
        }

        public DivisionNode()
        {
            Gid = Guid.NewGuid();
        }

    }
}
