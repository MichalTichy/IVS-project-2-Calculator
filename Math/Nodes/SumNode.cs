using Math.Nodes.Binary;

namespace Math.Nodes
{
    public class SumNode : IBinaryOperationNode
    {
        public INode RightNode { get; set; }
        public INode LeftNode { get; set; }

        public string TextRepresentation => "+";
        
        public double Evaluate()
        {
            return LeftNode.Evaluate() + RightNode.Evaluate();
        }
    }
}