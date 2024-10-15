using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Xunit;

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

            //verifica daca nodul curent trebuie sa se sparga sau nu
            if (IsPartialPrefix(node, item, itemIndex))
            {
                (int prefixIndex, itemIndex) = GetEndIndexes(node.Prefix, item, itemIndex);

                //nod nou cu ce ramane din nodul care se sparge
                RadixTreeNode childNode = new ();
                childNode.Children = node.Children.ToDictionary();
                childNode.Prefix = node.Prefix[prefixIndex..];
                childNode.IsWord = true;

                //se seteaza nodul care se sparge
                node.Children.Clear();
                node.Children.Add(childNode.Prefix[0], childNode);
                node.Prefix = node.Prefix[..prefixIndex];
                node.IsWord = false;
            }
            else
            {
                itemIndex += node.Prefix.Length;
            }

            //se adauga ultimul nod cu partea din string ramasa neadaugata (indexul trebuie sa marcheze punctul in care nu mai nimic comun)
            if (item.Length != itemIndex)
            {
                RadixTreeNode childNode = new ();
                childNode.Prefix = item[itemIndex..];
                childNode.IsWord = true;
                node.Children.Add(item[itemIndex], childNode);
            }
            //prefixul din nodul curent coincide cu ce ramane din string, deci doar se marcheaza ca si cuvant 
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
    }
}
