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
    public static class Extensions
    {
        internal static bool isUnary(this Type type)
        {
            return typeof(IUnaryOperationNode).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }
        internal static bool isPrecedingUnary(this Type type)
        {
            return typeof(IPrecedingUnaryOperationNode).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }
        internal static bool isFollowingUnary(this Type type)
        {
            return typeof(IFollowingUnaryOperationNode).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }

        internal static bool isBinary(this Type type)
        {
            return typeof(IBinaryOperationNode).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }
        internal static bool isFunctionNode(this Type type)
        {
            return typeof(IFunctionNode).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }
        
        public static ExpressionPartTypes ToExpressionPart(this Type type)
        {
            if (typeof(NumberNode).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
            return ExpressionPartTypes.Number;

            if (type.isPrecedingUnary())
                return ExpressionPartTypes.UnaryPreceding;

            if (type.isFollowingUnary())
                return ExpressionPartTypes.UnaryFollowing;

            if (type.isBinary())
                return ExpressionPartTypes.Binary;

            throw new NotSupportedException($"{nameof(type)} cannot be converted into {nameof(ExpressionPartTypes)}");
        }
    }
}
