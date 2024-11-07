using System;
using System.Collections.Generic;

namespace ObjectsCollection
{
    internal class RadixTreeNode
    {
        public RadixTreeNode()
        {
            Children = new Dictionary<string, RadixTreeNode>();
        }

        public Dictionary<string, RadixTreeNode> Children { get; set; }

        public bool IsWord { get; set; }
    }
}
