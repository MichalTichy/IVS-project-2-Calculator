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
            multiplication.LeftNode = new NumberNode(1.2m);
            multiplication.RightNode = new NumberNode(2.3m);

            Assert.AreEqual(2.76m, multiplication.Evaluate());
        }
    }
}
