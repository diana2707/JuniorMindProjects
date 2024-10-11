using System;
using System.Collections.Generic;

namespace ObjectsCollection
{
    internal class RadixTreeNode
    {
        public RadixTreeNode()
        {
            Prefix = string.Empty;
        }

        public string Prefix { get; set; }
    }
}
