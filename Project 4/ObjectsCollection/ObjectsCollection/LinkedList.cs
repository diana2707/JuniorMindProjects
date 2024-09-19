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

        public LinkedListNode<T> AddFirst(T item)
        {
            LinkedListNode<T> newNode = new (item);
            AddNode(newNode, sentinel, true);
            return newNode;
        }

        public void AddFirst(LinkedListNode<T> newNode)
        {
            AddNode(newNode, sentinel, true);
        }

        public LinkedListNode<T> AddLast(T item)
        {
            LinkedListNode<T> newNode = new (item);
            AddNode(newNode, sentinel, false);
            return newNode;
        }

        public void AddLast(LinkedListNode<T> newNode)
        {
            AddNode(newNode, sentinel, false);
        }

        public LinkedListNode<T> AddAfter(LinkedListNode<T> listNode, T item)
        {
            LinkedListNode<T> newNode = new (item);
            AddAfter(listNode, newNode);

            return newNode;
        }

        public void AddAfter(LinkedListNode<T> listNode, LinkedListNode<T> newNode)
        {
            ValidateNewNode(newNode);
            if (listNode != sentinel)
            {
                ValidateListNode(listNode);
            }

            SetNode(newNode, listNode, true);
            Count++;
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> listNode, T item)
        {
            ValidateListNode(listNode);

            LinkedListNode<T> newNode = new (item);
            AddAfter(listNode.Previous, newNode);

            return newNode;
        }

        public void AddBefore(LinkedListNode<T> listNode, LinkedListNode<T> newNode)
        {
            ValidateListNode(listNode);
            AddAfter(listNode.Previous, newNode);
        }

        public void Clear()
        {
            SetNode(sentinel, sentinel, true);
            Count = 0;
        }

        public bool Contains(T item)
        {
            return SearchForward(item) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ValidateArray(array, arrayIndex);

            LinkedListNode<T> current = sentinel.Next;

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
            return SearchForward(item);
        }

        public LinkedListNode<T> FindLast(T item)
        {
            return SearchBackward(item);
        }

        public bool Remove(T item)
        {
            LinkedListNode<T> node = SearchForward(item);

            if (node == null)
            {
                return false;
            }

            RemoveNode(node);
            return true;
        }

        public void Remove(LinkedListNode<T> node)
        {
            RemoveNode(node);
        }

        public void RemoveFirst()
        {
            RemoveNode(sentinel.Next);
        }

        public void RemoveLast()
        {
            RemoveNode(sentinel.Previous);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (LinkedListNode<T> current = sentinel.Next;  current != sentinel; current = current.Next)
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

        private LinkedListNode<T> SearchForward(T value)
        {
            for (LinkedListNode<T> current = sentinel.Next;
                current != sentinel;
                current = current.Next)
            {
                if (current.Value.Equals(value))
                {
                    return current;
                }
            }

            return null;
        }

        private LinkedListNode<T> SearchBackward(T value)
        {
            for (LinkedListNode<T> current = sentinel.Previous;
                current != sentinel;
                current = current.Previous)
            {
                if (current.Value.Equals(value))
                {
                    return current;
                }
            }

            return null;
        }

        private bool IsNodeInList(LinkedListNode<T> node)
        {
            for (LinkedListNode<T> current = sentinel.Next; current != sentinel; current = current.Next)
            {
                if (current.Equals(node))
                {
                    return true;
                }
            }

            return false;
        }

        private void AddNode(LinkedListNode<T> newNode, LinkedListNode<T> referenceNode, bool newNodeFollowsRefNode)
        {
            ValidateNewNode(newNode);
            if (referenceNode != sentinel)
            {
                ValidateListNode(referenceNode);
            }

            SetNode(newNode, referenceNode, newNodeFollowsRefNode);
            Count++;
        }

        private void RemoveNode(LinkedListNode<T> node)
        {
            ValidateList();
            ValidateListNode(node);

            ResetLinkedNodes(node);
            Count--;
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

        private static void ResetLinkedNodes(LinkedListNode<T> node)
        {
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
        }
    }
}
