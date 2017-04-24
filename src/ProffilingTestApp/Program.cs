using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProffilingTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataProvider = new RandomDataProvider();
            var standartDeviationTreeBuilder = new StandartDeviationTreeBuilder();

            //TEST 1 - 10 inputs
            //standartDeviationTreeBuilder.BuildStandartDeviationNodeTree(dataProvider.GetData(10)).Evaluate();
            //TEST 1 - 100 inputs
            //standartDeviationTreeBuilder.BuildStandartDeviationNodeTree(dataProvider.GetData(100)).Evaluate();
            ////TEST 1 - 1000 inputs
            standartDeviationTreeBuilder.BuildStandartDeviationNodeTree(dataProvider.GetData(1000)).Evaluate();
        }
    }
}
