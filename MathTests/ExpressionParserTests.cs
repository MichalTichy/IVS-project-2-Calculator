using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Math;
using Math.ExpressionTreeBuilder;
using Math.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math.Nodes.Values;

namespace MathTests
{
    public class ExpressionParserTests
    {
        [TestMethod]
        public void EqualityTest()
        {
            NumberNode n1 = new NumberNode(3);
            NumberNode n2 = new NumberNode(3);
            Assert.AreNotEqual(n1, n2);
            Assert.AreNotSame(n1, n2);
            Assert.AreNotEqual(n1.GetHashCode() , n2.GetHashCode());
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

        [TestMethod]
        public void FollowingBracketInPowTest()
        {
            var expressionParser = new ExpressionTreeBuilder<Tokenizer>();
            Assert.AreEqual(2, expressionParser.ParseExpression("2^(4/4)").Evaluate());
        }

        [TestMethod]
        public void CosTest()
        {
            var expressionParser = new ExpressionTreeBuilder<Tokenizer>();
            Assert.AreEqual(0, expressionParser.ParseExpression("cos 90").Evaluate());
        }

        [TestMethod]
        public void CosWithMultiplyTest()
        {
            var expressionParser = new ExpressionTreeBuilder<Tokenizer>();
            Assert.AreEqual(0, expressionParser.ParseExpression("2 * cos 90").Evaluate());
        }

    }
}
