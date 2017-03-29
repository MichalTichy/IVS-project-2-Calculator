using Math.Nodes.Functions.Binary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class SubstractionNodeTests
    {
        [TestMethod]
        public void PositiveNumbersSubstractionTest()
        {
            SubstractionNode substraction = new SubstractionNode();
            substraction.LeftNode = new NumberNode(23);
            substraction.RightNode = new NumberNode(42);

            Assert.AreEqual(-19, substraction.Evaluate());
        }

        [TestMethod]
        public void NegativeNumbersSubstractionTest()
        {
            SubstractionNode substraction = new SubstractionNode();
            substraction.LeftNode = new NumberNode(-31);
            substraction.RightNode = new NumberNode(-64);

            Assert.AreEqual(33, substraction.Evaluate());
        }

        [TestMethod]
        public void PositiveDecimalNumbersSubstractionTest()
        {
            SubstractionNode substraction = new SubstractionNode();
            substraction.LeftNode = new NumberNode(21.424242m);
            substraction.RightNode = new NumberNode(53.34322345m);

            Assert.AreEqual(-31.91898145m, substraction.Evaluate());
        }

        [TestMethod]
        public void NegativeDecimaleNumbersSubstractionTest()
        {
            SubstractionNode substraction = new SubstractionNode();
            substraction.LeftNode = new NumberNode(0.83294720473m);
            substraction.RightNode = new NumberNode(-64.332323432m);

            Assert.AreEqual(65.16527063673m, substraction.Evaluate());
        }

        [TestMethod]
        public void VeryLongDecimalNumbersSubstractionTest()
        {
            SubstractionNode substraction = new SubstractionNode();
            substraction.LeftNode = new NumberNode(6.241242424802814028048012848052m);
            substraction.RightNode = new NumberNode(9.424245434674746346346446364m);
            Assert.AreEqual(-3.183003009871932318298433515948m, substraction.Evaluate());
        }

        [TestMethod]
        public void ZeroNumberSubstractionTest()
        {
            SubstractionNode substraction = new SubstractionNode();
            substraction.LeftNode = new NumberNode(0);
            substraction.RightNode = new NumberNode(3);
            Assert.AreEqual(-3, substraction.Evaluate());
        }

        [TestMethod]
        public void BothZeroNumbersSubstractionTest()
        {
            SubstractionNode substraction = new SubstractionNode();
            substraction.LeftNode = new NumberNode(0);
            substraction.RightNode = new NumberNode(0);
            Assert.AreEqual(0, substraction.Evaluate());
        }
    }

}
