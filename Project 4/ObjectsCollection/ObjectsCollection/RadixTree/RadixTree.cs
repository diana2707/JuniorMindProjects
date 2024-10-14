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
            string currentPrefix = "";
            int index = 0;

            //verfica daca exista copii ai nodului curent
            while (index < currentString.Length && currentNode.Children.TryGetValue(currentString[index], out RadixTreeNode node))
            {
                currentNode = node;
                currentPrefix = currentNode.Prefix;
                currentString = currentString[index..];

                //verifica daca nodul curent trebuie sa se sparga sau nu
                if (!currentString.StartsWith(currentPrefix))
                {
                    index = GetPrefixEndPosition(currentString, currentPrefix);

                    //nod nou cu ce ramane din nodul care se sparge
                    RadixTreeNode newNode1 = new ();
                    newNode1.Prefix = currentPrefix[index..];
                    newNode1.IsWord = true;

                    //se seteaza nodul care se sparge
                    currentNode.Children.Add(newNode1.Prefix[0], newNode1);
                    currentNode.Prefix = currentString[..index];
                    currentNode.IsWord = currentString.Length == index;

                    break;
                    //catnip - cat => nu vreau sa mai intre in codul urmator pt ca deja am cat-nip corect
                    //cat - catnip => nu intra in codul de mai sus si sperge folosind codul de mai jos dar ajunge tot la cat -nip 
                }

                //inainteaza indexul pentru stringul curent cu lungimea prefixului deja verificat
                index = currentPrefix.Length;
            }

            //se adauga ultimul nod cu partea din string ramasa neadaugata (indexul trebuie sa marcheze punctul in care nu mai nimic comun)
            if (currentString.Length != index)
            {
                RadixTreeNode newNode = new ();
                newNode.Prefix = currentString[index..];
                newNode.IsWord = true;
                currentNode.Children.Add(currentString[index], newNode);
            }

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

                if (currentString.StartsWith(currentNode.Prefix))
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
