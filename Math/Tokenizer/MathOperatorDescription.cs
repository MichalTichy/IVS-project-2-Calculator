using System;
using System.Reflection;
using Math.Nodes.Functions;

namespace Math.Tokenizer
{
    /// <summary>
    /// Description of mathematical operation.
    /// </summary>
    public class MathOperatorDescription
    {
        /// <summary>
        /// Text representation of mathematical operator (eq. + , sqrt , ...)
        /// </summary>
        public readonly string TextRepresentation;


        /// <summary>
        /// Type of Node that will be created.
        /// </summary>
        public readonly Type NodeType;

        /// <summary>
        /// Operation type, this determines priority of operations.
        /// </summary>
        public readonly OperationType OperationType;


        /// <summary>
        /// Initializes new operator description.
        /// </summary>
        /// <param name="nodeType"> Type of node that will be created. </param>
        /// <param name="textRepresentation"> Text representation of mathematical operator (eq. + , sqrt , ...) </param>
        /// <param name="operationType"> Type of operation </param>
        /// <exception cref="ArgumentException"> Throws when provided arguments are not correct. </exception>
        public MathOperatorDescription(Type nodeType, string textRepresentation, OperationType operationType)
        {
            if (!nodeType.IsFunctionNode())
                throw new ArgumentException($"{nameof(nodeType)} is not implementation of {nameof(IFunctionNode)}");
            NodeType = nodeType;

            if (string.IsNullOrWhiteSpace(textRepresentation))
                throw new ArgumentException($"{nameof(textRepresentation)} cannot be empty or whitespace.");
            TextRepresentation = textRepresentation;

            OperationType = operationType;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is MathOperatorDescription desc))
                return false;

            if (desc.TextRepresentation != TextRepresentation)
                return false;

            if (NodeType.IsBinary() && desc.NodeType.IsBinary())
                return true;

            if (NodeType.IsUnary() && desc.NodeType.IsUnary())
                return true;

            return false;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return TextRepresentation;
        }
    }
}