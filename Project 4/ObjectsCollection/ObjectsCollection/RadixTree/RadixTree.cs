using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;

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
            RadixTreeNode currentNode = GetLastCommonNode(item, out int index);
            string currentString = item;
            string currentPrefix = "";

            //verfica daca exista copii ai nodului curent

            currentPrefix = currentNode.Prefix;
            currentString = currentString[index..];

            //verifica daca nodul curent trebuie sa se sparga sau nu
            if (!currentString.StartsWith(currentPrefix))
            {
                index = GetPrefixEndPosition(currentString, currentPrefix);

                //nod nou cu ce ramane din nodul care se sparge
                RadixTreeNode newNode1 = new();
                newNode1.Children = currentNode.Children.ToDictionary();
                newNode1.Prefix = currentPrefix[index..];
                newNode1.IsWord = true;

                //se seteaza nodul care se sparge
                currentNode.Children.Clear();
                currentNode.Children.Add(newNode1.Prefix[0], newNode1);
                currentNode.Prefix = currentString[..index];
                currentNode.IsWord = false;
            }
            else
            {
                index = currentNode.Prefix.Length;
            }

            //se adauga ultimul nod cu partea din string ramasa neadaugata (indexul trebuie sa marcheze punctul in care nu mai nimic comun)
            if (currentString.Length != index)
            {
                RadixTreeNode newNode = new ();
                newNode.Prefix = currentString[index..];
                newNode.IsWord = true;
                currentNode.Children.Add(currentString[index], newNode);
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

        private RadixTreeNode GetLastCommonNode(string currentString, out int index)//pot sa il numesc mai bine ca sa reflect ca iese si indexul?
        {
            RadixTreeNode currentNode = root;

            index = 0;

            while (index < currentString.Length && currentNode.Children.TryGetValue(currentString[index], out RadixTreeNode node))
            {
                currentNode = node;

                if (currentNode.Prefix.Length > currentString.Length - index
                    || currentString.IndexOf(currentNode.Prefix, index, currentNode.Prefix.Length) != index)
                {
                    return currentNode;
                }

                index += currentNode.Prefix.Length;
            }

            index = index - currentNode.Prefix.Length;

            return currentNode;
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
