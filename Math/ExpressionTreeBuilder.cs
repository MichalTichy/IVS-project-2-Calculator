using System;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using Math.Nodes.Functions;
using Math.Nodes.Functions.Binary;
using System.Reflection;
using System.Security.Principal;
using System.Text.RegularExpressions;
using Math.Nodes;
using Math.Nodes.Functions.Unary;
using Math.Nodes.Values;

namespace Math
{
    public class ExpressionTreeBuilder
    {
        private List<MathOperatorDescription> registeredOperators = new List<MathOperatorDescription>();
        public IReadOnlyCollection<MathOperatorDescription> RegisteredOperators => registeredOperators.AsReadOnly();

        public void RegisterOperator(MathOperatorDescription mathOperatorDescription)
        {
            if (registeredOperators.Any(t => t.Equals(mathOperatorDescription)))
                throw new ArgumentException(
                    $"{nameof(MathOperatorDescription)} with same identifier is allready defined.");


            registeredOperators.Add(mathOperatorDescription);
        }

        public ExpressionTreeBuilder()
        {
            RegisterDefaultOperators();
        }
        
        public ExpressionTreeBuilder(ICollection<MathOperatorDescription> operators )
        {
            foreach (var mathOperatorDescription in operators)
                RegisterOperator(mathOperatorDescription);
        }

        private void RegisterDefaultOperators()
        {
            RegisterOperator(new MathOperatorDescription(typeof(SumNode), "+",OperationType.LowPriorityOperation));
            RegisterOperator(new MathOperatorDescription(typeof(SubstractionNode),"-",OperationType.LowPriorityOperation));
            RegisterOperator(new MathOperatorDescription(typeof(NegationNode),"-",OperationType.FunctionCalls));
            RegisterOperator(new MathOperatorDescription(typeof(MultiplyNode),"*",OperationType.HighPriorityOperation));
            RegisterOperator(new MathOperatorDescription(typeof(DivisionNode),"/",OperationType.HighPriorityOperation));
            RegisterOperator(new MathOperatorDescription(typeof(FactorialNode),"!",OperationType.FunctionCalls));
            RegisterOperator(new MathOperatorDescription(typeof(PercentageNode),"%",OperationType.FunctionCalls));
            RegisterOperator(new MathOperatorDescription(typeof(PowNode),"^",OperationType.FunctionCalls));
        }

        public INode ParseExpression(string expression)
        {
            TemporaryNode currentNode = new TemporaryNode();
            
            var expressionParts = AssignOperatorDescriptionToTokens(SplitExpressionToTokens(expression));
            foreach (var expressionPart in expressionParts)
            {
                if (expressionPart.token == "(")
                {
                    currentNode = currentNode.GetLeftNode(); //BUG possible bug 5 + 3 + 4 Sqrt(10)
                }
                else if (expressionPart.token == ")")
                {
                    currentNode = currentNode.GetParentNode();
                }
                else if (NumberNode.IsNumber(expressionPart.token))
                {
                    currentNode.Value = decimal.Parse(expressionPart.token);
                    currentNode = currentNode.GetParentNode();
                }
                else
                {
                    var operatorDescription = expressionPart.operatorDescription;

                    if (currentNode.FutureType == null)
                    {
                        currentNode.FutureType = operatorDescription;
                    }
                    else if (currentNode.FutureType.OperationType >= operatorDescription.OperationType)
                    {
                        currentNode = currentNode.InsertToParent(new TemporaryNode() {FutureType = operatorDescription});
                    }
                    else
                    {
                        currentNode = currentNode.InsertToRight(new TemporaryNode() {FutureType = operatorDescription});
                    }

                    if (currentNode.FutureType.NodeType.isBinary() || currentNode.FutureType.NodeType.isPrecedingUnary())
                    {
                        currentNode = currentNode.GetRightNode();
                    }
                }
            }
            
            return currentNode.GetRoot().Build();
        }

        protected ICollection<(string token, MathOperatorDescription operatorDescription)> AssignOperatorDescriptionToTokens(ICollection<string> tokens)
        {
            var expressionTokens=new List<(string token, MathOperatorDescription operatorDescription)>();

            foreach (var token in tokens)
            {
                MathOperatorDescription description = null;
                var matchingOperators = registeredOperators.Where(t => t.TextRepresentation == token).ToArray();

                if (!matchingOperators.Any())
                {
                    expressionTokens.Add((token,(MathOperatorDescription)null));
                    continue;
                }

                var precedingExpressionPartType = GetPrecedingExpressionPartType(expressionTokens);

                description=GetMathOperatorThatMatchesTokenTheBest(matchingOperators, precedingExpressionPartType);

                if (description == null)
                    throw new ArgumentException("No matching operator found!");

                expressionTokens.Add((token,description));
            }

            return expressionTokens;
        }

        private static ExpressionPartTypes? GetPrecedingExpressionPartType(List<ValueTuple<string, MathOperatorDescription>> expressionTokens)
        {
            ExpressionPartTypes? precedingExpressionPartType;
            if (expressionTokens.Count == 0)
                precedingExpressionPartType = null;
            else if (expressionTokens.Last().Item1 == "(" || expressionTokens.Last().Item1 == ")")
                precedingExpressionPartType = ExpressionPartTypes.Parentheses;
            else if (NumberNode.IsNumber(expressionTokens.Last().Item1))
                precedingExpressionPartType = ExpressionPartTypes.Number;
            else
                precedingExpressionPartType = expressionTokens.Last().Item2.NodeType.ToExpressionPart();
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

        public virtual ICollection<MathOperatorDescription> GetPossibleNextMathOperators(ExpressionPartTypes? previousExpressionPart)
        {
            switch (previousExpressionPart)
            {
                case ExpressionPartTypes.Number:
                case ExpressionPartTypes.UnaryFollowing:
                    return RegisteredOperators.Where(t => t.NodeType.isFollowingUnary() || t.NodeType.isBinary()).ToList();

                case ExpressionPartTypes.Parentheses:
                case ExpressionPartTypes.UnaryPreceding:
                case ExpressionPartTypes.Binary:
                case null:
                    return RegisteredOperators.Where(t => t.NodeType.isPrecedingUnary()).ToList();

                default:
                    throw new ArgumentOutOfRangeException(nameof(previousExpressionPart), previousExpressionPart, null);
            }
        }

        private ICollection<string> SplitExpressionToTokens(string expression)
        {
            var tokens = new List<string> {expression.Replace(" ", "").ToLower()};

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

        private ICollection<string> SeparateOperatorFromText(string text, string operatorTextRepresentation)
        {
            var indexOfOperatorOccurrence = text.IndexOf(operatorTextRepresentation, StringComparison.Ordinal);

            if (indexOfOperatorOccurrence == -1)
                return new[] {text};

            var textParts=new List<string>();
            var precedingText =new string(text.Take(indexOfOperatorOccurrence).ToArray());
            var followingText = new string(text.Skip(indexOfOperatorOccurrence + operatorTextRepresentation.Length).ToArray());

            if (!string.IsNullOrWhiteSpace(precedingText))
                textParts.Add(precedingText);

            textParts.Add(operatorTextRepresentation);
            
            if (!string.IsNullOrWhiteSpace(followingText))
                textParts.Add(followingText);

            return textParts;
        }
    }

    public enum ExpressionPartTypes
    {
        Number,
        Parentheses,
        UnaryFollowing,
        UnaryPreceding,
        Binary
    }
}
