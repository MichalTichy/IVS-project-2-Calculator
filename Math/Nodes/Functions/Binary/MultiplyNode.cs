using System;

namespace Math.Nodes.Functions.Binary
{
    public class MultiplyNode : IBinaryOperationNode
    {
        public INode RightNode { get; set; }
        public INode LeftNode { get; set; }

        public INode Parent { get; set; }

        public decimal Evaluate()
        {
            return LeftNode.Evaluate() * RightNode.Evaluate();
        }
    }
}
