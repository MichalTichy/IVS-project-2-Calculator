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
        public void PosistiveNumberCosTest()
        {
            var cos = new CosNode() { ChildNode = new NumberNode(3) };
            Assert.AreEqual(0.998629534754574m, cos.Evaluate());
        }
        [TestMethod]
        public void AnotherPosistiveNumberCosTest()
        {
            var cos = new CosNode() { ChildNode = new NumberNode(45) };
            Assert.AreEqual(0.707106781186548m, cos.Evaluate());
        }

        [TestMethod]
        public void BigPosistiveNumberCosTest()
        {
            var cos = new CosNode() { ChildNode = new NumberNode(754) };
            Assert.AreEqual(0.829037572555042m, cos.Evaluate());
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
