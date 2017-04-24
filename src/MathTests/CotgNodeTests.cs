using System;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math.Nodes.Functions.Unary;

namespace MathTests
{
    [TestClass]
    public class CotgNodeTests
    {
        [TestMethod]
        public void PosistiveNumberCotgTest()
        {
            var cotg = new CotgNode() { ChildNode = new NumberNode(3) };
            Assert.AreEqual(19.0811366877282m, cotg.Evaluate());
        }
        [TestMethod]
        public void AnotherPosistiveNumberCotgTest()
        {
            var cotg = new CotgNode() { ChildNode = new NumberNode(45) };
            Assert.AreEqual(1, cotg.Evaluate());
        }

        [TestMethod]
        public void BigPosistiveNumberCotgTest()
        {
            var cotg = new CotgNode() { ChildNode = new NumberNode(754) };
            Assert.AreEqual(1.48256096851274m, cotg.Evaluate());
        }

        [TestMethod]
        public void NegativeNumberCotgTest()
        {
            var cotg = new CotgNode() { ChildNode = new NumberNode(-8) };
            Assert.AreEqual(-7.11536972238421m, cotg.Evaluate());
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ZeroNumberCotgTest()
        {
            var cotg = new CotgNode() { ChildNode = new NumberNode(0) };
            cotg.Evaluate();
        }
    }
}
