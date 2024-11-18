using System;
using System.Collections.Generic;

namespace ObjectsCollection
{
    internal class RadixTreeNode<T>
    {
        public RadixTreeNode()
        {
            Prefix = [];
            Children = new List<RadixTreeNode<T>>();
        }

        public List<T> Prefix { get; set; }

        public List<RadixTreeNode<T>> Children { get; set; }

        public bool IsEnd { get; set; }
    }
}
