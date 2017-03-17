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
            var expressionParser=new ExpressionParser();

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
    }
}
