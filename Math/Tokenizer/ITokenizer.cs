using System.Collections.Generic;
using Math.ExpressionTreeBuilder;

namespace Math.Tokenizer
{
    public interface ITokenizer
    {
        IReadOnlyCollection<MathOperatorDescription> RegisteredOperators { get; }

        ICollection<(string token, MathOperatorDescription operatorDescription)> AssignOperatorDescriptionToTokens(ICollection<string> tokens);
        ICollection<MathOperatorDescription> GetPossibleNextMathOperators(ExpressionPartTypes? previousExpressionPart);
        void RegisterOperator(MathOperatorDescription operatorDescription);
        ICollection<string> SplitExpressionToTokens(string expression);
    }
}