namespace Math.Nodes
{
    public interface INode
    {
        string TextRepresentation { get; }

        double Evaluate();
    }
}