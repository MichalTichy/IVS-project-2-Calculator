using System;

namespace Math.Nodes.Values
{
    public class NumberNode : IValueNode
    {

        protected readonly decimal value;

        public NumberNode(decimal value)
        {
            this.value = value;
        }

        public NumberNode(string value)
        {
            this.value = Decimal.Parse(value);
        }

        public INode Parent { get; set; }

        public virtual decimal Evaluate()
        {
            return value;
        }
    }
}