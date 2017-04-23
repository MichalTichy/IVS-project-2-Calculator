using System;
using System.Collections.Generic;
using Math.ExpressionTreeBuilder;

namespace Math.Tokenizer
{
    /// <summary>
    /// Parses math expression to individual tokens.
    /// </summary>
    public interface ITokenizer
    {
        /// <summary>
        /// Collection of all currently registered mathematical operations.
        /// </summary>
        IReadOnlyCollection<MathOperatorDescription> RegisteredOperators { get; }

        /// <summary>
        /// Assigns string tokens to matching operator descriptions.
        /// </summary>
        /// <param name="tokens">Collection of expression tokens.</param>
        /// <returns>Collection of token and its operator description.</returns>
        ICollection<(string token, MathOperatorDescription operatorDescription)> AssignOperatorDescriptionToTokens(ICollection<string> tokens);

        /// <summary>
        /// Gets all next possible math operators.
        /// </summary>
        /// <param name="previousExpressionPart">Type of preceding token.</param>
        /// <returns>Collection of possible math operators.</returns>
        ICollection<MathOperatorDescription> GetPossibleNextMathOperators(ExpressionPartTypes? previousExpressionPart);

        /// <summary>
        /// Registers additional math operator.
        /// </summary>
        /// <param name="operatorDescription">operator description</param>
        void RegisterOperator(MathOperatorDescription operatorDescription);

        /// <summary>
        /// Splits expression to individual tokens.
        /// </summary>
        /// <param name="expression">Mathematical expression. </param>
        /// <returns>Collection of tokens.</returns>
        ICollection<string> SplitExpressionToTokens(string expression);
    }
}