using Math;
using Math.Nodes;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class SumNodeTests
    {
        [TestMethod]
        public void BasicSummationTests()
        {
            var summation = new SumNode
            {
                LeftNode = new NumberNode(5),
                RightNode = new NumberNode(6)
            };

            Assert.AreEqual(11,summation.Evaluate());
        }

    }
}
