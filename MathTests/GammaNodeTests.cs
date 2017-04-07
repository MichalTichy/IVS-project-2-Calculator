using System;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Functions.Unary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class GammaNodeTests
    {
        [TestMethod]
        public void GammaTest1()
        {
            var factorial = new GammaNode() { ChildNode = new NumberNode(-1.5m) };
            Assert.IsTrue(factorial.Evaluate().Compare(2.363271801m));
        }

        [TestMethod]
        public void GammaTest2()
        {
            var factorial = new GammaNode() { ChildNode = new NumberNode(-0.5m) };
            Assert.IsTrue(factorial.Evaluate().Compare(-3.544907701m));
        }

        [TestMethod]
        public void GammaTest3()
        {
            var factorial = new GammaNode() { ChildNode = new NumberNode(0.5m) };
            Assert.IsTrue(factorial.Evaluate().Compare(1.772453850m));
        }

        [TestMethod]
        public void GammaTest4()
        {
            var factorial = new GammaNode() { ChildNode = new NumberNode(1m) };
            Assert.IsTrue(factorial.Evaluate().Compare(1));
        }
        
        [TestMethod]
        public void GammaTest5()
        {
            var factorial = new GammaNode() { ChildNode = new NumberNode(2m) };
            Assert.IsTrue(factorial.Evaluate().Compare(1));
        }

        [TestMethod]
        public void GammaTest6()
        {
            var factorial = new GammaNode() { ChildNode = new NumberNode(4m) };
            Assert.IsTrue(factorial.Evaluate().Compare(6));
        }
    }

}
