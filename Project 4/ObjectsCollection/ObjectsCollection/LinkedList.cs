using System;

namespace ObjectsCollection
{
    internal class LinkedList<T>
    {
        public LinkedList()
        {
            Count = 0;
            First = null;
            Last = null;
            IsReadOnly = false;
        }

        public int Count { get;  }

        public LinkedListNode<T>? First { get; }

        public LinkedListNode<T>? Last { get; }

        public bool IsReadOnly { get; }
    }
}
