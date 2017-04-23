using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Nodes.Functions.Unary
{
    /// <summary>
    /// Node used to calculate cosinus.
    /// </summary>
    public class CosNode : IPrecedingUnaryOperationNode
    {
        /// <inheritdoc />
        public INode Parent { get; set; }

        /// <inheritdoc />
        public INode ChildNode { get; set; }

        /// <inheritdoc />
        public decimal Evaluate()
        {
            decimal nodeValue = ChildNode.Evaluate();
            double cos = System.Math.Cos((double) nodeValue * (System.Math.PI / 180.0));

            return System.Math.Round((decimal) cos, 14, MidpointRounding.AwayFromZero);
        }
    }
}