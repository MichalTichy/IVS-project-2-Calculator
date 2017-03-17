using Math.Nodes;
using Math.Nodes.Functions.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class SubstractionNodeTests
    {
        [TestMethod]
        public void BasicSubstractionTest()
        {
            SubstractionNode substraction = new SubstractionNode
            {
                LeftNode = new NumberNode(3.5m),
                RightNode = new NumberNode(1.3m)
            };

            Assert.AreEqual(2.2m, substraction.Evaluate());
        }
    }
}
