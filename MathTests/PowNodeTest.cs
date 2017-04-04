using System;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class PowNodeTests
    {
        [TestMethod]
        public void PositiveNumbersPowTest()
        {
            PowNode power = new PowNode();
            power.LeftNode = new NumberNode(2);
            power.RightNode = new NumberNode(3);

            Assert.AreEqual(8, power.Evaluate());
        }
        

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void OverflowPositiveNumbersPowerTest()
        {
            PowNode power = new PowNode();
            power.LeftNode = new NumberNode(9999999999999999999);
            power.RightNode = new NumberNode(9999999999999999999);
            power.Evaluate();
        }

        [TestMethod]
        public void FirstZeroNumberPowerTest()
        {
            PowNode power = new PowNode();
            power.LeftNode = new NumberNode(0);
            power.RightNode = new NumberNode(2);

            Assert.AreEqual(0, power.Evaluate());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SecondZeroNumberPowerTest()
        {
            PowNode power = new PowNode();
            power.LeftNode = new NumberNode(2);
            power.RightNode = new NumberNode(0);

            power.Evaluate();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SecondNumberNotNaturalPowerTest()
        {
            PowNode power = new PowNode();
            power.LeftNode = new NumberNode(2);
            power.RightNode = new NumberNode(3.4m);

            power.Evaluate();
        }
    
    }
}
