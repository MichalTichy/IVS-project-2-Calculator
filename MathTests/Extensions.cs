using System;

namespace MathTests
{
    public static class Extensions
    {
        public static decimal NextDecimal(this Random random)
        {
            return new decimal(random.Next(int.MinValue + 1, int.MaxValue - 1) + random.NextDouble());
        }
        public static decimal NextDecimal(this Random random,int min,int max)
        {
            return new decimal(random.Next(min + 1, max - 1) + random.NextDouble());
        }
    }
}
