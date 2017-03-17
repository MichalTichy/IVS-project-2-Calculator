using System;
using System.Collections.Generic;
using System.Linq;
using Math.Nodes.Functions;
using Math.Nodes.Functions.Binary;
using System.Reflection;

namespace Math
{
    public class ExpressionParser
    {
        protected SortedDictionary<string,MathOperatorDescription> registeredOperators = new SortedDictionary<string,MathOperatorDescription>();
        public IReadOnlyCollection<MathOperatorDescription> RegisteredOperators => registeredOperators.Values.ToList().AsReadOnly();

        public void RegisterOperator(MathOperatorDescription mathOperatorDescription)
        {
            if (registeredOperators.ContainsKey(mathOperatorDescription.TextRepresentation))
                throw new ArgumentException("Operator with same text representation is allready registered.");

            registeredOperators.Add(mathOperatorDescription.TextRepresentation,mathOperatorDescription);
        }

        public ExpressionParser()
        {
            RegisterDefaultOperators();
        }

        protected void RegisterDefaultOperators()
        {
            RegisterOperator(new MathOperatorDescription(typeof(SumNode), "+",OperationType.LowPriorityOperation));
            RegisterOperator(new MathOperatorDescription(typeof(SubstractionNode),"-",OperationType.LowPriorityOperation));
            RegisterOperator(new MathOperatorDescription(typeof(MultiplyNode),"*",OperationType.HighPriorityOperation));
        }
    }
}
