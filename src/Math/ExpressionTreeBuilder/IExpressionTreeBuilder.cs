using Math.Nodes;

namespace Math.ExpressionTreeBuilder
{
    /// <summary>
    /// Builds expression tree using given tokenizer.
    /// </summary>
    public interface IExpressionTreeBuilder
    {
        /// <summary>
        /// Creates node tree from given expression
        /// </summary>
        /// <param name="expression"> Math expression to be parsed. </param>
        /// <returns> Tree composed from nodes. </returns>
        INode ParseExpression(string expression);
    }
}