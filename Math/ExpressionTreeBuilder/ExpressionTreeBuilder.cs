using Math.Nodes;
using Math.Nodes.Values;
using Math.Tokenizer;

namespace Math.ExpressionTreeBuilder
{
    public class ExpressionTreeBuilder<T> : IExpressionTreeBuilder where T : ITokenizer, new()
    {
        protected readonly T Tokenizer;
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

                    if (currentNode.FutureType.NodeType.IsBinary() || currentNode.FutureType.NodeType.IsPrecedingUnary())
                    {
                        currentNode = currentNode.GetRightNode();
                    }
                }
            }
            
            return currentNode.GetRoot().Build();
        }
        
    }
}
