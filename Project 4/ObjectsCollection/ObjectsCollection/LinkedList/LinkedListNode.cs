namespace ObjectsCollection
{
    public class LinkedListNode<T>
    {
        public LinkedListNode(T value)
        {
            Value = value;
            Next = null;
            Previous = null;
            List = null;
        }

        public T Value { get; set; }

        public LinkedListNode<T>? Next { get; internal set; }

        public LinkedListNode<T>? Previous { get; internal set; }

        public LinkedList<T>? List { get; internal set; }
    }
}
