using Math.Nodes;

namespace Math
{
    public interface IExpressionTreeBuilder
    {
        INode ParseExpression(string expression);
    }
}