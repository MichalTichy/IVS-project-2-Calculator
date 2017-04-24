using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Math.ExpressionTreeBuilder;
using Math.Nodes;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Functions.Unary;
using Math.Nodes.Values;
using Math.Tokenizer;

namespace Proffiling
{
    class Program
    {
        static void Main(string[] args)
        {
            //Waiting for proffiler to catch up
            Thread.Sleep(20000);

            var dataProvider = new DataProvider();
            var standartDeviationTreeBuilder=new StandartDeviationTreeBuilder();

            //TEST 1 - 10 inputs
            standartDeviationTreeBuilder.BuildStandartDeviationNodeTree(dataProvider.GetData(10)).Evaluate();
            //TEST 1 - 100 inputs
            standartDeviationTreeBuilder.BuildStandartDeviationNodeTree(dataProvider.GetData(100)).Evaluate();
            //TEST 1 - 1000 inputs
            standartDeviationTreeBuilder.BuildStandartDeviationNodeTree(dataProvider.GetData(1000)).Evaluate();

        }
    }
}