using System;
using Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class NumberNodeTests
    {
        [TestMethod]
        public void ReturnsItsValue()
        {
            double value = 5.123;
            var number=new NumberNode(value);
            Assert.AreEqual(value,number.Evaluate());
        }

        [TestMethod]
        public void BruteForceRandomTest()
        {
            Random random=new Random();
            for (int i = 0; i < 1000; i++)
            {
                var value = random.Next(int.MinValue+1, int.MaxValue-1)+random.NextDouble();
                var number=new NumberNode(value);
                Assert.AreEqual(value,number.Evaluate());
            }
        }
    }
}
