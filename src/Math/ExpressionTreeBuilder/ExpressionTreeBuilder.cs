using Math.Nodes;
using Math.Nodes.Values;
using Math.Tokenizer;

namespace Math.ExpressionTreeBuilder
{
    /// <summary>
    /// Builds expression tree using given tokenizer.
    /// </summary>
    /// <typeparam name="T">Tokenizer used to parse expression</typeparam>
    public class ExpressionTreeBuilder<T> : IExpressionTreeBuilder where T : ITokenizer, new()
    {
        /// <summary>
        /// Tokenizer used to parse given expressions.
        /// </summary>
        protected readonly T Tokenizer;

        /// <summary>
        /// Initializes new expression tree builder 
        /// </summary>
        public ExpressionTreeBuilder()
        {
            Tokenizer = new T();
        }

        /// <summary>
        /// Initializes new expression tree builder by using given tokenizer.
        /// </summary>
        /// <param name="tokenizer">Tokenizer used to parse math expressions.</param>
        public ExpressionTreeBuilder(T tokenizer)
        {
            Tokenizer = tokenizer;
        }

        /// <summary>
        /// Creates node tree from given expression
        /// </summary>
        /// <param name="expression"> Math expression to be parsed. </param>
        /// <returns> Tree composed from nodes. </returns>
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
                    else if (currentNode.FutureType.OperationPriority >= operatorDescription.OperationPriority)
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