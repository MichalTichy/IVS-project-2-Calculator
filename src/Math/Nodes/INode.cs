using System;

namespace Math.Nodes
{
    /// <summary>
    /// Base node interface
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// Parent node
        /// </summary>
        INode Parent { get; set; }

        /// <summary>
        /// Calculates value of node.
        /// </summary>
        /// <returns>Value of node</returns>
        decimal Evaluate();
    }
}