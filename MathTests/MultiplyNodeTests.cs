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
    public class MultiplyNodeTests
    {
        [TestMethod]
        public void BasicMultiplicationTest()
        {
            MultiplyNode multiplication = new MultiplyNode();
            multiplication.LeftNode = new NumberNode(1.2);
            multiplication.RightNode = new NumberNode(2.3);

            Assert.AreEqual(2.76, multiplication.Evaluate());
        }
    }
}
