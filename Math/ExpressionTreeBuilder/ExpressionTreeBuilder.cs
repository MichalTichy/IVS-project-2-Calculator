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
    public class ExpressionTreeBuilder<T> : IExpressionTreeBuilder where T : ITokenizer, new()
    {
        protected T Tokenizer;
        public ExpressionTreeBuilder()
        {
            Tokenizer = new T();
        }

        public ExpressionTreeBuilder(T tokenizer)
        {
            Tokenizer = tokenizer;
        }

        public virtual INode ParseExpression(string expression)
        {
            TemporaryNode currentNode = new TemporaryNode();

            var tokens = Tokenizer.SplitExpressionToTokens(expression);
            var expressionParts = Tokenizer.AssignOperatorDescriptionToTokens(tokens);

            foreach (var expressionPart in expressionParts)
            {
                if (expressionPart.token == "(")
                {
                    currentNode = currentNode.GetLeftNode();
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
        
    }
}
