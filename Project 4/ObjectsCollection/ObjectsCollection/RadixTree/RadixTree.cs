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
            if (Count == 0)
            {
                RadixTreeNode node = new ();
                node.Prefix = item;
                node.IsWord = true;
                root.Children.Add(item[0], node);
            }
            else
            {
                if (root.Children.TryGetValue(item[0], out RadixTreeNode? node))
                {
                    int index = 0;
                    string current = node.Prefix;

                    while (item[index] == current[index])
                    {
                        index++;
                    }

                    RadixTreeNode newNode1 = new ();
                    RadixTreeNode newNode2 = new ();

                    node.Prefix = current[..index];
                    node.IsWord = false;

                    newNode1.Prefix = current[index..];
                    newNode1.IsWord = true;

                    newNode2.Prefix = item[index..];
                    newNode2.IsWord = true;

                    node.Children.Add(newNode1.Prefix[0], newNode1);
                    node.Children.Add(newNode2.Prefix[0], newNode2);
                }
            }

            Count++;
        }
    }
}
