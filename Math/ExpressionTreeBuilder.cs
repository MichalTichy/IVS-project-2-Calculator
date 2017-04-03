using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using Math.Nodes.Functions;
using Math.Nodes.Functions.Binary;
using System.Reflection;
using System.Text.RegularExpressions;
using Math.Nodes;
using Math.Nodes.Functions.Unary;
using Math.Nodes.Values;

namespace Math
{
    public class ExpressionTreeBuilder
    {
        protected SortedDictionary<string,MathOperatorDescription> registeredOperators = new SortedDictionary<string,MathOperatorDescription>();
        public IReadOnlyCollection<MathOperatorDescription> RegisteredOperators => registeredOperators.Values.ToList().AsReadOnly();

        public virtual void RegisterOperator(MathOperatorDescription mathOperatorDescription)
        {
            if (registeredOperators.ContainsKey(mathOperatorDescription.TextRepresentation))
                throw new ArgumentException("Operator with same text representation is allready registered.");

            registeredOperators.Add(mathOperatorDescription.TextRepresentation,mathOperatorDescription);
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

        protected virtual void RegisterDefaultOperators()
        {
            RegisterOperator(new MathOperatorDescription(typeof(SumNode), "+",OperationType.LowPriorityOperation));
            RegisterOperator(new MathOperatorDescription(typeof(SubstractionNode),"-",OperationType.LowPriorityOperation));
            RegisterOperator(new MathOperatorDescription(typeof(MultiplyNode),"*",OperationType.HighPriorityOperation));
            RegisterOperator(new MathOperatorDescription(typeof(DivisionNode),"/",OperationType.HighPriorityOperation));
            RegisterOperator(new MathOperatorDescription(typeof(FactorialNode),"!",OperationType.FunctionCalls));
            RegisterOperator(new MathOperatorDescription(typeof(PercentageNode),"%",OperationType.FunctionCalls));
            RegisterOperator(new MathOperatorDescription(typeof(PowNode),"^",OperationType.FunctionCalls));
        }

        public virtual INode ParseExpression(string expression)
        {
            TemporaryNode currentNode = new TemporaryNode();

            var tokens = SplitExpressionToTokens(expression);
            foreach (var token in tokens)
            {
                if (token == "(")
                {
                    currentNode = currentNode.GetLeftNode();
                }
                else if (token == ")")
                {
                    currentNode = currentNode.GetParentNode();
                }
                else if (CheckIfIsTextIsValidNumber(token))
                {
                    currentNode.Value = decimal.Parse(token);
                    currentNode = currentNode.GetParentNode();
                }
                else
                {
                    var operatorDescription = registeredOperators[token];

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

                    if ((typeof(IBinaryOperationNode).IsAssignableFrom(currentNode.FutureType.NodeType)))
                    {
                        currentNode = currentNode.GetRightNode();
                    }
                }
            }

            if (currentNode.Value==null && currentNode.FutureType==null)
            {
                var toDelete = currentNode;
                currentNode = currentNode.GetParentNode();
                toDelete.UnreferenceFromParent();
            }

            return currentNode.GetRoot().Build();
        }
        
        
        protected ICollection<string> SplitExpressionToTokens(string expression)
        {
            var tokens = new List<string> {expression.Replace(" ", "").ToLower()};

            for (var i = 0; i < tokens.Count; i++)
            {
                string token = tokens[i];
                if (registeredOperators.ContainsKey(token) || token=="(" || token==")")
                    continue;

                var matchingOperator = registeredOperators.Keys.FirstOrDefault(t => token.Contains(t));

                if (matchingOperator == null)
                {
                    if (token.Contains("(")) matchingOperator = "(";
                    else if (token.Contains(")")) matchingOperator = ")";
                }

                if (matchingOperator == null && !CheckIfIsTextIsValidNumber(token))
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

        protected ICollection<string> SeparateOperatorFromText(string text, string operatorTextRepresentation)
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

        protected bool CheckIfIsTextIsValidNumber(string text)
        {
            //TODO rewrite to REGEX
            return decimal.TryParse(text, out var result);
        }
    }
}
