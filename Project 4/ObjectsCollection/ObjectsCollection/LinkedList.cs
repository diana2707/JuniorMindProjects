using System;

namespace ObjectsCollection
{
    internal class LinkedList<T>
    {
        public LinkedList()
        {
            Count = 0;
            IsReadOnly = false;
        }

        public int Count { get;  }

        public bool IsReadOnly { get; }
    }
}
