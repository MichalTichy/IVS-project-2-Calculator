namespace Math.Nodes
{
    public class NumberNode : INode
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