using System;

namespace Math.Nodes.Functions.Unary
{

    /// <summary>
    /// Node used to calculate gamma function
    /// </summary>
    public class GammaNode : IPrecedingUnaryOperationNode
    {
        /// <inheritdoc />
        public INode Parent { get; set; }

        /// <inheritdoc />
        public INode ChildNode { get; set; }

        /// <inheritdoc />
        public decimal Evaluate()
        {
            return (decimal)Gamma((double)ChildNode.Evaluate());
        }

        private static int g = 7;

        private static readonly double[] p =
        {
            0.99999999999980993, 676.5203681218851, -1259.1392167224028,
            771.32342877765313, -176.61502916214059, 12.507343278686905,
            -0.13857109526572012, 9.9843695780195716e-6, 1.5056327351493116e-7
        };

        /// <summary>
        /// Calculates gamma function.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>gamma function of value</returns>
        protected static double Gamma(double value)
        {
            if (value < 0.5)
            {
                return System.Math.PI / (System.Math.Sin(System.Math.PI * value) * Gamma(1 - value));
            }
            else
            {
                value -= 1;
                var x = p[0];
                for (var i = 1; i < g + 2; i++)
                {
                    x += p[i] / (value + i);
                }
                var t = value + g + 0.5;
                return System.Math.Sqrt(2 * System.Math.PI) * (System.Math.Pow(t, value + 0.5)) * System.Math.Exp(-t) * x;
            }
        }
    }
}