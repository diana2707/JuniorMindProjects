using System.Collections;

namespace ObjectsCollection
{
    internal class ReadOnlyCollection<T>
    {
        readonly IList<T> list;

        public ReadOnlyCollection(IList<T> list)
        {
            ArgumentNullException.ThrowIfNull(list);
            this.list = list;
        }
    }
}
