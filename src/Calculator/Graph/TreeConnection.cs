using System.Collections.Generic;

namespace Calculator.Graph
{
	/// <summary>
	/// Represent Node for draw on canvas
	/// </summary>
	public struct TreeConnection
	{
		/// <summary>
		/// node of Parent
		/// </summary>
		public ITreeNode IgnParent { get; private set; }
		/// <summary>
		/// Node of Child
		/// </summary>
		public ITreeNode IgnChild { get; private set; }
		/// <summary>
		/// list point to be connected to node
		/// </summary>
		public List<DPoint> LstPt { get; private set; }

		/// <summary>
		/// Constructor for Connection
		/// </summary>
		/// <param name="ignParent"></param>
		/// <param name="ignChild"></param>
		/// <param name="lstPt"></param>
		public TreeConnection(ITreeNode ignParent, ITreeNode ignChild, List<DPoint> lstPt) : this()
		{
			IgnChild = ignChild;
			IgnParent = ignParent;
			LstPt = lstPt;
		}
	}
}
