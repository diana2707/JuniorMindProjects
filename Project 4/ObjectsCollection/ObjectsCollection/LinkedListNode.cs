using System;

namespace ObjectsCollection
{
    internal class LinkedListNode<T>
    {
        public LinkedListNode(T value)
        {
            Value = value;
            Next = null;
        }

        public T Value { get; set; }

        public LinkedListNode<T>? Next { get; }
    }
}
