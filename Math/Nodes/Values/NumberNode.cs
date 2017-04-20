using System;

namespace Math.Nodes.Values
{
    public class NumberNode : IValueNode
    {

        protected readonly decimal value;
        public Guid Gid { get; set; }

        public NumberNode(decimal value)
        {
            this.value = value;
            Gid = Guid.NewGuid();
        }

        public NumberNode(string value)
        {
            this.value = Decimal.Parse(value);
            Gid = Guid.NewGuid();
        }

        public INode Parent { get; set; }

        public virtual decimal Evaluate()
        {
            return value;
        }

        public static bool IsNumber(string text)
        {
            return decimal.TryParse(text, out var result);
        }
    }
}