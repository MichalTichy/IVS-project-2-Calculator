using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Calculator.Graph;

namespace Calculator.Tree
{
	/// <summary>
	/// Custom Control of Panel
	/// </summary>
	public class TreeContainer : Panel
	{
		#region Private fields
		LayeredTreeDraw _ltd;
		int _iNextNameSuffix;
		#endregion

		#region Properties
		/// <summary>
		/// List of Connections
		/// </summary>
		public List<TreeConnection> Connections
		{
			get
			{
			    return _ltd?.Connections;
			}
		}
        #endregion

        #region Dependency Properties

        #region Root
        /// <summary>
        /// registring dependencyProperty for customcontrol
        /// </summary>
        public static readonly DependencyProperty RootProperty = DependencyProperty.Register("Root", typeof(string), typeof(TreeContainer), new PropertyMetadata(null));
			//DependencyProperty.Register("Root",	typeof(String),	typeof(TreeContainer),new FrameworkPropertyMetadata(null,FrameworkPropertyMetadataOptions.AffectsMeasure |FrameworkPropertyMetadataOptions.AffectsArrange |	FrameworkPropertyMetadataOptions.AffectsParentMeasure |	FrameworkPropertyMetadataOptions.AffectsParentArrange |FrameworkPropertyMetadataOptions.AffectsRender |	0,null,	null,true),
			//	null
			//);

		/// <summary>
		/// root of Tree
		/// </summary>
		public string Root
		{
			get
			{
				return (string)GetValue(RootProperty);
			}
			set
			{
				SetValue(RootProperty, value);
			}
		}
        #endregion

        #region VerticalJustification
        /// <summary>
        /// registring dependencyProperty for customcontrol
        /// </summary>
        public static readonly DependencyProperty VerticalJustifcationProperty = DependencyProperty.Register("VerticalJustification", typeof(VerticalJustification), typeof(TreeContainer), new PropertyMetadata(null));
			//DependencyProperty.Register(
			//	"VerticalJustification",
			//	typeof(VerticalJustification),
			//	typeof(TreeContainer),
			//	new FrameworkPropertyMetadata(
			//		VerticalJustification.top,
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
		/// size of Nodes in tree
		/// </summary>
		public VerticalJustification VerticalJustification
		{
			get
			{
				return (VerticalJustification)GetValue(VerticalJustifcationProperty);
			}
			set
			{
				SetValue(VerticalJustifcationProperty, value);
			}
		}

        #endregion

        #region VerticalBufferProperty
        /// <summary>
        /// registring dependencyProperty for customcontrol
        /// </summary>
        public static readonly DependencyProperty VerticalBufferProperty = DependencyProperty.Register("VerticalBuffer", typeof(double), typeof(TreeContainer), new PropertyMetadata(null));
        //DependencyProperty.Register(
        //	"VerticalBuffer",
        //	typeof(double),
        //	typeof(TreeContainer),
        //	new FrameworkPropertyMetadata(
        //		10.0,
        //		FrameworkPropertyMetadataOptions.AffectsMeasure |
        //		FrameworkPropertyMetadataOptions.AffectsArrange |
        //		FrameworkPropertyMetadataOptions.AffectsParentMeasure |
        //		FrameworkPropertyMetadataOptions.AffectsParentArrange |
        //		FrameworkPropertyMetadataOptions.AffectsRender |
        //		0,
        //		null,
        //		null,
        //		false
        //	),
        //	null
        //);

        /// <summary>
        /// registring dependencyProperty for customcontrol
        /// </summary>
        public double VerticalBuffer
		{
			get { return (double)GetValue(VerticalBufferProperty); }
			set { SetValue(VerticalBufferProperty, value); }
		}

        #endregion

        #region HorizontalBufferSubtreeProperty
        /// <summary>
        /// set sizeo fo buffer for subTrees
        /// </summary>
        public readonly static DependencyProperty HorizontalBufferSubtreeProperty = DependencyProperty.Register("HorizontalBufferSubtree", typeof(double), typeof(TreeContainer), new PropertyMetadata(null));
            //DependencyProperty.Register(
            //	"HorizontalBufferSubtree",
            //	typeof(double),
            //	typeof(TreeContainer),
            //	new FrameworkPropertyMetadata(
            //		10.0,
            //		FrameworkPropertyMetadataOptions.AffectsMeasure |
            //		FrameworkPropertyMetadataOptions.AffectsArrange |
            //		FrameworkPropertyMetadataOptions.AffectsParentMeasure |
            //		FrameworkPropertyMetadataOptions.AffectsParentArrange |
            //		FrameworkPropertyMetadataOptions.AffectsRender |
            //		0,
            //		null,
            //		null,
            //		false
            //	),
            //	null
            //);

