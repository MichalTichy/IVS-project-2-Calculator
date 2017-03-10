using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTests
{
    public static class Extensions
    {
        public static double NextBigDouble(this Random random)
        {
            return random.Next(int.MinValue + 1, int.MaxValue - 1) + random.NextDouble();
        }
    }
}
