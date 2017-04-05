using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math.Nodes.Functions.Unary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class SqrtNodeTests
    {
        [TestMethod]
        public void PosistiveNumbersSqrtTest()
        {
            var sqrt = new SqrtNode(){ ChildNode = new NumberNode(4) };
            Assert.AreEqual(2, sqrt.Evaluate());
        }
  
    }
}