        /// <summary>
        /// 
        /// </summary>
        public double HorizontalBufferSubtree
		{
			get { return (double)GetValue(HorizontalBufferSubtreeProperty); }
			set { SetValue(HorizontalBufferSubtreeProperty, value); }
		}
        #endregion

        #region HorizontalBufferProperty
        /// <summary>
        /// registring dependencyProperty for customcontrol
        /// </summary>
        public readonly static DependencyProperty HorizontalBufferProperty = DependencyProperty.Register("HorizontalBuffer", typeof(double), typeof(TreeContainer), new PropertyMetadata(null));
			//DependencyProperty.Register(
			//	"HorizontalBuffer",
			//	typeof(double),
			//	typeof(TreeContainer),
			//	new  FrameworkPropertyMetadata(
			//	    10.0,
			//	    FrameworkPropertyMetadataOptions.AffectsMeasure |
			//	    FrameworkPropertyMetadataOptions.AffectsArrange |
			//	    FrameworkPropertyMetadataOptions.AffectsParentMeasure |
			//	    FrameworkPropertyMetadataOptions.AffectsParentArrange |
			//	    FrameworkPropertyMetadataOptions.AffectsRender |
			//	    0,
			//	    null,
			//	    null,
			//	    false
			//	),
			//	null
			//);

		/// <summary>
		/// set spaces between nodes
		/// </summary>
		public double HorizontalBuffer
		{
			get { return (double)GetValue(HorizontalBufferProperty); }
			set { SetValue(HorizontalBufferProperty, value); }
		}
		#endregion
		#endregion

		#region Constructors

	    #endregion

        #region Parenting
        private void SetParents(TreeNode tnRoot)
        {
            // First pass to clear all parents
            foreach (UIElement uiel in Children)
            {
                TreeNode tn = uiel as TreeNode;
                if (tn != null)
                {
                    tn.ClearParent();
                }
            }

            // Second pass to properly set them from their children...
            foreach (UIElement uiel in Children)
            {
                TreeNode tn = uiel as TreeNode;
                if (tn != null && tn != tnRoot)
                {
                    tn.SetParent();
                }
            }
        }
        #endregion

        #region Public utilities
        /// <summary>
        /// clear all child in tree
        /// </summary>
        public void Clear()
        {
            //foreach (TreeNode tnCur in Children)
            //{
                
            //    UnregisterName(tnCur.Name);
            //}
            Children.Clear();
        }

        private void SetName(TreeNode tn, string strName)
        {
            tn.Name = strName;
            //RegisterName(strName, tn);
        }

        /// <summary>
        /// Adds node to Tree
        /// </summary>
        /// <param name="objContent"></param>
        /// <param name="strName"></param>
        /// <returns></returns>
        public TreeNode AddRoot(Object objContent, string strName)
		{
			TreeNode tnNew = new TreeNode();
			SetName(tnNew, strName);
			tnNew.Content = objContent;
			Children.Add(tnNew);
			Root = strName;
			return tnNew;
		}

		/// <summary>
		/// Adds new node s to tree
		/// </summary>
		/// <param name="objContent"></param>
		/// <returns></returns>
		public TreeNode AddRoot(Object objContent)
		{
			return AddRoot(objContent, StrNextName());
		}
        
		/// <summary>
		/// adds new nodes to tree
		/// </summary>
		/// <param name="objContent"></param>
		/// <param name="strName"></param>
		/// <param name="strParent"></param>
		/// <returns></returns>
		public TreeNode AddNode(Object objContent, string strName, string strParent)
		{
			TreeNode tnNew = new TreeNode();
			SetName(tnNew, strName);
			tnNew.Content = objContent;
			tnNew.TreeParent = strParent;
			Children.Add(tnNew);
            ReDraw();
            return tnNew;
		}

		private string StrNextName()
		{
			return "__TreeNode" + _iNextNameSuffix++;
		}

		/// <summary>
		/// Adds new nodes to tree
		/// </summary>
		/// <param name="objContent"></param>
		/// <param name="strName"></param>
		/// <param name="tnParent"></param>
		/// <returns></returns>
		public TreeNode AddNode(Object objContent, string strName, TreeNode tnParent)
		{
			return AddNode(objContent, strName, tnParent.Name);
		}

