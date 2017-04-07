using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var tokenizer=new Tokenizer();
            var expressionParser=new ExpressionTreeBuilder<Tokenizer>(tokenizer);

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
        public void MathOperationsOrderTest1()
        {
            var expressionParser=new ExpressionTreeBuilder<Tokenizer>();
            Assert.AreEqual(26, expressionParser.ParseExpression("3*6+8").Evaluate());
        }

        [TestMethod]
        public void MathOperationsOrderTest2()
        {
            var expressionParser=new ExpressionTreeBuilder<Tokenizer>();
            Assert.AreEqual(51, expressionParser.ParseExpression("3+6*8").Evaluate());
        }

        [TestMethod]
        public void ParenthesisTest1()
        {
            var expressionParser=new ExpressionTreeBuilder<Tokenizer>();
            Assert.AreEqual(72, expressionParser.ParseExpression("(3+6)*8").Evaluate());
        }

        [TestMethod]
        public void ParenthesisTest2()
        {
            var expressionParser = new ExpressionTreeBuilder<Tokenizer>();
            Assert.AreEqual(42, expressionParser.ParseExpression("3*(6+8)").Evaluate());
        }


        [TestMethod]
        public void HardTest()
        {
            var expressionParser = new ExpressionTreeBuilder<Tokenizer>();
            Assert.AreEqual(720, expressionParser.ParseExpression("(8*6/8)!").Evaluate());
        }

        [TestMethod]
        public void NegativeNumber()
        {
            var expressionParser = new ExpressionTreeBuilder<Tokenizer>();
            Assert.AreEqual(-3, expressionParser.ParseExpression("-3").Evaluate());
        }

        [TestMethod]
        public void NegativeNumberAndSubstractionTest()
        {
            var expressionParser = new ExpressionTreeBuilder<Tokenizer>();
            Assert.AreEqual(8, expressionParser.ParseExpression("3--5").Evaluate());
        }

        [TestMethod]
        public void NegativeNumberAndParenthesisTests()
        {
            var expressionParser = new ExpressionTreeBuilder<Tokenizer>();
            Assert.AreEqual(8, expressionParser.ParseExpression("(3-(-5))").Evaluate());
        }

        [TestMethod]
        public void MultipleParenthisTest()
        {
            var expressionParser = new ExpressionTreeBuilder<Tokenizer>();
            Assert.AreEqual(20, expressionParser.ParseExpression("(((((3)*(15-8)+(-1)))))").Evaluate());
        }

        [TestMethod]
        public void DivisionByZeroTest()
        {
            var expressionParser = new ExpressionTreeBuilder<Tokenizer>();
            Assert.ThrowsException<DivideByZeroException>(()=>expressionParser.ParseExpression("(2 - 1 + 14/0 + 7").Evaluate());
        }
    }
}
