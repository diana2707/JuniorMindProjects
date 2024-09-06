using System;

namespace ObjectsCollection
{
    internal class LinkedListNode<T>
    {
        public LinkedListNode(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}
