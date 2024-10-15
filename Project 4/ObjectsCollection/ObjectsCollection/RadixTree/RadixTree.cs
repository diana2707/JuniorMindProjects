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

            RadixTreeNode currentNode = GetLastCommonNode(item, out int itemIndex);
            string currentString = item;
            string currentPrefix = currentNode.Prefix;
            int prefixIndex = 0;
            //currentString = currentString[index..];

            //verifica daca nodul curent trebuie sa se sparga sau nu
            if (currentNode.Prefix.Length > currentString.Length - itemIndex
                    || currentString.IndexOf(currentNode.Prefix, itemIndex, currentNode.Prefix.Length) != itemIndex)
            {
                (prefixIndex, itemIndex) = GetEndIndexes(currentPrefix, currentString, itemIndex);

                //nod nou cu ce ramane din nodul care se sparge
                RadixTreeNode newNode1 = new ();
                newNode1.Children = currentNode.Children.ToDictionary();
                newNode1.Prefix = currentPrefix[prefixIndex..];
                newNode1.IsWord = true;

                //se seteaza nodul care se sparge
                currentNode.Children.Clear();
                currentNode.Children.Add(newNode1.Prefix[0], newNode1);
                currentNode.Prefix = currentPrefix[..prefixIndex];
                currentNode.IsWord = false;
            }
            else
            {
                itemIndex += currentNode.Prefix.Length;
            }

            //se adauga ultimul nod cu partea din string ramasa neadaugata (indexul trebuie sa marcheze punctul in care nu mai nimic comun)
            if (currentString.Length != itemIndex)
            {
                RadixTreeNode newNode = new ();
                newNode.Prefix = currentString[itemIndex..];
                newNode.IsWord = true;
                currentNode.Children.Add(currentString[itemIndex], newNode);
            }
            //prefixul din nodul curent coincide cu ce ramane din string, deci doar se marcheaza ca si cuvant 
            else
            {
                currentNode.IsWord = true;
            }

            Count++;
        }

        public bool Contains(string item)
        {
            RadixTreeNode node = GetLastCommonNode(item, out int index);

            return node.IsWord && item.EndsWith(node.Prefix);
        }

        private RadixTreeNode GetLastCommonNode(string currentString, out int nodeIndex)//pot sa il numesc mai bine ca sa reflect ca iese si indexul?
        {
            RadixTreeNode currentNode = root;

            int index = 0;
            nodeIndex = index;

            while (index < currentString.Length && currentNode.Children.TryGetValue(currentString[index], out RadixTreeNode node))
            {
                currentNode = node;
                nodeIndex = index;

                if (currentNode.Prefix.Length > currentString.Length - index
                    || currentString.IndexOf(currentNode.Prefix, index, currentNode.Prefix.Length) != index)
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
    }
}
