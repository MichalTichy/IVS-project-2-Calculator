using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Nodes
{
    public class TemporaryNode: INode
    {
        public decimal Evaluate()
        {
            throw new NotSupportedException();
        }
    }
}
