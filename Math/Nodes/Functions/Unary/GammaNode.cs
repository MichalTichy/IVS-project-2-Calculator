namespace Math.Nodes.Functions.Unary
{
    public class GammaNode : IPrecedingUnaryOperationNode
    {
        public INode Parent { get; set; }
        
        public INode ChildNode { get; set; }

        public decimal Evaluate()
        {
            return (decimal)Gamma((double)ChildNode.Evaluate());
        }

        private static int g = 7;

        private static double[] p =
        {
            0.99999999999980993, 676.5203681218851, -1259.1392167224028,
            771.32342877765313, -176.61502916214059, 12.507343278686905,
            -0.13857109526572012, 9.9843695780195716e-6, 1.5056327351493116e-7
        };

        protected static double Gamma(double z)
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

    }
}