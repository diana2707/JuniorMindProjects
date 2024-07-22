using System.Collections;

namespace ObjectsCollection
{
    internal class ReadOnlyList<T>
    {
        readonly IList<T> list;

        public ReadOnlyList(IList<T> list)
        {
            Validate(list);
            this.list = list;
            Count = list.Count;
            IsReadOnly = true;
        }

        public int Count { get; }

        public bool IsReadOnly { get; }

        public T this[int index]
        {
            get => list[index];

            set => throw new NotSupportedException();
        }

        private static void Validate(IList<T> list)
        {
            ArgumentNullException.ThrowIfNull(list);
        }
    }
}
