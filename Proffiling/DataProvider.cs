using System;
using System.Collections.Generic;
using System.Text;

namespace Proffiling
{
    public class DataProvider : IDataProvider
    {
        public decimal[] GetData(int count)
        {
            var random=new Random();
            var generatedNumbers = new decimal[count];
            for (int i = 0; i < count; i++)
            {
                generatedNumbers[i] = random.Next(100);
            }
            return generatedNumbers;
        }
    }
}
