using Math.Nodes.Functions.Binary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class SumNodeTests
    {
        [TestMethod]
        public void PositiveNumbersSummationTest()
        {
            SumNode summation = new SumNode();
            summation.LeftNode = new NumberNode(23);
            summation.RightNode = new NumberNode(42);

            Assert.AreEqual(65, summation.Evaluate());
        }

        [TestMethod]
        public void NegativeNumbersSummationTest()
        {
            SumNode summation = new SumNode();
            summation.LeftNode = new NumberNode(-31);
            summation.RightNode = new NumberNode(-64);

            Assert.AreEqual(-95, summation.Evaluate());
        }

        [TestMethod]
        public void PositiveDecimalNumbersSummationTest()
        {
            SumNode summation = new SumNode();
            summation.LeftNode = new NumberNode(21.424242m);
            summation.RightNode = new NumberNode(53.34322345m);

            Assert.AreEqual(74.76746545m, summation.Evaluate());
        }

        [TestMethod]
        public void NegativDecimaleNumbersSummationTest()
        {
            SumNode summation = new SumNode();
            summation.LeftNode = new NumberNode(0.83294720473m);
            summation.RightNode = new NumberNode(-64.332323432m);

            Assert.AreEqual(-63.49937622727m, summation.Evaluate());
        }

        [TestMethod]
        public void VeryLongDecimalNumbersSummationTest()
        {
            SumNode summation = new SumNode();
            summation.LeftNode = new NumberNode(6.241242424802814028048012848052m);
            summation.RightNode = new NumberNode(9.424245434674746346346446364m);
            Assert.AreEqual(15.665487859477560374394459212052m, summation.Evaluate());
        }
    }

}
