namespace Calculator.Graph
{
	/// <summary>
	/// Interface for TreeNode
	/// </summary>
	public interface ITreeNode
	{
		// PrivateNodeInfo is a cookie used by GraphLayout to keep track of information on
		// a per node basis.  The ITreeNode implementer just has to provide a way to
		// save and retrieve this cookie.
		/// <summary>
		/// Cookie
		/// </summary>
		object PrivateNodeInfo { get; set;  }

		/// <summary>
		/// collection of children of tree
		/// </summary>
		TreeNodeGroup TreeChildren { get; }
		/// <summary>
		/// size of tree
		/// </summary>
		double TreeWidth { get; }
		/// <summary>
		/// size of tree
		/// </summary>
		double TreeHeight { get; }
		/// <summary>
		/// extension for collapsing of nodes
		/// </summary>
		bool Collapsed { get; }
	}
}
