using System;
using System.Collections.Generic;

namespace ObjectsCollection
{
    internal class RadixTreeNode<T>
    {
        public RadixTreeNode()
        {
            Prefix = [];
            PathLength = 0;
            Children = new List<RadixTreeNode<T>>();
        }

        public List<T> Prefix { get; set; }

        public int PathLength { get; set; }

        public List<RadixTreeNode<T>> Children { get; set; }

        public bool IsEnd { get; set; }
    }
}
