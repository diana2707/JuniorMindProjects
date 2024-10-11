using System;
using System.Collections.Generic;

namespace ObjectsCollection
{
    internal class RadixTreeNode
    {
        public RadixTreeNode()
        {
            Prefix = string.Empty;
            Children = new Dictionary<char, RadixTreeNode>();
        }

        public string Prefix { get; set; }

        public Dictionary<char, RadixTreeNode> Children { get; set; }

        public bool IsWord { get; set; }
    }
}
