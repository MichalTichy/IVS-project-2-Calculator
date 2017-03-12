using System;
using Math;
using Math.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class NumberNodeTests
    {
        [TestMethod]
        public void ReturnsItsValue()
        {
            decimal value = 5.123m;
            var number=new NumberNode(value);
            Assert.AreEqual(value,number.Evaluate());
        }

        [TestMethod]
        public void BruteForceRandomTest()
        {
            Random random=new Random();
            for (int i = 0; i < 1000; i++)
            {
                var value = random.NextDecimal();
                var number=new NumberNode(value);
                Assert.AreEqual(value,number.Evaluate());
            }
        }
    }
}
