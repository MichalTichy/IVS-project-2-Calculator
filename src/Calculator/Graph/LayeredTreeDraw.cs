#define FIX
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Graph
{
	#region Enums
	/// <summary>
	/// Justification of connections between nodes in Tree
	/// </summary>
	public enum VerticalJustification
	{
		/// <summary>
		/// top point
		/// </summary>
		Top,
		/// <summary>
		/// center justification of text
		/// </summary>
		Center,
		/// <summary>
		/// bottom point
		/// </summary>
		Bottom
	}
	#endregion
	/// <summary>
	/// Build Tree overlay
	/// </summary>
	public class LayeredTreeDraw
	{

		#region Private variables

	    readonly ITreeNode _tnRoot;
	    readonly double _pxBufferHorizontal;
	    readonly double _pxBufferHorizontalSubtree;
	    readonly double _pxBufferVertical;
	    readonly List<TreeConnection> _lsttcn = new List<TreeConnection>();
	    readonly List<double> _lstLayerHeight = new List<double>();
	    readonly VerticalJustification _vj;
		static readonly TreeNodeGroup TngEmpty = new TreeNodeGroup();
		#endregion

		#region Properties
		/// <summary>
		/// Size of whole tree
		/// </summary>
		public double PxOverallHeight { get; private set;  }

		/// <summary>
		/// size of whole tree
		/// </summary>
		public double PxOverallWidth => Info(_tnRoot).SubTreeWidth;

	    /// <summary>
	    /// List storing connection information of all nodes
	    /// </summary>
	    public List<TreeConnection> Connections => _lsttcn;

	    #endregion

		#region Constructor
		/// <summary>
		/// Init constructor
		/// </summary>
		/// <param name="tnRoot"></param>
		/// <param name="pxBufferHorizontal"></param>
		/// <param name="pxBufferHorizontalSubtree"></param>
		/// <param name="pxBufferVertical"></param>
		/// <param name="vj"></param>
		public LayeredTreeDraw(
			ITreeNode tnRoot,
			double pxBufferHorizontal,
			double pxBufferHorizontalSubtree,
			double pxBufferVertical,
			VerticalJustification vj)
		{
			_pxBufferHorizontal = pxBufferHorizontal;
			_pxBufferHorizontalSubtree = pxBufferHorizontalSubtree;
			_pxBufferVertical = pxBufferVertical;
			PxOverallHeight = 0.0;
			_tnRoot = tnRoot;
			_vj = vj;
		}
		#endregion

		#region PrivateInfo Access
		private static LayeredTreeInfo Info(ITreeNode ign)
		{
			return (LayeredTreeInfo)ign.PrivateNodeInfo;
		}

		/// <summary>
		/// x position
		/// </summary>
		/// <param name="tn"></param>
		/// <returns></returns>
		public double X(ITreeNode tn)
		{
			if (Info(tn) == null)
			{
				return 0;
			}
			return Info(tn).PxFromLeft;
		}

		/// <summary>
		/// y position
		/// </summary>
		/// <param name="tn"></param>
		/// <returns></returns>
		public double Y(ITreeNode tn)
		{
			if (Info(tn) == null)
			{
				return 0;
			}
			return Info(tn).PxFromTop;
		}
		#endregion

		#region Enumerations over nodes
		/// <summary>
		/// extension for Collapsable
		/// </summary>
		/// <param name="tn">TreeNode instance</param>
		/// <typeparam name="T">T for Type</typeparam>
		/// <returns></returns>
		public static IEnumerable<T> VisibleDescendants<T>(ITreeNode tn)
		{
			foreach (var tnCur in tn.TreeChildren)
			{
				if (!tnCur.Collapsed)
				{
					foreach (var item in VisibleDescendants<T>(tnCur))
					{
						yield return item;
					}
				}
				yield return (T)tnCur;
			}
		}


		/// <summary>
		/// Extension for Collapsables
		/// </summary>
		/// <param name="tn"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static IEnumerable<T> Descendants<T>(ITreeNode tn)
		{
			foreach (var tnCur in tn.TreeChildren)
			{
				foreach (var item in Descendants<T>(tnCur))
				{
					yield return item;
				}
				yield return (T)tnCur;
			}
		}
		#endregion

		#region Layout
		#region Top Level Layout routines
		/// <summary>
		/// make layout Tree
		/// </summary>
		public void LayoutTree()
		{
			LayoutTree(_tnRoot, 0);
			DetermineFinalPositions(_tnRoot, 0, 0, Info(_tnRoot).PxLeftPosRelativeToBoundingBox);
		}

		private void LayoutTree(ITreeNode tnRoot, int iLayer)
		{
			if (GetChildren(tnRoot).Count == 0)
			{
				LayoutLeafNode(tnRoot);
			}
			else
			{
				LayoutInteriorNode(tnRoot, iLayer);
			}

			UpdateLayerHeight(tnRoot, iLayer);
		}

		private static void LayoutLeafNode(ITreeNode tnRoot)
		{
			double width = tnRoot.TreeWidth;
			LayeredTreeInfo lti = new LayeredTreeInfo(width);
			lti.LstPosLeftBoundaryRelativeToRoot.Add(0);
			lti.LstPosRightBoundaryRelativeToRoot.Add(width);
			tnRoot.PrivateNodeInfo = lti;
		}

		private void LayoutInteriorNode(ITreeNode tnRoot, int iLayer)
		{
		    TreeNodeGroup tng = GetChildren(tnRoot);

		    LayoutAllOurChildren(iLayer, tng);

			// This width doesn't account for the parent node's width...
			var ltiThis = new LayeredTreeInfo(CalculateWidthFromInterChildDistances(tnRoot));
			tnRoot.PrivateNodeInfo = ltiThis;

			// ...so that this centering may place the parent node negatively while the "width" is the width of
			// all the child nodes.
			CenterOverChildren(tnRoot, ltiThis);
			DetermineParentRelativePositionsOfChildren(tnRoot);
			CalculateBoundaryLists(tnRoot);
		}

		private void LayoutAllOurChildren(int iLayer, TreeNodeGroup tng)
		{
			List<Double> lstLeftToBb = new List<double>();
			List<int> lstResponsible = new List<int>();
			for (int i = 0; i < tng.Count; i++)
			{
				ITreeNode tn = tng[i];
				LayoutTree(tn, iLayer + 1);
				RepositionSubtree(i, tng, lstLeftToBb, lstResponsible);
			}
		}
		#endregion

		#region Parent Relative Positioning
		private static void CenterOverChildren(ITreeNode tnRoot, LayeredTreeInfo ltiThis)
		{
			// We should be centered between  the connection points of our children...
			var tnLeftMost = tnRoot.TreeChildren.LeftMost();
			var pxLeftChild = Info(tnLeftMost).PxLeftPosRelativeToBoundingBox + tnLeftMost.TreeWidth / 2;
			var tnRightMost = tnRoot.TreeChildren.RightMost();
			var pxRightChild = Info(tnRightMost).PxLeftPosRelativeToBoundingBox + tnRightMost.TreeWidth / 2;
			ltiThis.PxLeftPosRelativeToBoundingBox = (pxLeftChild + pxRightChild - tnRoot.TreeWidth) / 2;

			// If the root node was wider than the subtree, then we'll have a negative position for it.  We need
			// to readjust things so that the left of the root node represents the left of the bounding box and
			// the child distances to the Bounding box need to be adjusted accordingly.
		    if (!(ltiThis.PxLeftPosRelativeToBoundingBox < 0)) return;
		    foreach (var tnChildCur in tnRoot.TreeChildren)
		    {
		        Info(tnChildCur).PxLeftPosRelativeToBoundingBox -= ltiThis.PxLeftPosRelativeToBoundingBox;
		    }
		    ltiThis.PxLeftPosRelativeToBoundingBox = 0;
		}

		private void DetermineParentRelativePositionsOfChildren(ITreeNode tnRoot)
		{
			var ltiRoot = Info(tnRoot);
			foreach (ITreeNode tn in GetChildren(tnRoot))
			{
				LayeredTreeInfo ltiCur = Info(tn);
				ltiCur.PxLeftPosRelativeToParent = ltiCur.PxLeftPosRelativeToBoundingBox - ltiRoot.PxLeftPosRelativeToBoundingBox;
			}
		}
		#endregion

		#region Width Calculation
		private double CalculateWidthFromInterChildDistances(ITreeNode tnRoot)
		{
			double pxWidthCur;
			LayeredTreeInfo lti;
			double pxWidth = 0.0;

			lti = Info(tnRoot.TreeChildren.LeftMost());
			pxWidthCur = lti.PxLeftPosRelativeToBoundingBox;

			// If a subtree extends deeper than it's left neighbors then at that lower level it could potentially extend beyond those neighbors
			// on the left.  We have to check for this and make adjustements after the loop if it occurred.
			double pxUndercut = 0.0;

			foreach (ITreeNode tn in tnRoot.TreeChildren)
			{
				lti = Info(tn);
				pxWidthCur += lti.PxToLeftSibling;

				if (lti.PxLeftPosRelativeToBoundingBox > pxWidthCur)
				{
					pxUndercut = System.Math.Max(pxUndercut, lti.PxLeftPosRelativeToBoundingBox - pxWidthCur);
				}
				
				// pxWidth might already be wider than the current node's subtree if earlier nodes "undercut" on the
				// right hand side so we have to take the Max here...
				pxWidth = System.Math.Max(pxWidth, pxWidthCur + lti.SubTreeWidth - lti.PxLeftPosRelativeToBoundingBox);

				// After this next statement, the BoundingBox we're relative to is the one of our parent's subtree rather than
				// our own subtree (with the exception of undercut considerations)
				lti.PxLeftPosRelativeToBoundingBox = pxWidthCur;
			}
			if (pxUndercut > 0.0)
			{
				foreach (ITreeNode tn in tnRoot.TreeChildren)
				{
					Info(tn).PxLeftPosRelativeToBoundingBox += pxUndercut;
				}
				pxWidth += pxUndercut;
			}

			// We are never narrower than our root node's width which we haven't taken into account yet so
			// we do that here.
			return System.Math.Max(tnRoot.TreeWidth, pxWidth);
		}
		#endregion

		#region Boundary Lists
		private void CalculateBoundaryLists(ITreeNode tnRoot)
		{
			LayeredTreeInfo lti = Info(tnRoot);
			lti.LstPosLeftBoundaryRelativeToRoot.Add(0.0);
			lti.LstPosRightBoundaryRelativeToRoot.Add(tnRoot.TreeWidth);
			DetermineBoundary(tnRoot.TreeChildren, true /* fLeft */, lti.LstPosLeftBoundaryRelativeToRoot);
			DetermineBoundary(tnRoot.TreeChildren.Reverse(), false /* fLeft */, lti.LstPosRightBoundaryRelativeToRoot);

		}

		private void DetermineBoundary(IEnumerable<ITreeNode> entn, bool fLeft, List<double> lstPos)
		{
			int cLayersDeep = 1;
			List<double> lstPosCur;
			foreach (ITreeNode tnChild in entn)
			{
				LayeredTreeInfo ltiChild = Info(tnChild);

				if (fLeft)
				{
					lstPosCur = ltiChild.LstPosLeftBoundaryRelativeToRoot;
				}
				else
				{
					lstPosCur = ltiChild.LstPosRightBoundaryRelativeToRoot;
				}

				if (lstPosCur.Count >= lstPos.Count)
				{
					using (IEnumerator<double> enPosCur = lstPosCur.GetEnumerator())
					{
						for (int i = 0; i < cLayersDeep - 1; i++)
						{
							enPosCur.MoveNext();
						}

						while (enPosCur.MoveNext())
						{
							lstPos.Add(enPosCur.Current + ltiChild.PxLeftPosRelativeToParent);
							cLayersDeep++;
						}
					}
				}
			}
		}
		#endregion

		#region Repositioning Children
		private void ApportionSlop(int itn, int itnResponsible, TreeNodeGroup tngSiblings)
		{
			LayeredTreeInfo lti = Info(tngSiblings[itn]);
			ITreeNode tnLeft = tngSiblings[itn - 1];

			double pxSlop = lti.PxToLeftSibling - tnLeft.TreeWidth - _pxBufferHorizontal;
			if (pxSlop > 0)
			{
				for (int i = itnResponsible + 1; i < itn; i++)
				{
					Info(tngSiblings[i]).PxToLeftSibling += pxSlop * (i - itnResponsible) / (itn - itnResponsible);
				}
				lti.PxToLeftSibling -= (itn - itnResponsible - 1) * pxSlop / (itn - itnResponsible);
			}
		}

		private void RepositionSubtree(
			int itn,
			TreeNodeGroup tngSiblings,
			List<double> lstLeftToBb,
			List<int> lsttnResponsible)
		{
			int itnResponsible;
			ITreeNode tn = tngSiblings[itn];
			LayeredTreeInfo lti = Info(tn);

			if (itn == 0)
			{
				// No shifting but we still have to prepare the initial version of the
				// left hand skeleton list
				foreach (double pxRelativeToRoot in lti.LstPosRightBoundaryRelativeToRoot)
				{
					lstLeftToBb.Add(pxRelativeToRoot + lti.PxLeftPosRelativeToBoundingBox);
					lsttnResponsible.Add(0);
				}
				return;
			}

			ITreeNode tnLeft = tngSiblings[itn - 1];
			Info(tnLeft);
			int iLayer;
			double pxHorizontalBuffer = _pxBufferHorizontal;

			double pxNewPosFromBb = PxCalculateNewPos(lti, lstLeftToBb, lsttnResponsible, out itnResponsible, out iLayer);
			if (iLayer != 0)
			{
				pxHorizontalBuffer = _pxBufferHorizontalSubtree;
			}

			lti.PxToLeftSibling = pxNewPosFromBb - lstLeftToBb.First() + tnLeft.TreeWidth + pxHorizontalBuffer;

			int cLevels = System.Math.Min(lti.LstPosRightBoundaryRelativeToRoot.Count, lstLeftToBb.Count);
			for (int i = 0; i < cLevels; i++)
			{
				lstLeftToBb[i] = lti.LstPosRightBoundaryRelativeToRoot[i] + pxNewPosFromBb + pxHorizontalBuffer;
				lsttnResponsible[i] = itn;
			}
			for (int i = lstLeftToBb.Count; i < lti.LstPosRightBoundaryRelativeToRoot.Count; i++)
			{
				lstLeftToBb.Add(lti.LstPosRightBoundaryRelativeToRoot[i] + pxNewPosFromBb + pxHorizontalBuffer);
				lsttnResponsible.Add(itn);
			}

			ApportionSlop(itn, itnResponsible, tngSiblings);
		}

		private double PxCalculateNewPos(
			LayeredTreeInfo lti,
			List<double> lstLeftToBb,
			List<int> lstitnResponsible,
			out int itnResponsible,
			out int iLayerRet)
		{
		    int cLayers = System.Math.Min(lti.LstPosLeftBoundaryRelativeToRoot.Count, lstLeftToBb.Count);
			double pxRootPosRightmost = 0.0;
			iLayerRet = 0;

			using (IEnumerator<double> enRight = lti.LstPosLeftBoundaryRelativeToRoot.GetEnumerator(),
				enLeft = lstLeftToBb.GetEnumerator())
			using (IEnumerator<int> enResponsible = lstitnResponsible.GetEnumerator())
			{
				itnResponsible = -1;

				enRight.MoveNext();
				enLeft.MoveNext();
				enResponsible.MoveNext();
				for (int iLayer = 0; iLayer < cLayers; iLayer++)
				{
					double pxLeftBorderFromBb = enLeft.Current;
					double pxRightBorderFromRoot = enRight.Current;
					double pxRightRootBasedOnThisLevel;
					int itnResponsibleCur = enResponsible.Current;

					enLeft.MoveNext();
					enRight.MoveNext();
					enResponsible.MoveNext();

					pxRightRootBasedOnThisLevel = pxLeftBorderFromBb - pxRightBorderFromRoot;
					if (pxRightRootBasedOnThisLevel > pxRootPosRightmost)
					{
						iLayerRet = iLayer;
						pxRootPosRightmost = pxRightRootBasedOnThisLevel;
						itnResponsible = itnResponsibleCur;
					}
				}
			}

			return pxRootPosRightmost;
		}
		#endregion

		#region Height Calculations
		private void UpdateLayerHeight(ITreeNode tnRoot, int iLayer)
		{
			while (_lstLayerHeight.Count <= iLayer)
			{
				_lstLayerHeight.Add(0.0);
			}
			_lstLayerHeight[iLayer] = System.Math.Max(tnRoot.TreeHeight, _lstLayerHeight[iLayer]);
		}

		private double CalcJustify(double height, double pxRowHeight)
		{
			double dRet = 0.0;

			switch (_vj)
			{
				case VerticalJustification.Top:
					break;

				case VerticalJustification.Center:
					dRet = (pxRowHeight - height) / 2;
					break;

				case VerticalJustification.Bottom:
					dRet = pxRowHeight - height;
					break;
			}

			return dRet;
		}
		#endregion

		#region Collapse handling
		private TreeNodeGroup GetChildren(ITreeNode tn)
		{
			if (tn.Collapsed)
			{
				return TngEmpty;
			}
			return tn.TreeChildren;
		}
		#endregion

		#region Second pass to convert parent relative positions to absolute positions
		private void DetermineFinalPositions(ITreeNode tn, int iLayer, double pxFromTop, double pxParentFromLeft)
		{
			double pxRowHeight = _lstLayerHeight[iLayer];
			LayeredTreeInfo lti = Info(tn);
			double pxBottom;
			DPoint dptOrigin;

			lti.PxFromTop = pxFromTop + CalcJustify(tn.TreeHeight, pxRowHeight);
			pxBottom = lti.PxFromTop + tn.TreeHeight;
			if (pxBottom > PxOverallHeight)
			{
				PxOverallHeight = pxBottom;
			}
			lti.PxFromLeft = lti.PxLeftPosRelativeToParent + pxParentFromLeft;
			dptOrigin = new DPoint(lti.PxFromLeft + tn.TreeWidth / 2, lti.PxFromTop + tn.TreeHeight);
			iLayer++;
			foreach (ITreeNode tnCur in GetChildren(tn))
			{
				List<DPoint> lstcpt = new List<DPoint>();
				LayeredTreeInfo ltiCur = Info(tnCur);
				lstcpt.Add(dptOrigin);
				DetermineFinalPositions(tnCur, iLayer, pxFromTop + pxRowHeight + _pxBufferVertical, lti.PxFromLeft);
				lstcpt.Add(new DPoint(ltiCur.PxFromLeft + tnCur.TreeWidth / 2, ltiCur.PxFromTop));
				_lsttcn.Add(new TreeConnection(tn, tnCur, lstcpt));
			}
		}
		#endregion

		#endregion

		#region Internal classes
		private class LayeredTreeInfo
		{
		    public double SubTreeWidth { get; set; }
		    public double PxLeftPosRelativeToParent { get; set; }
			public double PxLeftPosRelativeToBoundingBox { get; set; }
			public double PxToLeftSibling { get; set; }
			public double PxFromTop { get; set; }
			public double PxFromLeft { get; set; }
		    public readonly List<double> LstPosLeftBoundaryRelativeToRoot = new List<double>();
			public readonly List<double> LstPosRightBoundaryRelativeToRoot = new List<double>();

			/// <summary>
			/// Initializes a new instance of the GraphLayoutInfo class.
			/// </summary>
			public LayeredTreeInfo(double subTreeWidth)
			{
			    SubTreeWidth = subTreeWidth;
			    PxLeftPosRelativeToParent = 0;
				PxFromTop = 0;
			}
		}
		#endregion
	}
}
