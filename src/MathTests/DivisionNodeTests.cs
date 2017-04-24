using System;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class DivisionNodeTests
    {
        [TestMethod]
        public void PositiveNumbersDivisionTest()
        {
            DivisionNode division = new DivisionNode();
            division.LeftNode = new NumberNode(60);
            division.RightNode = new NumberNode(30);

            Assert.AreEqual(2, division.Evaluate());
        }

        [TestMethod]
        public void NegativeNumbersDivisionTest()
        {
            DivisionNode division = new DivisionNode();
            division.LeftNode = new NumberNode(-12);
            division.RightNode = new NumberNode(-6);

            Assert.AreEqual(2, division.Evaluate());
        }

        [TestMethod]
        public void PositiveDecimalNumbersDivisionTest()
        {
            DivisionNode division = new DivisionNode();
            division.LeftNode = new NumberNode(21.42m);
            division.RightNode = new NumberNode(53.34m);

            Assert.AreEqual(0.401574803149606299212598425196850393700787m, division.Evaluate());
        }

        [TestMethod]
        public void NegativDecimaleNumbersDivisionTest()
        {
            DivisionNode division = new DivisionNode();
            division.LeftNode = new NumberNode(0.83294720473m);
            division.RightNode = new NumberNode(-64.332323432m);

            Assert.AreEqual(-0.01294756912690141994683740214m, division.Evaluate());
        }

        [TestMethod]
        public void FirstZeroNumberDivisionTest()
        {
            DivisionNode division = new DivisionNode();
            division.LeftNode = new NumberNode(0);
            division.RightNode = new NumberNode(1);
            Assert.AreEqual(0, division.Evaluate());
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void SecondZeroNumberDivisionTest()
        {
            DivisionNode division = new DivisionNode();
            division.LeftNode = new NumberNode(1);
            division.RightNode = new NumberNode(0);
            division.Evaluate();
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void BothZeroNumbersDivisionTest()
        {
            DivisionNode division = new DivisionNode();
            division.LeftNode = new NumberNode(0);
            division.RightNode = new NumberNode(0);
            division.Evaluate();
        }

        [TestMethod]
        public void VeryLongDecimalNumbersDivisionTest()
        {
            DivisionNode divison = new DivisionNode();
            divison.LeftNode = new NumberNode(6.24124242480281402804801284m);
            divison.RightNode = new NumberNode(9.42424543467474634634644m);
            Assert.AreEqual(0.6622538078050611060600621418m, divison.Evaluate());
        }
    }

}
