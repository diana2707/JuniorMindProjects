using System.Collections;

namespace ObjectsCollection
{
    public class LinkedList<T> : ICollection<T>
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

        public void Add(T item)
        {
            LinkedListNode<T> node = new (item);
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

        public bool Contains(T item)
        {
            return !IsEmpty() && LinearSearch(First, item) != null;
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

        public LinkedListNode<T> Find(T item)
        {
            if (IsEmpty())
            {
                return null;
            }

            LinkedListNode<T> current = First;

            while (current != sentinel)
            {
                if (current.Value.Equals(item))
                {
                    return current;
                }

                current = current.Next;
            }

            return null;
        }

        public LinkedListNode<T> FindLast(T item)
        {
            if (IsEmpty())
            {
                return null;
            }

            LinkedListNode<T> current = Last;

            while (current != sentinel)
            {
                if (current.Value.Equals(item))
                {
                    return current;
                }

                current = current.Previous;
            }

            return null;
        }

        public bool Remove(T item)
        {
            bool isRemoved = false;
            LinkedListNode<T> current = First;

            while (current != sentinel)
            {
                if (current.Value.Equals(item))
                {
                    current.Previous.Next = current.Next;
                    current.Next.Previous = current.Previous;
                    isRemoved = true;
                }

                current = current.Next;
            }

            return isRemoved;
        }

        public IEnumerator<T> GetEnumerator()
        {
            LinkedListNode<T> current = First;

            while (current != sentinel)
            {
                yield return current.Value;

                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool IsEmpty()
        {
            return sentinel.Next == sentinel;
        }

        private LinkedListNode<T> LinearSearch(LinkedListNode<T> start, T value)
        {
            LinkedListNode<T> current = start;
            while (current != sentinel)
            {
                if (current.Value.Equals(value))
                {
                    return current;
                }

                current = start == First ? current.Next : current.Previous;
            }

            return null;
        }
    }
}
