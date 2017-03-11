namespace Math.Nodes
{
    public class NumberNode : INode
    {
        protected readonly double value;

        public NumberNode(double value)
        {
            this.value = value;
        }

        public virtual double Evaluate()
        {
            return value;
        }
    }
}