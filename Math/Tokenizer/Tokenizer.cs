using System;
using System.Collections.Generic;
using System.Linq;
using Math.ExpressionTreeBuilder;
using Math.Nodes.Functions;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Functions.Unary;
using Math.Nodes.Values;

namespace Math.Tokenizer
{
    /// <summary>
    /// Parses math expression to individual tokens.
    /// </summary>
    public class Tokenizer : ITokenizer
    {
        /// <inheritdoc />
        public IReadOnlyCollection<MathOperatorDescription> RegisteredOperators => registeredOperators.AsReadOnly();

        private List<MathOperatorDescription> registeredOperators = new List<MathOperatorDescription>();

        /// <summary>
        /// Initializes new tokenizer class and registers default operators.
        /// </summary>
        public Tokenizer()
        {
            RegisterDefaultOperators();
        }

        /// <summary>
        /// Initializes new tokenizer and registers given operators.
        /// Default operators are not registered.
        /// </summary>
        /// <param name="operators">Math operator descriptions to be registered.</param>
        public Tokenizer(ICollection<MathOperatorDescription> operators)
        {
            foreach (var mathOperatorDescription in operators)
                RegisterOperator(mathOperatorDescription);
        }

        /// <summary>
        /// Registers default operators.
        /// </summary>
        protected void RegisterDefaultOperators()
        {
            RegisterOperator(new MathOperatorDescription(typeof(SumNode), "+", OperationPriority.LowPriorityOperation,OperationCategory.Basic));
            RegisterOperator(new MathOperatorDescription(typeof(SubstractionNode), "-",OperationPriority.LowPriorityOperation,OperationCategory.Basic));
            RegisterOperator(new MathOperatorDescription(typeof(NegationNode), "-", OperationPriority.FunctionCalls,OperationCategory.Special));
            RegisterOperator(new MathOperatorDescription(typeof(MultiplyNode), "*", OperationPriority.HighPriorityOperation,OperationCategory.Basic));
            RegisterOperator(new MathOperatorDescription(typeof(DivisionNode), "/", OperationPriority.HighPriorityOperation,OperationCategory.Basic));
            RegisterOperator(new MathOperatorDescription(typeof(FactorialNode), "!", OperationPriority.FunctionCalls,OperationCategory.Special));
            RegisterOperator(new MathOperatorDescription(typeof(PercentageNode), "%", OperationPriority.FunctionCalls,OperationCategory.Special));
            RegisterOperator(new MathOperatorDescription(typeof(PowNode), "^", OperationPriority.FunctionCalls,OperationCategory.Basic));
            RegisterOperator(new MathOperatorDescription(typeof(RootNode), "sqrt", OperationPriority.FunctionCalls,OperationCategory.Basic));
            RegisterOperator(new MathOperatorDescription(typeof(SqrtNode), "sqrt", OperationPriority.FunctionCalls,OperationCategory.Basic));
            RegisterOperator(new MathOperatorDescription(typeof(LogNode), "log", OperationPriority.FunctionCalls,OperationCategory.Special));
            RegisterOperator(new MathOperatorDescription(typeof(GammaNode), "Γ", OperationPriority.FunctionCalls,OperationCategory.Special));
            RegisterOperator(new MathOperatorDescription(typeof(SinNode), "sin", OperationPriority.FunctionCalls,OperationCategory.Goniometric));
            RegisterOperator(new MathOperatorDescription(typeof(CosNode), "cos", OperationPriority.FunctionCalls,OperationCategory.Goniometric));
            RegisterOperator(new MathOperatorDescription(typeof(TanNode), "tan", OperationPriority.FunctionCalls,OperationCategory.Goniometric));
            RegisterOperator(new MathOperatorDescription(typeof(CotgNode), "cotg", OperationPriority.FunctionCalls, OperationCategory.Goniometric));
        }


        /// <summary>
        /// Registers given operator.
        /// </summary>
        /// <param name="operatorDescription">description of operator</param>
        /// <exception cref="ArgumentException">Throws argument exception when the same operator is allready registered.</exception>
        public void RegisterOperator(MathOperatorDescription operatorDescription)
        {
            if (registeredOperators.Any(t => t.Equals(operatorDescription)))
                throw new ArgumentException(
                    $"{nameof(MathOperatorDescription)} with same identifier is allready defined.");


            registeredOperators.Add(operatorDescription);
        }


        /// <inheritdoc />
        public virtual ICollection<MathOperatorDescription> GetPossibleNextMathOperators(
            ExpressionPartTypes? previousExpressionPart)
        {
            switch (previousExpressionPart)
            {
                case ExpressionPartTypes.Number:
                case ExpressionPartTypes.UnaryFollowing:
                case ExpressionPartTypes.RightParentheses:
                    return
                        RegisteredOperators.Where(t => t.NodeType.IsFollowingUnary() || t.NodeType.IsBinary()).ToList();

                case ExpressionPartTypes.LeftParentheses:
                case ExpressionPartTypes.UnaryPreceding:
                case ExpressionPartTypes.Binary:
                case null:
                    return RegisteredOperators.Where(t => t.NodeType.IsPrecedingUnary()).ToList();

                default:
                    throw new ArgumentOutOfRangeException(nameof(previousExpressionPart), previousExpressionPart, null);
            }
        }

