using System;
using System.Collections.Generic;

namespace ObjectsCollection
{
    internal class RadixTreeNode
    {
        public RadixTreeNode()
        {
            Children = new Dictionary<char, RadixTreeNode>();
        }

        public Dictionary<char, RadixTreeNode> Children { get; set; }

        public bool IsWord { get; set; }
    }
}
