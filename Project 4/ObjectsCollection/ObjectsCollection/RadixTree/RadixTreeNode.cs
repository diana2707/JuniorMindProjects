using System;

namespace ObjectsCollection
{
    internal class RadixTreeNode<T>
    {
        internal RadixTreeNode()
        {
            Segment = [];
            PathLength = 0;
            Children = new List<RadixTreeNode<T>>();
        }

        internal List<T> Segment { get; set; }

        internal int PathLength { get; set; }

        internal List<RadixTreeNode<T>> Children { get; set; }

        internal bool IsEnd { get; set; }

        internal RadixTreeNode<T> AddLeaf(List<T> segment)
        {
            RadixTreeNode<T> node = new ();
            node.Segment = segment;
            node.PathLength = PathLength + segment.Count;
            node.IsEnd = true;

            Children.Add(node);
            return node;
        }

        internal void Split(int splitIndex)
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
