using System;
using System.Collections.Generic;

namespace ObjectsCollection
{
    public class RadixTree
    {
        readonly RadixTreeNode root;

        public RadixTree()
        {
            root = new RadixTreeNode();
            Count = 0;
        }

        public int Count { get; private set; }

        public void Add(string item)
        {
            RadixTreeNode node = new RadixTreeNode();
            node.Prefix = item;
            root.Children.Add(item, node);

            Count++;
        }
    }
}
