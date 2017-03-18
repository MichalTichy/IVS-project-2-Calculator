namespace Math.Nodes.Values
{
    public class NumberNode : IValueNode
    {

        protected readonly decimal value;

        public NumberNode(decimal value)
        {
            this.value = value;
        }

        public virtual decimal Evaluate()
        {
            return value;
        }
    }
}