		/// <summary>
		/// Adds new nodes to tre
		/// </summary>
		/// <param name="objContent"></param>
		/// <param name="tnParent"></param>
		/// <returns></returns>
		public TreeNode AddNode(Object objContent, TreeNode tnParent)
		{
			return AddNode(objContent, StrNextName(), tnParent.Name);
		}
        #endregion

        #region Panel overrides
        /// <summary>
        /// ovveride for customcontrol
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            if (Children.Count == 0)
            {
                return new Size(100, 20);
            }

            Size szFinal = new Size(0, 0);
            string strRoot = Root;
            TreeNode tnRoot = FindName(strRoot) as TreeNode;

            foreach (UIElement uiel in Children)
            {
                uiel.Measure(availableSize);
                Size szThis = uiel.DesiredSize;

                if (szThis.Width > szFinal.Width || szThis.Height > szFinal.Height)
                {
                    szFinal = new Size(
                        System.Math.Max(szThis.Width, szFinal.Width),
                        System.Math.Max(szThis.Height, szFinal.Height));
                }
            }

            if (tnRoot != null)
            {
                SetParents(tnRoot);
                _ltd = new LayeredTreeDraw(tnRoot, HorizontalBuffer, HorizontalBufferSubtree, VerticalBuffer, VerticalJustification.Top);
                _ltd.LayoutTree();
                szFinal = new Size(_ltd.PxOverallWidth, _ltd.PxOverallHeight);
            }

            return szFinal;
        }

        /// <summary>
        /// size of panel
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement uiel in Children)
            {
                TreeNode tn = uiel as TreeNode;
                Point ptLocation = new Point(0, 0);
                if (tn != null)
                {
                    ptLocation = new Point(_ltd.X(tn), _ltd.Y(tn));
                }
                uiel.Arrange(new Rect(ptLocation, uiel.DesiredSize));
            }

            return finalSize;
        }
        #endregion

        #region Connection Rendering
        static Point PtFromDPoint(DPoint dpt)
		{
			return new Point(dpt.X, dpt.Y);
		}

        /// <summary>
        /// Redraws all connections of tree
        /// </summary>
        public void ReDraw()
        {
            if (Connections != null)
            {
                SolidColorBrush brsh = new SolidColorBrush(Colors.Black);
                brsh.Opacity = 0.5;


                // ReSharper disable once ObjectCreationAsStatement
                new Point(120, 120);
                bool fHaveLastPoint;

                foreach (TreeConnection tcn in Connections)
                {
                    fHaveLastPoint = false;
                    foreach (DPoint dpt in tcn.LstPt)
                    {
                        if (!fHaveLastPoint)
                        {
                            PtFromDPoint(tcn.LstPt[0]);
                            fHaveLastPoint = true;
                            continue;
                        }
                        Line pen = new Line
                        {
                            Stroke = brsh,
                            X1 = PtFromDPoint(tcn.LstPt[0]).X,
                            Y1 = PtFromDPoint(tcn.LstPt[0]).Y,
                            X2 = PtFromDPoint(dpt).X,
                            Y2 = PtFromDPoint(dpt).Y
                        };
                        Children.Add(pen);
                        PtFromDPoint(dpt);
                    }
                }
            }
        }
        //protected override void OnLoad(Windows.UI.Xaml.Media.DrawingContext dc)
        //{
        //    base.OnRender(dc);
        //    if (Connections != null)
        //    {
        //        SolidColorBrush brsh = new SolidColorBrush(Colors.Black);
        //        brsh.Opacity = 0.5;


        //        Pen pen = new Pen(brsh, 1.0);
        //        Point ptLast = new Point(0, 0);
        //        bool fHaveLastPoint = false;

        //        foreach (TreeConnection tcn in Connections)
        //        {
        //            fHaveLastPoint = false;
        //            foreach (DPoint dpt in tcn.LstPt)
        //            {
        //                if (!fHaveLastPoint)
        //                {
        //                    ptLast = PtFromDPoint(tcn.LstPt[0]);
        //                    fHaveLastPoint = true;
        //                    continue;
        //                }
        //                dc.DrawLine(pen, PtFromDPoint(dpt), ptLast);
        //                ptLast = PtFromDPoint(dpt);
        //            }
        //        }
        //    }
        //}
        #endregion
    }
}
