using System;
using System.Collections.Generic;
using System.Collections.Immutable;

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
            //vf sa nu se introduca dubluri

            RadixTreeNode node = GetLastCommonNode(item, out int itemIndex);

            if (IsPartialPrefix(node, item, itemIndex))
            {
                (int prefixIndex, itemIndex) = GetEndIndexes(node.Prefix, item, itemIndex);

                RadixTreeNode splitNode = new ();
                SetNode(splitNode, node.Prefix[prefixIndex..], node.Children, true);

                Dictionary<char, RadixTreeNode> children = new () { { splitNode.Prefix[0], splitNode } };
                SetNode(node, node.Prefix[..prefixIndex], children, false);
            }
            else
            {
                itemIndex += node.Prefix.Length;
            }

            if (item.Length != itemIndex)
            {
                RadixTreeNode newNode = new ();
                SetNode(newNode, item[itemIndex..], newNode.Children, true);
                node.Children.Add(item[itemIndex], newNode);
            }
            else
            {
                node.IsWord = true;
            }

            Count++;
        }

        public bool Contains(string item)
        {
            RadixTreeNode node = GetLastCommonNode(item, out int index);

            return node.IsWord && item.EndsWith(node.Prefix);
        }

        private RadixTreeNode GetLastCommonNode(string item, out int itemIndex)//pot sa il numesc mai bine ca sa reflect ca iese si indexul?
        {
            RadixTreeNode currentNode = root;

            int index = 0;
            itemIndex = index;

            while (index < item.Length && currentNode.Children.TryGetValue(item[index], out RadixTreeNode node))
            {
                currentNode = node;
                itemIndex = index;

                if (IsPartialPrefix(currentNode, item, itemIndex))
                {
                    return currentNode;
                }

                index += currentNode.Prefix.Length;
            }

            return currentNode;
        }

        public bool Remove(string item)
        {
            RadixTreeNode node = GetLastCommonNode(item, out int index);

            if (node.Children.Count > 0)
            {
                node.IsWord = false;
                Count--;
                return true;
            }

            return false;
        }

        private (int prefixIndex, int itemIndex) GetEndIndexes(string prefix, string item, int itemIndex)
        {
            int minLength = Math.Min(prefix.Length, item.Length - itemIndex);
            int prefixIndex = 0;

            for (int i = 0; i < minLength; i++)
            {
                if (prefix[i] == item[itemIndex])
                {
                    prefixIndex++;
                    itemIndex++;
                }
            }

            return (prefixIndex, itemIndex);
        }

        private bool IsPartialPrefix(RadixTreeNode node, string item, int itemIndex)
        {
            return node.Prefix.Length > item.Length - itemIndex
                    || item.IndexOf(node.Prefix, itemIndex, node.Prefix.Length) != itemIndex;
        }

        private void SetNode(RadixTreeNode node, string prefix, Dictionary<char, RadixTreeNode> children, bool isWord)
        {
            node.Prefix = prefix;
            node.IsWord = isWord;
            node.Children = children.ToDictionary();
        }
    }
}
