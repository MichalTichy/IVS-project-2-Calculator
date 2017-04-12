using System;
using System.Collections.Generic;
using System.Linq;
using Math.Nodes;
using Math.Nodes.Functions;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Functions.Unary;
using Math.Nodes.Values;

namespace Math
{
    public class Tokenizer : ITokenizer
    {
        public IReadOnlyCollection<MathOperatorDescription> RegisteredOperators => registeredOperators.AsReadOnly();

        private List<MathOperatorDescription> registeredOperators = new List<MathOperatorDescription>();

        public Tokenizer()
        {
            RegisterDefaultOperators();
        }
        
        public Tokenizer(ICollection<MathOperatorDescription> operators)
        {
            foreach (var mathOperatorDescription in operators)
                RegisterOperator(mathOperatorDescription);
        }

        protected void RegisterDefaultOperators()
        {
            RegisterOperator(new MathOperatorDescription(typeof(SumNode), "+", OperationType.LowPriorityOperation));
            RegisterOperator(new MathOperatorDescription(typeof(SubstractionNode), "-", OperationType.LowPriorityOperation));
            RegisterOperator(new MathOperatorDescription(typeof(NegationNode), "-", OperationType.FunctionCalls));
            RegisterOperator(new MathOperatorDescription(typeof(MultiplyNode), "*", OperationType.HighPriorityOperation));
            RegisterOperator(new MathOperatorDescription(typeof(DivisionNode), "/", OperationType.HighPriorityOperation));
            RegisterOperator(new MathOperatorDescription(typeof(FactorialNode), "!", OperationType.FunctionCalls));
            RegisterOperator(new MathOperatorDescription(typeof(PercentageNode), "%", OperationType.FunctionCalls));
            RegisterOperator(new MathOperatorDescription(typeof(PowNode), "^", OperationType.FunctionCalls));
            RegisterOperator(new MathOperatorDescription(typeof(RootNode), "sqrt", OperationType.FunctionCalls)); 
            RegisterOperator(new MathOperatorDescription(typeof(SqrtNode),"sqrt",OperationType.FunctionCalls)); 
            RegisterOperator(new MathOperatorDescription(typeof(LogNode), "log", OperationType.FunctionCalls));
            RegisterOperator(new MathOperatorDescription(typeof(GammaNode), "Γ", OperationType.FunctionCalls));
            RegisterOperator(new MathOperatorDescription(typeof(SinNode), "sin", OperationType.FunctionCalls));
            RegisterOperator(new MathOperatorDescription(typeof(CosNode), "cos", OperationType.FunctionCalls));
            RegisterOperator(new MathOperatorDescription(typeof(TanNode), "tan", OperationType.FunctionCalls));
            RegisterOperator(new MathOperatorDescription(typeof(CotgNode), "cotg", OperationType.FunctionCalls));

        }

        public void RegisterOperator(MathOperatorDescription operatorDescription)
        {
            if (registeredOperators.Any(t => t.Equals(operatorDescription)))
                throw new ArgumentException(
                    $"{nameof(MathOperatorDescription)} with same identifier is allready defined.");


            registeredOperators.Add(operatorDescription);
        }
        

        public virtual ICollection<MathOperatorDescription> GetPossibleNextMathOperators(ExpressionPartTypes? previousExpressionPart)
        {
            switch (previousExpressionPart)
            {
                case ExpressionPartTypes.Number:
                case ExpressionPartTypes.UnaryFollowing:
                case ExpressionPartTypes.RightParentheses:
                    return RegisteredOperators.Where(t => t.NodeType.isFollowingUnary() || t.NodeType.isBinary()).ToList();

                case ExpressionPartTypes.LeftParentheses:
                case ExpressionPartTypes.UnaryPreceding:
                case ExpressionPartTypes.Binary:
                case null:
                    return RegisteredOperators.Where(t => t.NodeType.isPrecedingUnary()).ToList();

                default:
                    throw new ArgumentOutOfRangeException(nameof(previousExpressionPart), previousExpressionPart, null);
            }
        }

        public virtual ICollection<(string token, MathOperatorDescription operatorDescription)> AssignOperatorDescriptionToTokens(ICollection<string> tokens)
        {

            var expressionTokens = new List<(string token, MathOperatorDescription operatorDescription)>();

            foreach (var token in tokens)
            {
                MathOperatorDescription description = null;
                var matchingOperators = registeredOperators.Where(t => t.TextRepresentation == token).ToArray();

                if (!matchingOperators.Any())
                {
                    expressionTokens.Add((token, (MathOperatorDescription)null));
                    continue;
                }

                var precedingExpressionPartType = GetPrecedingExpressionPartType(expressionTokens.Last());

                description = GetMathOperatorThatMatchesTokenTheBest(matchingOperators, precedingExpressionPartType);

                if (description == null)
                    throw new ArgumentException("No matching operator found!");

                expressionTokens.Add((token, description));
            }

            return expressionTokens;
        }

        public static ExpressionPartTypes? GetPrecedingExpressionPartType((string token, MathOperatorDescription operatorDescription)? lastExpressionToken)
        {
            ExpressionPartTypes? precedingExpressionPartType;
            if (!lastExpressionToken.HasValue)
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

        protected virtual MathOperatorDescription GetMathOperatorThatMatchesTokenTheBest(ICollection<MathOperatorDescription> possibleDescriptions, ExpressionPartTypes? previousExpressionPart)
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

        public virtual ICollection<string> SplitExpressionToTokens(string expression)
        {
            var tokens = new List<string> { expression.Replace(" ", "").ToLower() };

            for (var i = 0; i < tokens.Count; i++)
            {
                string token = tokens[i];

                var matchingOperator = registeredOperators.FirstOrDefault(t => token.Contains(t.TextRepresentation))?.TextRepresentation;

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
        protected virtual ICollection<string> SeparateOperatorFromText(string text, string operatorTextRepresentation)
        {
            var indexOfOperatorOccurrence = text.IndexOf(operatorTextRepresentation, StringComparison.Ordinal);

            if (indexOfOperatorOccurrence == -1)
                return new[] { text };

            var textParts = new List<string>();
            var precedingText = new string(text.Take(indexOfOperatorOccurrence).ToArray());
            var followingText = new string(text.Skip(indexOfOperatorOccurrence + operatorTextRepresentation.Length).ToArray());

            if (!string.IsNullOrWhiteSpace(precedingText))
                textParts.Add(precedingText);

            textParts.Add(operatorTextRepresentation);

            if (!string.IsNullOrWhiteSpace(followingText))
                textParts.Add(followingText);

            return textParts;
        }
    }
}