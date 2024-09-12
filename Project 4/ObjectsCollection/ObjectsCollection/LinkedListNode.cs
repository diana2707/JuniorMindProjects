using System;

namespace ObjectsCollection
{
    internal class LinkedListNode<T>
    {
        internal LinkedListNode<T>? next;
        internal LinkedListNode<T>? previous;

        public LinkedListNode(T value)
        {
            Value = value;
            next = Next;
            previous = Previous;
        }

        public T Value { get; set; }

        public LinkedListNode<T>? Next { get; }

        public LinkedListNode<T>? Previous { get; }
    }
}
