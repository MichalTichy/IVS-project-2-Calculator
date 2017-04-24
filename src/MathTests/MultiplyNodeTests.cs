using System;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class MultiplyNodeTests
    {
        [TestMethod]
        public void PositiveNumbersMultiplicationTest()
        {
            MultiplyNode multiplication = new MultiplyNode();
            multiplication.LeftNode = new NumberNode(23);
            multiplication.RightNode = new NumberNode(42);

            Assert.AreEqual(966, multiplication.Evaluate());
        }

        [TestMethod]
        public void NegativeNumbersMultiplicationTest()
        {
            MultiplyNode multiplication = new MultiplyNode();
            multiplication.LeftNode = new NumberNode(-31);
            multiplication.RightNode = new NumberNode(-64);

            Assert.AreEqual(1984, multiplication.Evaluate());
        }

        [TestMethod]
        public void PositiveDecimalNumbersMultiplicationTest()
        {
            MultiplyNode multiplication = new MultiplyNode();
            multiplication.LeftNode = new NumberNode(21.424242m);
            multiplication.RightNode = new NumberNode(53.34322345m);

            Assert.AreEqual(1142.8381282528749m, multiplication.Evaluate());
        }

        [TestMethod]
        public void NegativDecimaleNumbersMultiplicationTest()
        {
            MultiplyNode multiplication = new MultiplyNode();
            multiplication.LeftNode = new NumberNode(0.83294720473m);
            multiplication.RightNode = new NumberNode(-64.332323432m);

            Assert.AreEqual(-53.58542897647068023336m, multiplication.Evaluate());
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void OverflowPositiveNumbersMultiplicationTest()
        {
            MultiplyNode multiplication = new MultiplyNode();
            multiplication.LeftNode = new NumberNode(9999999999999999999);
            multiplication.RightNode = new NumberNode(9999999999999999999);
            multiplication.Evaluate();
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void OverflowNegativeNumbersMultiplicationTest()
        {
            MultiplyNode multiplication = new MultiplyNode();
            multiplication.LeftNode = new NumberNode(-999999999999999999);
            multiplication.RightNode = new NumberNode(-999999999999999999);
            multiplication.Evaluate();
        }

        [TestMethod]
        public void VeryLongDecimalNumbersMultiplicationTest()
        {
            MultiplyNode multiplication = new MultiplyNode();
            multiplication.LeftNode = new NumberNode(6.241242424802814028048012848052m);
            multiplication.RightNode = new NumberNode(9.424245434674746346346446364m);
            Assert.AreEqual(58.81900042864626397698076729688581331333756828662489988292m, multiplication.Evaluate());
        }
    }

}
