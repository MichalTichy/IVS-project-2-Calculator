namespace Math
{
    public interface INode
    {
        string TextRepresentation { get; }

        double Evaluate();
    }
}