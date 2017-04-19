using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Math.ExpressionTreeBuilder;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Functions.Unary;
using Math.Nodes.Values;

namespace Math
{
    public static class Extensions
    {
        internal static bool isUnary(this Type type)
        {
            return typeof(IUnaryOperationNode).IsAssignableFrom(type);
        }
        internal static bool isPrecedingUnary(this Type type)
        {
            return typeof(IPrecedingUnaryOperationNode).IsAssignableFrom(type);
        }
        internal static bool isFollowingUnary(this Type type)
        {
            return typeof(IFollowingUnaryOperationNode).IsAssignableFrom(type);
        }

        internal static bool isBinary(this Type type)
        {
            return typeof(IBinaryOperationNode).IsAssignableFrom(type);
        }
        
        public static ExpressionPartTypes ToExpressionPart(this Type nodeType)
        {
            if (typeof(NumberNode).IsAssignableFrom(nodeType))
                return ExpressionPartTypes.Number;

            if (nodeType.isPrecedingUnary())
                return ExpressionPartTypes.UnaryPreceding;

            if (nodeType.isFollowingUnary())
                return ExpressionPartTypes.UnaryFollowing;

            if (nodeType.isBinary())
                return ExpressionPartTypes.Binary;

            throw new NotSupportedException($"{nameof(nodeType)} cannot be converted into {nameof(ExpressionPartTypes)}");
        }
    }
}
