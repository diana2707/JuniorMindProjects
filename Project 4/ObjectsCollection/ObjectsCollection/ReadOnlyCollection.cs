using System.Collections;

namespace ObjectsCollection
{
    internal class ReadOnlyCollection<T>
    {
        readonly IList<T> list;

        public ReadOnlyCollection(IList<T> list)
        {
            Validate(list);
            this.list = list;
        }

        private static void Validate(IList<T> list)
        {
            ArgumentNullException.ThrowIfNull(list);
        }
    }
}
