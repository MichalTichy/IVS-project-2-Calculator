using Math.Nodes;

namespace Math.ExpressionTreeBuilder
{
    public interface IExpressionTreeBuilder
    {
        INode ParseExpression(string expression);
    }
}