using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math.Nodes.Functions.Unary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class SqrtNodeTests
    {
        [TestMethod]
        public void PosistiveNumberSqrtTest()
        {
            var sqrt = new SqrtNode(){ ChildNode = new NumberNode(4) };
            Assert.AreEqual(2, sqrt.Evaluate());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NegativeNumberSqrtTest()
        {
            var sqrt = new SqrtNode() { ChildNode = new NumberNode(-1) };
            sqrt.Evaluate();
        }

        [TestMethod]
        public void ZeroNumberSqrtTest()
        {
            var sqrt = new SqrtNode() { ChildNode = new NumberNode(0) };
            Assert.AreEqual(0, sqrt.Evaluate());
        }


    }
}
