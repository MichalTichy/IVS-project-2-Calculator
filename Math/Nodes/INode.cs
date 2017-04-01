namespace Math.Nodes
{
    public interface INode
    {
        INode Parent { get; set; }
        decimal Evaluate();
    }
}