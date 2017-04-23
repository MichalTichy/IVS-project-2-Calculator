namespace Math.ExpressionTreeBuilder
{
    /// <summary>
    /// Parts of math expression
    /// </summary>
    public enum ExpressionPartTypes
    {
        /// <summary>
        /// Number
        /// </summary>
        Number,

        /// <summary>
        /// (
        /// </summary>
        LeftParentheses,

        /// <summary>
        /// )
        /// </summary>
        RightParentheses,

        /// <summary>
        /// Unary function
        /// Operator follows operand
        /// eq. 10!
        /// </summary>
        UnaryFollowing,

        /// <summary>
        /// Unary function
        /// Operator precedes operand
        /// eq. -10
        /// </summary>
        UnaryPreceding,

        /// <summary>
        /// Binary function
        /// eq. 5+5
        /// </summary>
        Binary
    }
}