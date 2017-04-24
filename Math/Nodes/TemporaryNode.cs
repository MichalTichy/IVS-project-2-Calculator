using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Math.Nodes.Functions.Binary;
using Math.Nodes.Functions.Unary;
using Math.Nodes.Values;
using Math.Tokenizer;

namespace Math.Nodes
{
    internal class TemporaryNode
    {
        protected TemporaryNode ParentNode { get; set; }

        protected TemporaryNode RightNode { get; set; }

        protected TemporaryNode LeftNode { get; set; }

        public MathOperatorDescription FutureType { get; set; }

        public decimal? Value { get; set; }

        public virtual TemporaryNode GetParentNode()
        {
            if (ParentNode == null)
                InsertToParent(new TemporaryNode());

            return ParentNode;
        }

        public virtual TemporaryNode GetRightNode()
        {
            if (RightNode == null)
                InsertToRight(new TemporaryNode());

            return RightNode;
        }

        public virtual TemporaryNode GetLeftNode()
        {
            if (LeftNode == null)
                InsertToLeft(new TemporaryNode());

            return LeftNode;
        }

        public virtual TemporaryNode InsertToRight(TemporaryNode node)
        {
            var tmp = RightNode;

            node.ParentNode = this;
            RightNode = node;

            node.LeftNode = tmp;
            if (tmp != null)
                tmp.ParentNode = node;

            return node;
        }

        public virtual TemporaryNode InsertToLeft(TemporaryNode node)
        {
            var tmp = LeftNode;

            node.ParentNode = this;
            LeftNode = node;

            node.LeftNode = tmp;
            if (tmp != null)
                tmp.ParentNode = node;

            return node;
        }

        public virtual TemporaryNode InsertToParent(TemporaryNode node)
        {
            var tmp = ParentNode;

            node.ParentNode = tmp;
            ParentNode = node;

            if (tmp != null)
            {
                if (tmp.LeftNode == this)
                {
                    tmp.LeftNode = node;
                }
                if (tmp.RightNode == this)
                {
                    tmp.RightNode = node;
                }
            }

            node.LeftNode = this;
            return node;
        }

        public virtual TemporaryNode GetRoot()
        {
            var root = this;
            while (root.ParentNode != null)
            {
                root = root.ParentNode;
            }
            return root;
        }

        public virtual INode Build(INode parentNode = null)
        {
            CheckBuildConditions();

            if (Value == null && FutureType == null)
                return BuildChildNode(parentNode);

            if (Value != null)
                return new NumberNode(Value.Value)
                    {Parent = parentNode};

            var node = (INode) Activator.CreateInstance(FutureType.NodeType);

            if (node is IUnaryOperationNode unaryNode)
                return FillNode(unaryNode, parentNode);

            if (node is IBinaryOperationNode binaryNode)
                return FillNode(binaryNode, parentNode);

            throw new NotSupportedException("Node type is not supported");
        }

        private INode BuildChildNode(INode parent)
        {
            if (RightNode == null && LeftNode == null)
                return null;

            if (RightNode != null && LeftNode != null)
                throw new ArgumentException("Cannot build empty node with right and left child set");

            return RightNode != null ? RightNode.Build(parent) : LeftNode.Build(parent);
        }

        private INode FillNode(IUnaryOperationNode node, INode parentNode)
        {
            node.Parent = parentNode;
            if (RightNode != null)
            {
                node.ChildNode = RightNode.Build(node);
            }
            else if (LeftNode != null)
            {
                node.ChildNode = LeftNode.Build(node);
            }
            return node;
        }

        private INode FillNode(IBinaryOperationNode node, INode parentNode)
        {
            node.Parent = parentNode;
            node.RightNode = RightNode.Build(node);
            node.LeftNode = LeftNode.Build(node);
            return node;
        }

        public void UnreferenceFromParent()
        {
            if (ParentNode.LeftNode == this)
            {
                ParentNode.LeftNode = null;
            }

            if (ParentNode.RightNode == this)
            {
                ParentNode.RightNode = null;
            }
        }

        private void CheckBuildConditions()
        {
            if (Value != null && FutureType != null)
                throw new ArgumentException($"{nameof(Value)} and {nameof(FutureType)} cannot be set simoutanously.");

            var canBeUnaryNode = (LeftNode == null && RightNode != null) || (LeftNode != null && RightNode == null);
            if (FutureType != null &&
                (FutureType.NodeType.IsUnary() && !canBeUnaryNode))
                throw new ArgumentException($"Unary node needs exactly one child node.");

            var canBeBinaryNode = LeftNode != null && RightNode != null;
            if (FutureType != null &&
                (FutureType.NodeType.IsBinary() && !canBeBinaryNode))
                throw new ArgumentException($"Binary node needs exactly two child nodes.");
        }
    }
}