using Math = System.Math;

namespace Math.Nodes.Functions.Binary
{
    public class PowNode : IBinaryOperationNode
    {
        public INode RightNode { get; set; }
        public INode LeftNode { get; set; }

        public INode Parent { get; set; }

        public decimal Evaluate()
        {
            return (decimal)System.Math.Pow((double)LeftNode.Evaluate(), (double)RightNode.Evaluate());
        }
    }
}
