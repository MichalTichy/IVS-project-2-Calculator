using System;

namespace Math.Nodes
{
    public interface INode
    {
        INode Parent { get; set; }
        decimal Evaluate();

        Guid Gid { get; set; }
    }
}