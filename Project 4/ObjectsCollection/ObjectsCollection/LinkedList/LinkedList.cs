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
            sentinel.List = this;
        }

        public int Count { get; private set; }

        public LinkedListNode<T>? First => IsEmpty() ? null : sentinel.Next;

        public LinkedListNode<T>? Last => IsEmpty() ? null : sentinel.Previous;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            AddLast(item);
        }

        public LinkedListNode<T> AddFirst(T item)
        {
            LinkedListNode<T> newNode = new(item);
            AddFirst(newNode);

            return newNode;
        }

        public void AddFirst(LinkedListNode<T> newNode)
        {
            AddAfter(sentinel, newNode);
        }

        public LinkedListNode<T> AddLast(T item)
        {
            LinkedListNode<T> newNode = new(item);
            AddLast(newNode);

            return newNode;
        }

        public void AddLast(LinkedListNode<T> newNode)
        {
            AddBefore(sentinel, newNode);
        }

        public LinkedListNode<T> AddAfter(LinkedListNode<T> listNode, T item)
        {
            LinkedListNode<T> newNode = new(item);
            AddAfter(listNode, newNode);

            return newNode;
        }

        public void AddAfter(LinkedListNode<T> listNode, LinkedListNode<T> newNode)
        {
            ValidateNewNode(newNode);
            ValidateListNode(listNode);

            newNode.List = this;
            newNode.Next = listNode.Next;
            newNode.Previous = listNode;

            listNode.Next.Previous = newNode;
            listNode.Next = newNode;

            Count++;
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> listNode, T item)
        {
            LinkedListNode<T> newNode = new(item);
            AddBefore(listNode, newNode);

            return newNode;
        }

        public void AddBefore(LinkedListNode<T> listNode, LinkedListNode<T> newNode)
        {
            ValidateListNode(listNode);
            AddAfter(listNode.Previous, newNode);
        }

        public void Clear()
        {
            for (LinkedListNode<T> current = sentinel.Next;
                current != sentinel;
                current = current.Next)
            {
                current.List = null;
            }

            sentinel.Next = sentinel;
            sentinel.Previous = sentinel;

            Count = 0;
        }

        public bool Contains(T item)
        {
            return Find(item) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ValidateArray(array, arrayIndex);

            for (LinkedListNode<T> current = sentinel.Next;
                current != sentinel;
                current = current.Next)
            {
                array[arrayIndex] = current.Value;
                arrayIndex++;
            }
        }

        public LinkedListNode<T> Find(T item)
        {
            for (LinkedListNode<T> current = sentinel.Next;
                current != sentinel;
                current = current.Next)
            {
                if (current.Value.Equals(item))
                {
                    return current;
                }
            }

            return null;
        }

        public LinkedListNode<T> FindLast(T item)
        {
            for (LinkedListNode<T> current = sentinel.Previous;
               current != sentinel;
               current = current.Previous)
            {
                if (current.Value.Equals(item))
                {
                    return current;
                }
            }

            return null;
        }

        public bool Remove(T item)
        {
            LinkedListNode<T> node = Find(item);

            if (node == null)
            {
                return false;
            }

            Remove(node);
            return true;
        }

        public void Remove(LinkedListNode<T> node)
        {
            ValidateList();
            ValidateListNode(node);

            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
            node.List = null;

            Count--;
        }

        public void RemoveFirst()
        {
            Remove(sentinel.Next);
        }

        public void RemoveLast()
        {
            Remove(sentinel.Previous);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (LinkedListNode<T> current = sentinel.Next; current != sentinel; current = current.Next)
            {
                yield return current.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static void ValidateNewNode(LinkedListNode<T> node)
        {
            ArgumentNullException.ThrowIfNull(node);

            if (node.List == null)
            {
                return;
            }

            throw new InvalidOperationException("New node belongs to another list.");
        }

        private void ValidateListNode(LinkedListNode<T> node)
        {
            ArgumentNullException.ThrowIfNull(node);

            if (ReferenceEquals(node.List, this))
            {
                return;
            }

            throw new InvalidOperationException("Node is not in the current list.");
        }

        private void ValidateList()
        {
            if (!IsEmpty())
            {
                return;
            }

            throw new InvalidOperationException("List is empty.");
        }

        private void ValidateArray(T[] array, int index)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);

            if (Count <= array.Length - index)
            {
                return;
            }

            throw new ArgumentException("There is not enough space from index to end of array.");
        }

        private bool IsEmpty()
        {
            return Count == 0;
        }
    }
}
