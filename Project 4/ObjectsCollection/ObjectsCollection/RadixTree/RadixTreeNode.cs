using System;
using System.Collections.Generic;

namespace ObjectsCollection
{
    internal class RadixTreeNode<T>
    {
        public RadixTreeNode()
        {
            Segment = [];
            PathLength = 0;
            Children = new List<RadixTreeNode<T>>();
        }

        public List<T> Segment { get; set; }

        public int PathLength { get; set; }

        public List<RadixTreeNode<T>> Children { get; set; }

        public bool IsEnd { get; set; }

        internal RadixTreeNode<T> AddLeaf(List<T> segment)
        {
            RadixTreeNode<T> node = new ();
            node.Segment = segment;
            node.PathLength = PathLength + segment.Count;

            Children.Add(node);
            return node;
        }

        internal void SplitNode(int splitIndex)
        {
            RadixTreeNode<T> newNode = new ()
            {
                Segment = Segment.GetRange(splitIndex, Segment.Count - splitIndex),
                PathLength = PathLength,
                Children = Children.ToList(),
                IsEnd = IsEnd
            };

            Children.Clear();
            Children.Add(newNode);
            Segment = Segment.GetRange(0, splitIndex);
            PathLength -= newNode.Segment.Count;
            IsEnd = false;
        }

        internal void Remove(RadixTreeNode<T> child)
        {
            Children.Remove(child);
        }
    }
}
