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
            Assert.AreEqual(0.0524077792830412m, tan.Evaluate());
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
            Assert.AreEqual(0.674508516842426m, tan.Evaluate());
        }

        [TestMethod]
        public void NegativeNumberTanTest()
        {
            var tan = new TanNode() { ChildNode = new NumberNode(-8) };
            Assert.AreEqual(-0.140540834702391m, tan.Evaluate());
        }

        [TestMethod]
        public void ZeroNumberTanTest()
        {
            var tan = new TanNode() { ChildNode = new NumberNode(0) };
            Assert.AreEqual(0, tan.Evaluate());
        }
    }
}
