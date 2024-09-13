using Newtonsoft.Json.Linq;
using System;

namespace ObjectsCollection
{
    internal class LinkedList<T>
    {
        readonly LinkedListNode<T> sentinel;

        public LinkedList()
        {
            sentinel = new LinkedListNode<T>(default);
            sentinel.Next = sentinel;
            sentinel.Previous = sentinel;

            Count = 0;
            IsReadOnly = false;
        }

        public int Count { get; private set; }

        public LinkedListNode<T>? First => IsEmpty() ? null : sentinel.Next;

        public LinkedListNode<T>? Last => IsEmpty() ? null : sentinel.Previous;

        public bool IsReadOnly { get; }

        public void Add(LinkedListNode<T> node)
        {
            if (IsEmpty())
            {
                sentinel.Next = node;
                node.Previous = sentinel;
            }
            else
            {
                Last.Next = node;
                node.Previous = Last;
            }

            sentinel.Previous = node;
            node.Next = sentinel;

            Count++;
        }

        public void Clear()
        {
            sentinel.Next = sentinel;
            sentinel.Previous = sentinel;

            Count = 0;
        }

        public bool Contains(T value)
        {
            if (IsEmpty())
            {
                return false;
            }

            LinkedListNode<T> current = First;

            while (current != sentinel)
            {
                if (current.Value.Equals(value))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            LinkedListNode<T> current = First;

            for (int i = arrayIndex; i < Count + arrayIndex; i++)
            {
                if (current != sentinel)
                {
                    array[i] = current.Value;
                    current = current.Next;
                }
            }
        }

        public void Remove(T value)
        {
            LinkedListNode<T> current = First;

            while (current != sentinel)
            {
                if (current.Value.Equals(value))
                {
                    current.Previous = current.Next;
                }

                current = current.Next;
            }
        }

        private bool IsEmpty()
        {
            return sentinel.Next == sentinel;
        }
    }
}
