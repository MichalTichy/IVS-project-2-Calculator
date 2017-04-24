using System;
using Math.ExpressionTreeBuilder;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Functions.Unary;
using Math.Nodes.Values;
using Math.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math = System.Math;

namespace MathTests
{
    [TestClass]
    public class RandomNodeTests
    {
        [TestMethod]
        public void GenerateRandomNumbersTest()
        {
            var parser=new ExpressionTreeBuilder<Tokenizer>();
            var random=new Random();
            for (int i = 0; i < 10000; i++)
            {
                var min = random.Next(100);
                var max = min + 1 + System.Math.Abs(random.Next(100));
                var result = parser.ParseExpression($"{min}rnd{max}").Evaluate();
                Assert.IsTrue(result>min && result<max);
            }
        }
    }

}
