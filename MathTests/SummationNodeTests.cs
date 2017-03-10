using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class SummationNodeTests
    {
        [TestMethod]
        public void BasicSummationTests()
        {
            var summation = new SummationNode
            {
                LeftNode = new NumberNode(5),
                RightNode = new NumberNode(6)
            };

            Assert.AreEqual(11,summation.Evaluate());
        }

    }
}
