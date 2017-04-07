using System;
using System.Numerics;

namespace Math.Nodes.Functions.Unary
{
    public class FactorialNode : IUnaryOperationNode
    {
        public INode Parent { get; set; }
        public INode ChildNode { get; set; }

        public decimal Evaluate()
        {
            var childNodeValue = ChildNode.Evaluate();

            CheckIfItsPossibleToCalculateFactorial(childNodeValue);

            if (childNodeValue % 1 == 0)
                return Factorial(childNodeValue);
            
            return (decimal)Gamma((double)(childNodeValue + 1));
        }

        private static decimal Factorial(decimal childNodeValue)
        {
            decimal factorial = 1;
            for (decimal i = 1; i <= childNodeValue; i++)
            {
                factorial *= i;
            }

            return factorial;
        }


        private static int g = 7;

        private static double[] p =
        {
            0.99999999999980993, 676.5203681218851, -1259.1392167224028,
            771.32342877765313, -176.61502916214059, 12.507343278686905,
            -0.13857109526572012, 9.9843695780195716e-6, 1.5056327351493116e-7
        };

        public static double Gamma(double z)
        {
            if (z < 0.5)
            {
                return System.Math.PI / (System.Math.Sin(System.Math.PI * z) * Gamma(1 - z));
            }
            else
            {
                z -= 1;
                var x = p[0];
                for (var i = 1; i < g + 2; i++)
                {
                    x += p[i] / (z + i);
                }
                var t = z + g + 0.5;
                return System.Math.Sqrt(2 * System.Math.PI) * (System.Math.Pow(t, z + 0.5)) * System.Math.Exp(-t) * x;
            }
        }

        void CheckIfItsPossibleToCalculateFactorial(decimal value)
        {

            if (value>27)
            {
                throw new OverflowException("Cannot calculate factorial of bigger number than 27.");
            }

            if (value<0)
            {
                throw new ArgumentException("Cannot calculate factorial of negative number");
            }
        }


    }
}