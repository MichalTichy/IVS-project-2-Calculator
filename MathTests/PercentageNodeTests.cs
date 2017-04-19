using System;
using Math;
using Math.ExpressionTreeBuilder;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Functions.Unary;
using Math.Nodes.Values;
using Math.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class PercentageNodeTests
    {
        [TestMethod]
        public void PercentageToFractionTest()
        {
            var p300 = new PercentageNode(){ChildNode = new NumberNode(300)};
            Assert.AreEqual(3,p300.Evaluate());

            var p100 = new PercentageNode(){ChildNode = new NumberNode(100)};
            Assert.AreEqual(1,p100.Evaluate());

            var p50 = new PercentageNode(){ChildNode = new NumberNode(50)};
            Assert.AreEqual(0.5m,p50.Evaluate());

            var p25 = new PercentageNode(){ChildNode = new NumberNode(25)};
            Assert.AreEqual(0.25m,p25.Evaluate());

            var p1 = new PercentageNode(){ChildNode = new NumberNode(1)};
            Assert.AreEqual(0.01m,p1.Evaluate());

            var p0andHalf= new PercentageNode(){ChildNode = new NumberNode(0.5m)};
            Assert.AreEqual(0.005m, p0andHalf.Evaluate());

            var p0 = new PercentageNode() {ChildNode = new NumberNode(0)};
            Assert.AreEqual(0, p0.Evaluate());
        }

        [TestMethod]
        public void PercentageSum()
        {
            var expression = new ExpressionTreeBuilder<Tokenizer>().ParseExpression("100+50%");
            Assert.AreEqual(150,expression.Evaluate());

            var expression2 = new ExpressionTreeBuilder<Tokenizer>().ParseExpression("50%+100");
            Assert.AreEqual(100.5m,expression2.Evaluate());
        }


        [TestMethod]
        public void PercentageMultiply()
        {
            var expression = new ExpressionTreeBuilder<Tokenizer>().ParseExpression("100*50%");
            Assert.AreEqual(50, expression.Evaluate());
        }



        [TestMethod]
        public void Percentage0()
        {
            var expression = new ExpressionTreeBuilder<Tokenizer>().ParseExpression("100*0%");
            Assert.AreEqual(0, expression.Evaluate());
        }

        [TestMethod]
        public void Percentage150()
        {
            var expression = new ExpressionTreeBuilder<Tokenizer>().ParseExpression("100*150%");
            Assert.AreEqual(150, expression.Evaluate());
        }


        [TestMethod]
        public void PercentageInUnaryOperation()
        {
            var expression = new ExpressionTreeBuilder<Tokenizer>().ParseExpression("300%!");
            Assert.AreEqual(6, expression.Evaluate());
        }



        [TestMethod]
        public void CalculatedPercentageValue()
        {
            var expression = new ExpressionTreeBuilder<Tokenizer>().ParseExpression("100*(25*2)%");
            Assert.AreEqual(50, expression.Evaluate());
        }
    }

}
