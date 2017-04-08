using System;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math.Nodes.Functions.Unary;

namespace MathTests
{
    [TestClass]
    public class SinNodeTests
    {
        [TestMethod]
        public void PosistiveNumberSinTest()
        {
            var sin = new SinNode() { ChildNode = new NumberNode(4) };
            Assert.AreEqual(0.0697564737441253m, sin.Evaluate());
        }

        [TestMethod]
        public void NegativeNumberSinTest()
        {
            var sin = new SinNode() { ChildNode = new NumberNode(-1) };
            Assert.AreEqual(-0.0174524064372835m, sin.Evaluate());
        }

        [TestMethod]
        public void ZeroNumberSinTest()
        {
            var sin = new SinNode() { ChildNode = new NumberNode(0) };
            Assert.AreEqual(0, sin.Evaluate());
        }




    }


}
