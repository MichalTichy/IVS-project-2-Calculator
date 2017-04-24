using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Calculator.Graph
{
	/// <summary>
	/// Represent collection of nodes which are associated with parrent node
	/// </summary>
	public class TreeNodeGroup : IEnumerable<ITreeNode>
	{
	    private readonly Collection<ITreeNode> _col = new Collection<ITreeNode>();

		/// <summary>
		/// count of items in collection
		/// </summary>
		public int Count
		{
			get
			{
				return _col.Count;
			}
		}

		/// <summary>
		/// for IEnumerable
		/// </summary>
		/// <param name="index"></param>
		public ITreeNode this[int index] => _col[index];

	    /// <summary>
	    /// Adding Node to Connections
	    /// </summary>
	    /// <param name="tn"></param>
	    public void Add(ITreeNode tn)
		{
			_col.Add(tn);
		}

		internal ITreeNode LeftMost()
		{
			return _col.First();
		}

		internal ITreeNode RightMost()
		{
			return _col.Last();
		}

		#region IEnumerable<IGraphNode> Members

		public IEnumerator<ITreeNode> GetEnumerator()
		{
			return _col.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _col.GetEnumerator();
		}

		#endregion
	}
}
