using System;
using System.Collections.Generic;

namespace ObjectsCollection
{
    internal class RadixTreeNode<T>
    {
        public RadixTreeNode()
        {
            Children = new Dictionary<IEnumerable<T>, RadixTreeNode<T>>();
        }

        public Dictionary<IEnumerable<T>, RadixTreeNode<T>> Children { get; set; }

        public bool IsWord { get; set; }
    }
}
