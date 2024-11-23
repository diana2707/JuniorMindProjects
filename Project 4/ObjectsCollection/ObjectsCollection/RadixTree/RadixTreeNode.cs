namespace ObjectsCollection
{
    internal class RadixTreeNode<T>
    {
        internal RadixTreeNode()
        {
            Segment = [];
            Children = new List<RadixTreeNode<T>>();
        }

        internal List<T> Segment { get; set; }

        internal List<RadixTreeNode<T>> Children { get; set; }

        internal bool IsEnd { get; set; }

        internal void AddLeaf(List<T> segment)
        {
            RadixTreeNode<T> node = new();
            node.Segment = segment;
            node.IsEnd = true;

            Children.Add(node);
        }

        internal void Split(int splitIndex)
        {
            RadixTreeNode<T> newNode = new()
            {
                Segment = Segment.GetRange(splitIndex, Segment.Count - splitIndex),
                Children = Children.ToList(),
                IsEnd = IsEnd
            };

            Children.Clear();
            Children.Add(newNode);
            Segment = Segment.GetRange(0, splitIndex);
            IsEnd = false;
        }

        internal void Remove(RadixTreeNode<T> child)
        {
            Children.Remove(child);
        }
    }
}
