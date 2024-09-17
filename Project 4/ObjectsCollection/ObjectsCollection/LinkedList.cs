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
        }

        public LinkedList(IEnumerable<T> collection) : this()
        {
            ArgumentNullException.ThrowIfNull(collection);

            foreach (T item in collection)
            {
                AddLast(item);
            }
        }

        public int Count { get; private set; }

        public LinkedListNode<T>? First => IsEmpty() ? null : sentinel.Next;

        public LinkedListNode<T>? Last => IsEmpty() ? null : sentinel.Previous;

        public bool IsReadOnly => false;

        void ICollection<T>.Add(T item)
        {
            AddLast(item);
        }

        public void AddFirst(T item)
        {
            LinkedListNode<T> newNode = new (item);

            AddFirst(newNode);
        }

        public void AddFirst(LinkedListNode<T> newNode)
        {
            ValidateNewNode(newNode);

            SetNode(newNode, sentinel, true);

            Count++;
        }

        public void AddLast(T item)
        {
            LinkedListNode<T> newNode = new (item);

            AddLast(newNode);
        }

        public void AddLast(LinkedListNode<T> newNode)
        {
            ValidateNewNode(newNode);

            SetNode(newNode, sentinel, false);

            Count++;
        }

        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T item)
        {
            LinkedListNode<T> newNode = new (item);
            AddAfter(node, newNode);

            return newNode;
        }

        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            ValidateListNode(node);
            ValidateNewNode(newNode);

            SetNode(newNode, node, true);

            Count++;
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T item)
        {
            LinkedListNode<T> newNode = new (item);
            AddBefore(node, newNode);

            return newNode;
        }

        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            ValidateListNode(node);
            ValidateNewNode(newNode);

            SetNode(newNode, node, false);

            Count++;
        }

        public void Clear()
        {
            SetNode(sentinel, sentinel, true);

            Count = 0;
        }

        public bool Contains(T item)
        {
            return Search(item, true) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ValidateArray(array, arrayIndex);

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
            return Search(item, true);
        }

        public LinkedListNode<T> FindLast(T item)
        {
            return Search(item, false);
        }

        public bool Remove(T item)
        {
            LinkedListNode<T> node = Search(item, true);

            if (node == null)
            {
                return false;
            }

            Remove(node);
            return true;
        }

        public void Remove(LinkedListNode<T> node)
        {
            ValidateListNode(node);

            RemoveNode(node);
            Count--;
        }

        public void RemoveFirst()
        {
            ValidateList();

            RemoveNode(First);
            Count--;
        }

        public void RemoveLast()
        {
            ValidateList();

            RemoveNode(Last);
            Count--;
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

            if (IsNodeInList(node))
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

        private LinkedListNode<T> Search(T value, bool startFromFirst)
        {
            if (IsEmpty())
            {
                return null;
            }

            LinkedListNode<T> current = startFromFirst ? First : Last;

            while (current != sentinel)
            {
                if (current.Value.Equals(value))
                {
                    return current;
                }

                current = startFromFirst ? current.Next : current.Previous;
            }

            return null;
        }

        private bool IsNodeInList(LinkedListNode<T> node)
        {
            if (IsEmpty())
            {
                return false;
            }

            LinkedListNode<T> current = First;

            while (current != sentinel)
            {
                if (current.Equals(node))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        private void SetNode(LinkedListNode<T> newNode, LinkedListNode<T> referenceNode, bool newNodeFollowsRefNode)
        {
            newNode.List = this;
            newNode.Next = newNodeFollowsRefNode ? referenceNode.Next : referenceNode;
            newNode.Previous = newNodeFollowsRefNode ? referenceNode : referenceNode.Previous;

            if (newNodeFollowsRefNode)
            {
                referenceNode.Next.Previous = newNode;
                referenceNode.Next = newNode;
            }
            else
            {
                referenceNode.Previous.Next = newNode;
                referenceNode.Previous = newNode;
            }
        }

        private static void RemoveNode(LinkedListNode<T> node)
        {
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
        }
    }
}
