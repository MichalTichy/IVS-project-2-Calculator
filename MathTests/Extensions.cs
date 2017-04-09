using System;
using System.Runtime.Remoting.Messaging;

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

        public static bool Compare(this decimal n1, decimal n2)
        {
            return System.Math.Abs(n1 - n2) < 0.00001m;
        }
    }
}
