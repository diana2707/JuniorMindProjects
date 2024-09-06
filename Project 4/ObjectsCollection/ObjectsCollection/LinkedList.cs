using System;

namespace ObjectsCollection
{
    internal class LinkedList<T>
    {
        public LinkedList()
        {
            Count = 0;
            First = null;
            IsReadOnly = false;
        }

        public int Count { get;  }

        public LinkedListNode<T>? First { get; }

        public bool IsReadOnly { get; }
    }
}
