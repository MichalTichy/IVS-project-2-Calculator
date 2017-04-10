using System;
using System.Reflection;
using Math.Nodes.Functions;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Functions.Unary;

namespace Math
{
    public class MathOperatorDescription
    {
        public readonly string TextRepresentation;
        public readonly Type NodeType;
        public readonly OperationType OperationType;

        public MathOperatorDescription(Type nodeType, string textRepresentation, OperationType operationType)
        {
            if (!typeof(IFunctionNode).IsAssignableFrom(nodeType))
                throw new ArgumentException($"{nameof(nodeType)} is not implementation of {nameof(IFunctionNode)}");
            NodeType = nodeType;

            if (string.IsNullOrWhiteSpace(textRepresentation))
                throw new ArgumentException($"{nameof(textRepresentation)} cannot be empty or whitespace.");
            TextRepresentation = textRepresentation;

            OperationType = operationType;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MathOperatorDescription desc))
                return false;

            if (desc.TextRepresentation != TextRepresentation)
                return false;

            if (NodeType.isBinary() && desc.NodeType.isBinary())
                return true;

            if (NodeType.isUnary() && desc.NodeType.isUnary())
                return true;

            return false;
        }

        public override string ToString()
        {
            return TextRepresentation;
        }
    }
}