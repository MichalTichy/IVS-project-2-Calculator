namespace Math
{
    public class NumberNode : INode
    {
        public string TextRepresentation => value.ToString();

        protected readonly double value;

        public NumberNode(double value)
        {
            this.value = value;
        }

        public double Evaluate()
        {
            return value;
        }
    }
}