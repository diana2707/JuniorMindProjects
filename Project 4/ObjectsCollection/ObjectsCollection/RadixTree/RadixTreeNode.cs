using System;
using System.Collections.Generic;

namespace ObjectsCollection
{
    internal class RadixTreeNode
    {
        public RadixTreeNode()
        {
            Prefix = string.Empty;
            Children = new Dictionary<string, RadixTreeNode>();
        }

        public string Prefix { get; set; }

        public Dictionary<string, RadixTreeNode> Children { get; set; }

        public bool IsWord { get; set; }
    }
}
