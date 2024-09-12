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

            Count = 0;
            IsReadOnly = false;
        }

        public int Count { get; private set; }

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

            Count++;
        }

        public void Clear()
        {
            sentinel.next = sentinel;
            sentinel.previous = sentinel;

            Count = 0;
        }

        private bool IsEmpty()
        {
            return sentinel.next == sentinel;
        }
    }
}
