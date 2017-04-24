using System;

namespace Proffiling
{
    public class StdInDataProvider : IDataProvider
    {
        public decimal[] GetData(int count)
        {
            var data = new decimal[count];
            for (int i = 0; i < count; i++)
            {
                data[i] = Convert.ToDecimal(Console.ReadLine());
            }
            return data;
        }
    }
}