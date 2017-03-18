using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math;
using Math.Nodes.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class ExpressionParserTests
    {
        [TestMethod]
        public void CheckIfAllFunctionNodesAreRegisteredByDefault()
        {
            var expressionParser=new ExpressionTreeBuilder();

            var type = typeof(IFunctionNode);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract).ToArray();

            Assert.AreEqual(types.Count(),expressionParser.RegisteredOperators.Count);
            foreach (var functionType in types)
            {
                Assert.IsNotNull(expressionParser.RegisteredOperators.SingleOrDefault(t=>t.NodeType==functionType));
            }
        }

        [TestMethod]
        public void MathOperationsOrderTest1()
        {
            var expressionParser=new ExpressionTreeBuilder();
            Assert.AreEqual(26, expressionParser.ParseExpression("3*6+8").Evaluate());
        }

        [TestMethod]
        public void MathOperationsOrderTest2()
        {
            var expressionParser=new ExpressionTreeBuilder();
            Assert.AreEqual(51, expressionParser.ParseExpression("3+6*8").Evaluate());
        }

        [TestMethod]
        public void ParenthesisTest1()
        {
            var expressionParser=new ExpressionTreeBuilder();
            Assert.AreEqual(72, expressionParser.ParseExpression("(3+6)*8").Evaluate());
        }

        [TestMethod]
        public void ParenthesisTest2()
        {
            var expressionParser=new ExpressionTreeBuilder();
            Assert.AreEqual(42, expressionParser.ParseExpression("3*(6+8)").Evaluate());
        }
    }
}
