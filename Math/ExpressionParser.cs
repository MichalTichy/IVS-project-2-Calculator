using System;
using System.Collections.Generic;
using System.Linq;
using Math.Nodes.Functions;
using Math.Nodes.Functions.Binary;
using System.Reflection;
using System.Text.RegularExpressions;
using Math.Nodes;

namespace Math
{
    public class ExpressionParser
    {
        protected SortedDictionary<string,MathOperatorDescription> registeredOperators = new SortedDictionary<string,MathOperatorDescription>();
        public IReadOnlyCollection<MathOperatorDescription> RegisteredOperators => registeredOperators.Values.ToList().AsReadOnly();

        public void RegisterOperator(MathOperatorDescription mathOperatorDescription)
        {
            if (registeredOperators.ContainsKey(mathOperatorDescription.TextRepresentation))
                throw new ArgumentException("Operator with same text representation is allready registered.");

            registeredOperators.Add(mathOperatorDescription.TextRepresentation,mathOperatorDescription);
        }

        public ExpressionParser()
        {
            RegisterDefaultOperators();
        }

        protected void RegisterDefaultOperators()
        {
            RegisterOperator(new MathOperatorDescription(typeof(SumNode), "+",OperationType.LowPriorityOperation));
            RegisterOperator(new MathOperatorDescription(typeof(SubstractionNode),"-",OperationType.LowPriorityOperation));
            RegisterOperator(new MathOperatorDescription(typeof(MultiplyNode),"*",OperationType.HighPriorityOperation));
        }

        public INode ParseExpression(string expression)
        {
            var rootNode=new TemporaryNode();

            return rootNode;
        }

        public ICollection<string> SplitExpressionToTokens(string expression)
        {
            var tokens = new List<string> {expression.Replace(" ", "").ToLower()};

            for (var i = 0; i < tokens.Count; i++)
            {
                string token = tokens[i];
                if (registeredOperators.ContainsKey(token))
                    continue;

                var matchingOperators = registeredOperators.Keys.FirstOrDefault(t => token.Contains(t));

                if (matchingOperators == null && !CheckIfIsTextIsValidNumber(token))
                {
                    throw new ArgumentException($"Given expression is not valid. InvalidPartOfExpression: {token}");
                }
                else if (matchingOperators != null)
                {
                    var separatedOperatorFromSoroundingText = SeparateOperatorFromText(token, matchingOperators);

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

            var precedingText =new string(text.Take(indexOfOperatorOccurrence).ToArray());
            var followingText = new string(text.Skip(indexOfOperatorOccurrence + operatorTextRepresentation.Length).ToArray());
            return new[] {precedingText, operatorTextRepresentation, followingText};
        }

        protected bool CheckIfIsTextIsValidNumber(string text)
        {
            //TODO rewrite to REGEX
            var result=new decimal();
            return decimal.TryParse(text, out result);
        }
    }
}
