using System;

namespace Math.Nodes.Functions.Binary
{
    public class SumNode : IBinaryOperationNode
    {
        public INode RightNode { get; set; }
        public INode LeftNode { get; set; }

        public INode Parent { get; set; }

        public virtual decimal Evaluate()
        {
            return Decimal.Add(LeftNode.Evaluate(), RightNode.Evaluate());
        }
    }
}