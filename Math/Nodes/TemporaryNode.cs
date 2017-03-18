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

namespace Math.Nodes
{
    internal class TemporaryNode
    {
        protected TemporaryNode ParentNode { get; set; }

        protected TemporaryNode RightNode { get; set; }

        protected TemporaryNode LeftNode { get; set; }

        public MathOperatorDescription FutureType { get; set; }

        public decimal? Value { get; set; }

        public TemporaryNode GetParentNode()
        {
            if (ParentNode == null)
                InsertToParent(new TemporaryNode());

            return ParentNode;
        }

        public TemporaryNode GetRightNode()
        {
            if (RightNode == null)
                InsertToRight(new TemporaryNode());

            return RightNode;
        }

        public TemporaryNode GetLeftNode()
        {
            if (LeftNode == null)
                InsertToLeft(new TemporaryNode());

            return LeftNode;
        }

        public TemporaryNode InsertToRight(TemporaryNode node)
        {
            var tmp = RightNode;

            node.ParentNode = this;
            RightNode = node;

            node.LeftNode = tmp;
            if (tmp != null)
                tmp.ParentNode = node;

            return node;
        }

        public TemporaryNode InsertToLeft(TemporaryNode node)
        {
            var tmp = LeftNode;

            node.ParentNode = this;
            LeftNode = node;

            node.LeftNode = tmp;
            if (tmp != null)
                tmp.ParentNode = node;

            return node;
        }

        public TemporaryNode InsertToParent(TemporaryNode node)
        {
            var tmp = ParentNode;

            node.ParentNode = tmp;
            ParentNode = node;

            if (tmp!=null)
            {
                if (tmp.LeftNode==this)
                {
                    tmp.LeftNode = node;
                }
                if (tmp.RightNode==this)
                {
                    tmp.RightNode = node;
                }
            }

            node.LeftNode = this;
            return node;
        }

        public TemporaryNode GetRoot()
        {
            var root = this;
            while (root.ParentNode!=null)
            {
                root = root.ParentNode;
            }
            return root;
        }

        public INode Build()
        {
            CheckBuildConditions();

            if (Value != null)
                return new NumberNode(Value.Value);

            var node = (INode)Activator.CreateInstance(FutureType.NodeType);
            var unaryNode = node as IUnaryOperationNode;
            if (unaryNode != null)
                return FillNode(unaryNode);

            var binaryNode = node as IBinaryOperationNode;
            if (binaryNode != null)
                return FillNode(binaryNode);

            throw new NotSupportedException("Node type is not supported");
        }

        private INode FillNode(IUnaryOperationNode node)
        {
            if (RightNode != null)
            {
                node.ChildNode = RightNode.Build();
            }
            else if (LeftNode != null)
            {
                node.ChildNode = LeftNode.Build();
            }
            return node;
        }

        private INode FillNode(IBinaryOperationNode node)
        {
            node.RightNode = RightNode.Build();
            node.LeftNode = LeftNode.Build();
            return node;
        }

        private void CheckBuildConditions()
        {
            if (Value != null && FutureType != null)
                throw new ArgumentException($"{nameof(Value)} and {nameof(FutureType)} cannot be set simoutanously.");

            if (Value == null && FutureType == null)
                throw new ArgumentException($"{nameof(Value)} or {nameof(FutureType)} has to be set.");

            var canBeUnaryNode = (LeftNode == null && RightNode != null) || (LeftNode != null && RightNode == null);
            if (FutureType != null && (FutureType.NodeType.IsInstanceOfType(typeof(IUnaryOperationNode)) && !canBeUnaryNode))
                throw new ArgumentException($"Unary node needs exactly one child node.");

            var canBeBinaryNode = LeftNode != null && RightNode != null;
            if (FutureType != null && (FutureType.NodeType.IsInstanceOfType(typeof(IBinaryOperationNode)) && !canBeBinaryNode))
                throw new ArgumentException($"Binary node needs exactly two child nodes.");
        }
    }
}
