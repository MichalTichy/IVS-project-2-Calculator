using Math.Nodes;
using Math.Nodes.Functions.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTests
{
    [TestClass]
    public class SubstractionNodeTests
    {
        [TestMethod]
        public void BasicSubstractionTest()
        {
            SubstractionNode substraction = new SubstractionNode();
            substraction.LeftNode = new NumberNode(3.5);
            substraction.RightNode = new NumberNode(1.3);

            Assert.AreEqual(2.2, substraction.Evaluate());
        }
    }
}
