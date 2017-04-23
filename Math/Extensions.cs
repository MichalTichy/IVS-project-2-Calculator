using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Math.ExpressionTreeBuilder;
using Math.Nodes.Functions;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Functions.Unary;
using Math.Nodes.Values;

namespace Math
{
    /// <summary>
    /// Helper class with extension methods.
    /// </summary>
    public static class Extensions
    {
        internal static bool IsUnary(this Type type)
        {
            return typeof(IUnaryOperationNode).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }
        internal static bool IsPrecedingUnary(this Type type)
        {
            return typeof(IPrecedingUnaryOperationNode).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }
        internal static bool IsFollowingUnary(this Type type)
        {
            return typeof(IFollowingUnaryOperationNode).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }

        internal static bool IsBinary(this Type type)
        {
            return typeof(IBinaryOperationNode).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }
        internal static bool IsFunctionNode(this Type type)
        {
            return typeof(IFunctionNode).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }
        
        /// <summary>
        /// Transforms Node type to Expression part
        /// </summary>
        /// <param name="type">Node type</param>
        /// <returns> resulting expression part </returns>
        /// <exception cref="NotSupportedException"> Throws when given node cannot be translated to ExpressionPartType </exception>
        public static ExpressionPartTypes ToExpressionPart(this Type type)
        {
            if (typeof(NumberNode).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
            return ExpressionPartTypes.Number;

            if (type.IsPrecedingUnary())
                return ExpressionPartTypes.UnaryPreceding;

            if (type.IsFollowingUnary())
                return ExpressionPartTypes.UnaryFollowing;

            if (type.IsBinary())
                return ExpressionPartTypes.Binary;

            throw new NotSupportedException($"{nameof(type)} cannot be converted into {nameof(ExpressionPartTypes)}");
        }
    }
}