        /// <inheritdoc />
        public virtual ICollection<(string token, MathOperatorDescription operatorDescription)>
            AssignOperatorDescriptionToTokens(ICollection<string> tokens)
        {
            var expressionTokens = new List<(string token, MathOperatorDescription operatorDescription)>();

            foreach (var token in tokens)
            {
                MathOperatorDescription description = null;
                var matchingOperators = registeredOperators.Where(t => t.TextRepresentation == token).ToArray();

                if (!matchingOperators.Any())
                {
                    expressionTokens.Add((token, (MathOperatorDescription) null));
                    continue;
                }

                var precedingExpressionPartType = GetPrecedingExpressionPartType(expressionTokens.LastOrDefault());

                description = GetMathOperatorThatMatchesTokenTheBest(matchingOperators, precedingExpressionPartType);

                if (description == null)
                    throw new ArgumentException("No matching operator found!");

                expressionTokens.Add((token, description));
            }

            return expressionTokens;
        }

        /// <summary>
        /// Gets type of preceding token.
        /// </summary>
        /// <param name="lastExpressionToken">token to analyze</param>
        /// <returns>Type of preceding token.</returns>
        public static ExpressionPartTypes? GetPrecedingExpressionPartType(
            (string token, MathOperatorDescription operatorDescription)? lastExpressionToken)
        {
            ExpressionPartTypes? precedingExpressionPartType;
            if (lastExpressionToken == null || (lastExpressionToken.Value.Item1==null && lastExpressionToken.Value.Item2 == null))
                precedingExpressionPartType = null;
            else if (lastExpressionToken.Value.token == "(")
                precedingExpressionPartType = ExpressionPartTypes.LeftParentheses;
            else if (lastExpressionToken.Value.token == ")")
                precedingExpressionPartType = ExpressionPartTypes.RightParentheses;
            else if (NumberNode.IsNumber(lastExpressionToken.Value.token))
                precedingExpressionPartType = ExpressionPartTypes.Number;
            else
                precedingExpressionPartType = lastExpressionToken.Value.operatorDescription.NodeType.ToExpressionPart();
            return precedingExpressionPartType;
        }

        /// <summary>
        /// Picks most suiting operator from collection of possible operators.
        /// </summary>
        /// <param name="possibleDescriptions">Collection of possible operators.</param>
        /// <param name="previousExpressionPart">Preceding expression part.</param>
        /// <returns>Best matching operator description.</returns>
        /// <exception cref="ArgumentException">Throws when no or multiple matching operators were found.</exception>
        protected virtual MathOperatorDescription GetMathOperatorThatMatchesTokenTheBest(
            ICollection<MathOperatorDescription> possibleDescriptions, ExpressionPartTypes? previousExpressionPart)
        {
            if (possibleDescriptions.Count == 1)
                return possibleDescriptions.First();

            var possibleOperators = GetPossibleNextMathOperators(previousExpressionPart);
            var results = possibleDescriptions.Intersect(possibleOperators).ToArray();

            if (!results.Any())
                throw new ArgumentException("No matching operator found!");

            if (results.Count() > 1)
                throw new ArgumentException("Found multiple possible operators!");

            return results.First();
        }

        /// <inheritdoc />
        public virtual ICollection<string> SplitExpressionToTokens(string expression)
        {
            var tokens = new List<string> {expression.Replace(" ", "").ToLower()};

            for (var i = 0; i < tokens.Count; i++)
            {
                string token = tokens[i];

                var matchingOperator =
                    registeredOperators.FirstOrDefault(t => token.Contains(t.TextRepresentation))?.TextRepresentation;

                if (matchingOperator == null)
                {
                    if (token.Contains("(")) matchingOperator = "(";
                    else if (token.Contains(")")) matchingOperator = ")";
                }

                if (matchingOperator == token)
                    continue;

                if (matchingOperator == null && !NumberNode.IsNumber(token))
                    throw new ArgumentException($"Given expression is not valid. InvalidPartOfExpression: {token}");

                if (matchingOperator != null)
                {
                    var separatedOperatorFromSoroundingText = SeparateOperatorFromText(token, matchingOperator);

                    var indexOfToken = tokens.IndexOf(token);
                    tokens.RemoveAt(indexOfToken);
                    tokens.InsertRange(indexOfToken, separatedOperatorFromSoroundingText);
                    i--;
                }
            }

            return tokens;
        }

        /// <summary>
        /// Pulls operator from text.
        /// </summary>
        /// <param name="text">source text</param>
        /// <param name="operatorTextRepresentation">operator to exprect</param>
        /// <returns>Collection with extracted operator and rest of the string.</returns>
        protected virtual ICollection<string> SeparateOperatorFromText(string text, string operatorTextRepresentation)
        {
            var indexOfOperatorOccurrence = text.IndexOf(operatorTextRepresentation, StringComparison.Ordinal);

            if (indexOfOperatorOccurrence == -1)
                return new[] {text};

            var textParts = new List<string>();
            var precedingText = new string(text.Take(indexOfOperatorOccurrence).ToArray());
            var followingText =
                new string(text.Skip(indexOfOperatorOccurrence + operatorTextRepresentation.Length).ToArray());

            if (!string.IsNullOrWhiteSpace(precedingText))
                textParts.Add(precedingText);

            textParts.Add(operatorTextRepresentation);

            if (!string.IsNullOrWhiteSpace(followingText))
                textParts.Add(followingText);

            return textParts;
        }
    }
}