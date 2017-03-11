namespace Math.Nodes.Binary
{
    public class SumNode : IBinaryOperationNode
    {
        public INode RightNode { get; set; }
        public INode LeftNode { get; set; }

        public virtual string TextRepresentation => "+";
        
        public virtual double Evaluate()
        {
            return LeftNode.Evaluate() + RightNode.Evaluate();
        }
    }
}