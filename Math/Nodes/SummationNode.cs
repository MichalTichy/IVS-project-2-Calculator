namespace Math
{
    public class SummationNode : IBinaryOperationNode
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