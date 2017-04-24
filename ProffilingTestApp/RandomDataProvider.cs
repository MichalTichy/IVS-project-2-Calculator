using System;

namespace ProffilingTestApp
{
    public class RandomDataProvider : IDataProvider
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
