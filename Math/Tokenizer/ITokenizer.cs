using System.Collections.Generic;

namespace Math
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