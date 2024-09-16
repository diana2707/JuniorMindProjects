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

        public void AddFirst(T item)
        {
            //when node is null throw exception 
            LinkedListNode<T> newNode = new (item);

            AddFirst(newNode);
        }

        public void AddFirst(LinkedListNode<T> newNode)
        {
            //when node is null throw exception 

            newNode.Next = First;
            newNode.Previous = sentinel.Previous;
            First.Previous = newNode;
            sentinel.Next = newNode;

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
    }
}
