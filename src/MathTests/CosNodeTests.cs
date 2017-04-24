using System;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math.Nodes.Functions.Unary;

namespace MathTests
{
    [TestClass]
    public class CosNodeTests
    {
        [TestMethod]
        public void PositiveNumberCosTest()
        {
            var cos = new CosNode() { ChildNode = new NumberNode(3) };
            Assert.AreEqual(0.99862953475457m, cos.Evaluate());
        }
        [TestMethod]
        public void AnotherPositiveNumberCosTest()
        {
            var cos = new CosNode() { ChildNode = new NumberNode(45) };
            Assert.AreEqual(0.70710678118655m, cos.Evaluate());
        }

        [TestMethod]
        public void BigPositiveNumberCosTest()
        {
            var cos = new CosNode() { ChildNode = new NumberNode(754) };
            Assert.AreEqual(0.82903757255504m, cos.Evaluate());
        }

        [TestMethod]
        public void NegativeNumberCosTest()
        {
            var cos = new CosNode() { ChildNode = new NumberNode(-8) };
            Assert.AreEqual(0.99026806874157m, cos.Evaluate());
        }

        [TestMethod]
        public void ZeroNumberCosTest()
        {
            var cos = new CosNode() { ChildNode = new NumberNode(0) };
            Assert.AreEqual(1, cos.Evaluate());
        }
    }
}
