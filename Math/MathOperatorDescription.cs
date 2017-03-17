using System;
using System.Reflection;
using Math.Nodes.Functions;

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
    }
}