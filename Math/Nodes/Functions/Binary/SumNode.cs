namespace Math.Nodes.Functions.Binary
{
    public class SumNode : IFunctionNode
    {
        public INode RightNode { get; set; }
        public INode LeftNode { get; set; }
        
        public virtual double Evaluate()
        {
            return LeftNode.Evaluate() + RightNode.Evaluate();
        }
    }
}