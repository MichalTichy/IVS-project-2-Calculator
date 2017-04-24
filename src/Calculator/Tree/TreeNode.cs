using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Calculator.Graph;

namespace Calculator.Tree
{
	/// <summary>
	/// Represent Node for GraphTree
	/// </summary>
	public class TreeNode : ContentControl, ITreeNode
	{
        #region Dependency Properties
        #region Collapsed
        /// <summary>
        /// Register of dependency for custom control
        /// </summary>
        public static readonly DependencyProperty CollapsedProperty = DependencyProperty.Register("Collapsed", typeof(bool), typeof(TreeNode), new PropertyMetadata(null));
            //DependencyProperty.Register(
            //	"Collapsed",
            //	typeof(bool),
            //	typeof(TreeNode),
            //	new FrameworkPropertyMetadata(
            //		false,
            //		FrameworkPropertyMetadataOptions.AffectsMeasure |
            //		FrameworkPropertyMetadataOptions.AffectsArrange |
            //		FrameworkPropertyMetadataOptions.AffectsParentMeasure |
            //		FrameworkPropertyMetadataOptions.AffectsParentArrange |
            //		FrameworkPropertyMetadataOptions.AffectsRender |
            //		0,
            //		CollapsePropertyChange,
            //		CollapsePropertyCoerce,
            //		true
            //	),
            //	null
            //);

	    /// <summary>
	    /// Event for collapsions of nodes
	    /// </summary>
	    /// <param name="o"></param>
	    /// <param name="e"></param>
	    static public void CollapsePropertyChange(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			TreeNode tn = o as TreeNode;
			if (tn != null && tn.Collapsible)
			{
				bool fCollapsed = ((bool)e.NewValue);
				foreach (TreeNode tnCur in LayeredTreeDraw.VisibleDescendants<TreeNode>(tn))
				{
					tnCur.Visibility = fCollapsed ? Visibility.Collapsed : Visibility.Visible;
				}
			}
		}

		public bool Collapsed
		{
			get { return (bool)GetValue(CollapsedProperty); }
			set { SetValue(CollapsedProperty, value); }
		}
        #endregion

        #region Collapsible
        /// <summary>
        /// registration of dependency for custom control
        /// </summary>
        public static readonly DependencyProperty CollapsibleProperty = DependencyProperty.Register("Collapsible", typeof(bool), typeof(TreeNode), new PropertyMetadata(null));
			//DependencyProperty.Register(
			//	"Collapsible",
			//	typeof(bool),
			//	typeof(TreeNode),
			//	new FrameworkPropertyMetadata(
			//		true,
			//		FrameworkPropertyMetadataOptions.AffectsMeasure |
			//		FrameworkPropertyMetadataOptions.AffectsArrange |
			//		FrameworkPropertyMetadataOptions.AffectsParentMeasure |
			//		FrameworkPropertyMetadataOptions.AffectsParentArrange |
			//		FrameworkPropertyMetadataOptions.AffectsRender |
			//		0,
			//		CollapsiblePropertyChange,
			//		null,
			//		true
			//	),
			//	null
			//);

		/// <summary>
		/// Extension for collapsing nodes
		/// </summary>
		/// <param name="o"></param>
		/// <param name="e"></param>
		static public void CollapsiblePropertyChange(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			TreeNode tn = o as TreeNode;
			if (((bool)e.NewValue) == false && tn != null)
			{
				tn.Collapsed = false;
			}
		}

		/// <summary>
		/// flag for collapsible
		/// </summary>
		public bool Collapsible
		{
			get { return (bool)GetValue(CollapsibleProperty); }
			set { SetValue(CollapsibleProperty, value); }
		}
        #endregion

        #region TreeParent
        /// <summary>
        /// Register dependency to customcontrol
        /// </summary>
        public static readonly DependencyProperty TreeParentProperty = DependencyProperty.Register("TreeParent", typeof(string), typeof(TreeNode), new PropertyMetadata(null));
            //DependencyProperty.Register(
            //	"TreeParent",
            //	typeof(string),
            //	typeof(TreeNode),
            //	new FrameworkPropertyMetadata(
            //		null,
            //		FrameworkPropertyMetadataOptions.AffectsMeasure |
            //		FrameworkPropertyMetadataOptions.AffectsArrange |
            //		FrameworkPropertyMetadataOptions.AffectsParentMeasure |
            //		FrameworkPropertyMetadataOptions.AffectsParentArrange |
            //		FrameworkPropertyMetadataOptions.AffectsRender |
            //		0,
            //		null,
            //		null,
            //		true
            //	),
            //	null
            //);

        /// <summary>
        /// Gets Element for container
        /// </summary>
        /// <param name="tn"></param>
        /// <returns></returns>
        public static TreeNode GetParentElement(TreeNode tn)
		{
			TreeContainer tc;
			TreeNode tnParent;

			if (tn == null)
			{
				return null;
			}
			tc = tn.Parent as TreeContainer;
			if (tc == null)
			{
				return null;
			}
			string strParent = tn.TreeParent;
			if (strParent == null)
			{
				return null;
			}

			tnParent = tc.FindName(strParent) as TreeNode;
			if (tnParent == null)
			{
				return null;
			}
			return tnParent;
		}

		/// <summary>
		/// Text repreyentation of parent
		/// </summary>
		public string TreeParent
		{
			get { return (string)GetValue(TreeParentProperty); }
			set { SetValue(TreeParentProperty, value); }
		}
		#endregion
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor for treeNode
		/// </summary>
		public TreeNode()
		{
			TreeChildren = new TreeNodeGroup();
			Background = new SolidColorBrush(Colors.Transparent);
		}

		static TreeNode()
		{
		}
		#endregion

		#region Parenting
		internal void ClearParent()
		{
			TreeChildren = new TreeNodeGroup();
		}

		internal bool SetParent()
		{
			TreeNode tn = GetParentElement(this);
			if (tn == null)
			{
				return false;
			}
			tn.TreeChildren.Add(this);
			return true;
		}
		#endregion

		#region ITreeNode Members
		public object PrivateNodeInfo { get; set; }

		public TreeNodeGroup TreeChildren { get; private set; }

		internal Size NodeSize()
		{
			return DesiredSize;
		}

		public double TreeHeight
		{
			get
			{
				return NodeSize().Height;
			}
		}

		public double TreeWidth
		{
			get
			{
				return NodeSize().Width;
			}
		}
		#endregion
	}
}
