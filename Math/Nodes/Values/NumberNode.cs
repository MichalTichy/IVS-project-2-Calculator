using System;

namespace Math.Nodes.Values
{
    /// <summary>
    /// Node used to store numeric value.
    /// </summary>
    public class NumberNode : IValueNode
    {
        /// <summary>
        /// Value of the node.
        /// </summary>
        protected readonly decimal Value;

        /// <summary>
        /// Initializes new number node with given value.
        /// </summary>
        /// <param name="value">future value</param>
        public NumberNode(decimal value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Parses given value and initialize new number now from it.
        /// </summary>
        /// <param name="value">future value</param>
        public NumberNode(string value)
        {
            Value = decimal.Parse(value);
        }

        /// <inheritdoc />
        public INode Parent { get; set; }

        /// <inheritdoc />
        public virtual decimal Evaluate()
        {
            return Value;
        }

        /// <summary>
        /// checks if given string can be converted to number.
        /// </summary>
        /// <param name="text">source text</param>
        /// <returns>Whether given string is valid number.</returns>
        public static bool IsNumber(string text)
        {
            return decimal.TryParse(text, out var result);
        }
    }
}