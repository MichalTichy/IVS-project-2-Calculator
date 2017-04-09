using System.Collections.Generic;

namespace Math
{
    public interface ITokenizer
    {
        IReadOnlyCollection<MathOperatorDescription> RegisteredOperators { get; }
        void RegisterOperator(MathOperatorDescription operatorDescription);
        ICollection<(string token, MathOperatorDescription operatorDescription)> AssignOperatorDescriptionToTokens(ICollection<string> tokens);
        ICollection<string> SplitExpressionToTokens(string expression);
    }
}