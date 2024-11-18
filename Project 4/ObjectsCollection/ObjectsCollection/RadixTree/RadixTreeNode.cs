using System;
using System.Collections.Generic;

namespace ObjectsCollection
{
    internal class RadixTreeNode<T>
    {
        public RadixTreeNode()
        {
            Prefix = [];
            Children = new Dictionary<IEnumerable<T>, RadixTreeNode<T>>();
        }

        public List<T> Prefix { get; set; }
        public Dictionary<IEnumerable<T>, RadixTreeNode<T>> Children { get; set; }

        public bool IsEnd { get; set; }
    }
}
