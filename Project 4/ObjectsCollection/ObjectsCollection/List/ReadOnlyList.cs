using System.Collections;

namespace ObjectsCollection
{
    internal class ReadOnlyList<T> : IList<T>
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

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(T element)
        {
            return list.Contains(element);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int IndexOf(T element)
        {
            return list.IndexOf(element);
        }

        public void Add(T item) => throw new NotSupportedException();

        public void Insert(int index, T element) => throw new NotSupportedException();

        public void Clear() => throw new NotSupportedException();

        public bool Remove(T element) => throw new NotSupportedException();

        public void RemoveAt(int index) => throw new NotSupportedException();

        private static void Validate(IList<T> list)
        {
            ArgumentNullException.ThrowIfNull(list);
        }
    }
}
