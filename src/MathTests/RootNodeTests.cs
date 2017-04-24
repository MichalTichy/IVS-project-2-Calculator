using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class RootNodeTests
    {
        [TestMethod]
        public void PositiveNumbersRootNodeTest()
        {
            RootNode root = new RootNode();
            root.LeftNode = new NumberNode(2);
            root.RightNode = new NumberNode(4);

            Assert.AreEqual(2, root.Evaluate());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NegativeNumerRootNodeTest()
        {
            RootNode root = new RootNode();
            root.LeftNode = new NumberNode(2);
            root.RightNode = new NumberNode(-4);

            root.Evaluate();
        }

        [TestMethod]
        public void NegativeArgumentRootNodeTests()
        {
            RootNode root = new RootNode();
            root.LeftNode = new NumberNode(-2);
            root.RightNode = new NumberNode(4);

            Assert.AreEqual(0.5m, root.Evaluate());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ZeroArgumentRootNodeTests()
        {
            RootNode root = new RootNode();
            root.LeftNode = new NumberNode(0);
            root.RightNode = new NumberNode(4);

            root.Evaluate();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BothZeroArgumentRootNodeTests()
        {
            RootNode root = new RootNode();
            root.LeftNode = new NumberNode(0);
            root.RightNode = new NumberNode(0);

            root.Evaluate();
        }

        [TestMethod]
        public void SecondZeroArgumentRootNodeTests()
        {
            RootNode root = new RootNode();
            root.LeftNode = new NumberNode(2);
            root.RightNode = new NumberNode(0);

            Assert.AreEqual(0,root.Evaluate());
        }

        [TestMethod]
        public void SqrtArgumentRootNodeTests()
        {
            RootNode root = new RootNode();
            root.LeftNode = new NumberNode(-2);
            root.RightNode = new NumberNode(0.5m);

           Assert.AreEqual(1.4142135623731m, root.Evaluate());
        }

        [TestMethod]
        public void NegativeDecimalArgumentRootNodeTests()
        {
            RootNode root = new RootNode();
            root.LeftNode = new NumberNode(-2);
            root.RightNode = new NumberNode(0.5m);

            Assert.AreEqual(1.4142135623731m, root.Evaluate());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OddNegativeRootNodeTests()
        {
            RootNode root = new RootNode();
            root.LeftNode = new NumberNode(2);
            root.RightNode = new NumberNode(-8);

            root.Evaluate();
        }

        [TestMethod]
        public void EvenNegativeRootNodeTests()
        {
            RootNode root = new RootNode();
            root.LeftNode = new NumberNode(3);
            root.RightNode = new NumberNode(-8);

            Assert.AreEqual(-2, root.Evaluate());

        }
    }
}
