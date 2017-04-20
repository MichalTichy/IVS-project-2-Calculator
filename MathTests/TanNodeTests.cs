using System;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math.Nodes.Functions.Unary;

namespace MathTests
{
    [TestClass]
    public class TanNodeTests
    {
        [TestMethod]
        public void PosistiveNumberTanTest()
        {
            var tan = new TanNode() { ChildNode = new NumberNode(3) };
            Assert.AreEqual(0.05240777928304m, tan.Evaluate());
        }
        [TestMethod]
        public void AnotherPosistiveNumberTanTest()
        {
            var tan = new TanNode() { ChildNode = new NumberNode(45) };
            Assert.AreEqual(1, tan.Evaluate());
        }

        [TestMethod]
        public void BigPosistiveNumberTanTest()
        {
            var tan = new TanNode() { ChildNode = new NumberNode(754) };
            Assert.AreEqual(0.67450851684243m, tan.Evaluate());
        }

        [TestMethod]
        public void NegativeNumberTanTest()
        {
            var tan = new TanNode() { ChildNode = new NumberNode(-8) };
            Assert.AreEqual(-0.14054083470239m, tan.Evaluate());
        }

        [TestMethod]
        public void ZeroNumberTanTest()
        {
            var tan = new TanNode() { ChildNode = new NumberNode(0) };
            Assert.AreEqual(0, tan.Evaluate());
        }
    }
}
