using System;

namespace ObjectsCollection
{
    internal class LinkedList<T>
    {
        readonly LinkedListNode<T> sentinel;

        public LinkedList()
        {
            sentinel = new LinkedListNode<T>(default);
            sentinel.next = sentinel;
            sentinel.previous = sentinel;

            //sentinel.next = First;
            //sentinel.previous = Last;

            Count = 0;
            IsReadOnly = false;
        }

        public int Count { get;  }

        public LinkedListNode<T>? First => IsEmpty() ? null : sentinel.next;

        public LinkedListNode<T>? Last => IsEmpty() ? null : sentinel.previous;

        public bool IsReadOnly { get; }

        public void Add(LinkedListNode<T> node)
        {
            if (IsEmpty())
            {
                sentinel.next = node;
                node.previous = sentinel;
            }
            else
            {
                Last.next = node;
                node.previous = Last;
            }

            sentinel.previous = node;
            node.next = sentinel;
        }

        private bool IsEmpty()
        {
            return sentinel.next == sentinel;
        }
    }
}
