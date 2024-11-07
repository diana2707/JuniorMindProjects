using System;
using System.Collections.Generic;

namespace ObjectsCollection
{
    internal class RadixTreeNode<T>
    {
        public RadixTreeNode()
        {
            Children = new Dictionary<string, RadixTreeNode<T>>();
        }

        public Dictionary<string, RadixTreeNode<T>> Children { get; set; }

        public T Value { get; set; }

        public bool IsWord { get; set; }
    }
}
