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
            //should have a read only collection?
        }

        public LinkedList(IEnumerable<T> collection) : this()
        {
            ArgumentNullException.ThrowIfNull(collection);

            foreach (T item in collection)
            {
                AddLast(item);
            }

            //see to accept null values for items
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
            //when node is null throw exception 
            //verify it accept null values but not null next, previous, check every situation

            LinkedListNode<T> newNode = new (item);

            AddFirst(newNode);
        }

        public void AddFirst(LinkedListNode<T> newNode)
        {
            ArgumentNullException.ThrowIfNull(newNode);
            if (newNode.List != null)
            {
                throw new InvalidOperationException("New node belongs to another list.");
            }

            if (IsEmpty())
            {
                AddToEmptyList(newNode);
            }
            else
            {
                newNode.Next = First;
                newNode.Previous = sentinel;
                First.Previous = newNode;
                sentinel.Next = newNode;
                newNode.List = this;
            }

            Count++;
        }

        public void AddLast(T item)
        {
            //when node is null throw exception 

            LinkedListNode<T> newNode = new (item);

            AddLast(newNode);
        }

        public void AddLast(LinkedListNode<T> newNode)
        {
            //when node is null throw exception 

            if (IsEmpty())
            {
                AddToEmptyList(newNode);
            }
            else
            {
                newNode.Previous = Last;
                newNode.Next = sentinel;
                Last.Next = newNode;
                sentinel.Previous = newNode;
                newNode.List = this;
            }

            Count++;
        }

        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T item)
        {
            //verifica edge case, AddAfter last node
            LinkedListNode<T> newNode = new (item);

            AddAfter(node, newNode);
            return newNode;
        }

        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            ValidateNodes(node, newNode);

            newNode.Previous = node;
            newNode.Next = node.Next;
            node.Next.Previous = newNode;
            node.Next = newNode;
            newNode.List = this;

            Count++;
        }

        public void AddBefore(LinkedListNode<T> node, T item)
        {
            //verifica edge case, addbefore first node

            LinkedListNode<T> newNode = new (item);

            AddBefore(node, newNode);
        }

        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            ValidateNodes(node, newNode);

            newNode.Next = node;
            newNode.Previous = node.Previous;
            node.Previous.Next = newNode;
            node.Previous = newNode;
            newNode.List = this;

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
            return Search(item, true) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            //why is implicit interface implementation?
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
            //throw ex if node is null

            if (node == null)
            {
                return;
            }

            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
            Count--;
        }

        public void RemoveFirst()
        {
            sentinel.Next = First.Next;
            First.Next.Previous = sentinel;
            Count--;
        }

        public void RemoveLast()
        {
            sentinel.Previous = Last.Previous;
            Last.Previous.Next = sentinel;
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

        private bool IsEmpty()
        {
            return sentinel.Next == sentinel;
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

        private void AddToEmptyList(LinkedListNode<T> node)
        {
            sentinel.Next = node;
            node.Previous = sentinel;
            sentinel.Previous = node;
            node.Next = sentinel;
            node.List = this;
        }

        private void ValidateNodes(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            ArgumentNullException.ThrowIfNull(node);
            ArgumentNullException.ThrowIfNull(newNode);

            if (!IsNodeInList(node))
            {
                throw new InvalidOperationException("Node is not in the current list.");
            }

            if (newNode.List != null)
            {
                throw new InvalidOperationException("New node belongs to another list.");
            }
        }
    }
}
