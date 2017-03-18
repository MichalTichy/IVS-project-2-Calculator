namespace Math.Nodes.Functions.Binary
{
    public class SumNode : IBinaryOperationNode
    {
        public INode RightNode { get; set; }
        public INode LeftNode { get; set; }
        
        public virtual decimal Evaluate()
        {
            return LeftNode.Evaluate() + RightNode.Evaluate();
        }
    }
}