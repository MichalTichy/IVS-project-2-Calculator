using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class LogNodeTests
    {
        [TestMethod]
        public void PositiveNumberLogNodeTest()
        {
            LogNode log = new LogNode();
            log.LeftNode = new NumberNode(2);
            log.RightNode = new NumberNode(4);

            Assert.AreEqual(2, log.Evaluate());
        }

        [TestMethod]
        public void PositiveDecimalNumberLogNodeTest()
        {
            LogNode log = new LogNode();
            log.LeftNode = new NumberNode(2);
            log.RightNode = new NumberNode(4.4m);

            Assert.AreEqual(2.13750352374994m, log.Evaluate());
        }

        [TestMethod]
        public void PositiveDecimalArgumentNumberLogNodeTest()
        {
            LogNode log = new LogNode();
            log.LeftNode = new NumberNode(2.2m);
            log.RightNode = new NumberNode(4);

            Assert.AreEqual(1.75823631157355m, log.Evaluate());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ZeroNumberLogNodeTest()
        {
            LogNode log = new LogNode();
            log.LeftNode = new NumberNode(2);
            log.RightNode = new NumberNode(0);

            log.Evaluate();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ZeroArgumentNumberLogNodeTest()
        {
            LogNode log = new LogNode();
            log.LeftNode = new NumberNode(0);
            log.RightNode = new NumberNode(2);

            log.Evaluate();
        }


    }
}
