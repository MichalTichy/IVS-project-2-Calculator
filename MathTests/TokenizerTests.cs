using System;
using System.Diagnostics;
using System.Linq;
using Math;
using Math.Nodes.Functions;
using Math.Nodes.Functions.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class TokenizerTests
    {
        [TestMethod]
        public void CheckIfAllFunctionNodesAreRegisteredByDefault()
        {
            var tokenizer = new Tokenizer();
            var expressionParser = new ExpressionTreeBuilder<Tokenizer>(tokenizer);

            var type = typeof(IFunctionNode);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract && p.IsPublic).ToArray();

            var fail = false;
            foreach (var functionType in types)
            {
                if (tokenizer.RegisteredOperators.SingleOrDefault(t => t.NodeType == functionType) == null)
                {
                    Debug.WriteLine($"{functionType} not found");
                    fail = true;
                }
            }

            Assert.IsFalse(fail);
            Assert.AreEqual(types.Count(), tokenizer.RegisteredOperators.Count);
        }

        [TestMethod]
        public void CheckPossibleNextOperators()
        {
            var tokenizer = new Tokenizer();
            var operations = tokenizer.GetPossibleNextMathOperators(ExpressionPartTypes.RightParentheses);
            Assert.IsTrue(operations.Any(t=>t.NodeType==typeof(SumNode)));
        }
    }
}