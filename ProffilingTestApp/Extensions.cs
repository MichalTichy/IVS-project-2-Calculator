using System;

namespace ProffilingTestApp
{
    public static class Extensions
    {
        public static decimal NextDecimal(this Random rng)
        {
            byte scale = (byte)rng.Next(10);
            return new decimal(rng.Next(),
                rng.Next(),
                rng.Next(),
                false,
                scale);
        }
    }
}
