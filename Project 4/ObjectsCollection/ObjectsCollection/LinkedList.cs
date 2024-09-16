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
            //when node is null throw exception 

            LinkedListNode<T> newNode = new (item);

            AddFirst(newNode);
        }

        public void AddFirst(LinkedListNode<T> newNode)
        {
            //when node is null throw exception 

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
            }

            Count++;
        }

        public void AddAfter(LinkedListNode<T> node, T item)
        {
            //when node is null throw exception 
            LinkedListNode<T> newNode = new (item);

            AddAfter(node, newNode);
        }

        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            //when node is null throw exception 

            newNode.Previous = node;
            newNode.Next = node.Next;
            node.Next.Previous = newNode;
            node.Next = newNode;

            Count++;
        }

        public void AddBefore(LinkedListNode<T> node, T item)
        {
            //when node is null throw exception 

            LinkedListNode<T> newNode = new (item);

            AddBefore(node, newNode);
        }

        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            //when node is null throw exception 

            newNode.Next = node;
            newNode.Previous = node.Previous;
            node.Previous.Next = newNode;
            node.Previous = newNode;

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
            LinkedListNode<T> toRemove = Search(item, true);

            if (toRemove == null)
            {
                return false;
            }

            toRemove.Previous.Next = toRemove.Next;
            toRemove.Next.Previous = toRemove.Previous;

            return true;
        }

        public bool Remove(LinkedListNode<T> node)
        {
            if (node == null)
            {
                return false;
            }

            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;

            return true;
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

        private void AddToEmptyList(LinkedListNode<T> node)
        {
            sentinel.Next = node;
            node.Previous = sentinel;
            sentinel.Previous = node;
            node.Next = sentinel;
        }
    }
}
