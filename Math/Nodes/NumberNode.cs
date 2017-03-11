namespace Math.Nodes
{
    public class NumberNode : INode
    {
        public virtual string TextRepresentation => value.ToString();

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