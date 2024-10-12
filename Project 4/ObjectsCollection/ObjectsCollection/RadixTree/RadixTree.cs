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
            RadixTreeNode currentNode = root;
            string currentString = item;
            int index = 0;

            while (index < currentString.Length && currentNode.Children.TryGetValue(currentString[index], out RadixTreeNode node))
            {
                currentNode = node;
                string currentPrefix = currentNode.Prefix;
                currentString = currentString[index..];

                if (!currentString.StartsWith(currentPrefix))
                {
                    index = GetPrefixEndPosition(currentString, currentPrefix);
                    currentNode.Prefix = currentString[..index];
                    currentNode.IsWord = false;

                    RadixTreeNode newNode1 = new ();
                    newNode1.Prefix = currentPrefix[index..];
                    newNode1.IsWord = true;
                    currentNode.Children.Add(newNode1.Prefix[0], newNode1);

                    break;
                }

                index = currentPrefix.Length;
            }

            RadixTreeNode newNode = new ();
            newNode.Prefix = currentString[index..];
            newNode.IsWord = true;
            currentNode.Children.Add(currentString[index], newNode);

            Count++;
        }

        public bool Contains(string item)
        {
            RadixTreeNode currentNode = root;
            string currentString = item;

            int index = 0;

            while (index < currentString.Length && currentNode.Children.TryGetValue(currentString[index], out currentNode))
            {
                currentString = currentString[index..];

                if (currentNode.IsWord && currentString == currentNode.Prefix)
                {
                    return true;
                }

                if (!currentNode.IsWord && currentString.StartsWith(currentNode.Prefix))
                {
                    index = currentNode.Prefix.Length; //de vf daca mai e corect sau refac la +=
                }
            }

            return false;
        }

        private int GetPrefixEndPosition(string first, string second)
        {
            int minLength = Math.Min(first.Length, second.Length);
            int index = 0;
            for (int i = 0; i < minLength; i++)
            {
                if (first[i] == second[i])
                {
                    index++;
                }
            }

            return index;
        }
    }
}
