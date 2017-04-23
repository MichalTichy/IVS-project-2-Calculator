using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Math.ExpressionTreeBuilder;
using Math.Nodes;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Functions.Unary;
using Math.Nodes.Values;
using Math.Tokenizer;
using Math = System.Math;

namespace Proffiling
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildStandartDeviationNodeTree(new[] {1.0m, 2.0m,3.0m}).Evaluate();
        }
        public static INode BuildStandartDeviationNodeTree(decimal[] inputs)
        {
            var parser = new ExpressionTreeBuilder<Tokenizer>();
            var averageTree = BuildAverageNodeTree(inputs);
            var average = averageTree.Evaluate();

            var sqrt=new SqrtNode();
            
            var multiplication=new MultiplyNode();
            sqrt.ChildNode = multiplication;

            multiplication.LeftNode = parser.ParseExpression($"1/({inputs.Length}-1)");

            var powNodes = inputs.Select(
                t => parser.ParseExpression($"({t} - {average})^2"));

            multiplication.RightNode = BuildSumNodeTree(powNodes.ToArray());

            return sqrt;
        }

        public static INode BuildSumNodeTree(INode[] inputs)
        {
            SumNode root=new SumNode();
            SumNode currentNode = root;

            for (int i = 0; i < inputs.Count() - 1; i++)
            {
                currentNode.RightNode = inputs[i];
                if (i != inputs.Count() - 2)
                {
                    var sumNode = new SumNode();
                    currentNode.LeftNode = sumNode;
                    currentNode = sumNode;
                }

            }
            currentNode.LeftNode = inputs.Last();

            return root;

        }

        public static INode BuildAverageNodeTree(decimal[] inputs)
        {
            var division = new DivisionNode
            {
                RightNode = new NumberNode(inputs.Count()),
                LeftNode = BuildSumNodeTree(inputs.Select(t => new NumberNode(t)).ToArray())
            };

            return division;
        }
        
    }
}