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
        }

        public int Count { get; }

        public T this[int index]
        {
            get => list[index];
        }

        private static void Validate(IList<T> list)
        {
            ArgumentNullException.ThrowIfNull(list);
        }
    }
}
