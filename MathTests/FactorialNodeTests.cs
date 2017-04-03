using System;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Functions.Unary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class FactorialNodeTests
    {
        [TestMethod]
        public void BasicFactorialTest()
        {
            var factorial = new FactorialNode {ChildNode = new NumberNode(6)};
            Assert.AreEqual(720,factorial.Evaluate());
        }

        [TestMethod]
        public void FactorialOfZero()
        {
            var factorial = new FactorialNode {ChildNode = new NumberNode(0)};
            Assert.AreEqual(1,factorial.Evaluate());
        }

        [TestMethod]
        public void FactorialOfDecimalNumber()
        {
            var factorial = new FactorialNode {ChildNode = new NumberNode(2.5m)};
            Assert.IsTrue((factorial.Evaluate()-3.32335m)<0.00001m);
        }
    }

}
