using System;

namespace Math.Nodes.Functions.Unary
{
    public class NegationNode : IPrecedingUnaryOperationNode
    {
        public INode Parent { get; set; }
        public INode ChildNode { get; set; }
        public Guid Gid { get; set; }

        public decimal Evaluate()
        {
            return ChildNode.Evaluate() * -1;
        }
        public NegationNode()
        {
            Gid = Guid.NewGuid();
        }
    }
}