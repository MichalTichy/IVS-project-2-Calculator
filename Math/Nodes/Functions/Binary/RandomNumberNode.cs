using System;

namespace Math.Nodes.Functions.Binary
{
    /// <summary>
    /// Generates random decimal number
    /// </summary>
    public class RandomNumberNode : IBinaryOperationNode
    {
        /// <inheritdoc />
        public INode Parent { get; set; }

        /// <summary>
        /// Min value
        /// </summary>
        public INode RightNode { get; set; }

        /// <summary>
        /// Max value
        /// </summary>
        public INode LeftNode { get; set; }

        private static Random random;


        /// <summary>
        /// Initializes new random number node.
        /// </summary>
        public RandomNumberNode()
        {
            if (random == null)
                random = new Random();
        }


        /// <inheritdoc />
        public decimal Evaluate()
        {
            var min = LeftNode.Evaluate();
            var max = RightNode.Evaluate();

            return (decimal) random.NextDouble() * (max - min) + min;
        }
        
    }
}