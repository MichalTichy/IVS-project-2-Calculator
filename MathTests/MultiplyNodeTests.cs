using Math.Nodes;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class MultiplyNodeTests
    {
        [TestMethod]
        public void BasicMultiplicationTest()
        {
            MultiplyNode multiplication = new MultiplyNode();
            multiplication.LeftNode = new NumberNode(1.2m);
            multiplication.RightNode = new NumberNode(2.3m);

            Assert.AreEqual(2.76m, multiplication.Evaluate());
        }
    }
}
