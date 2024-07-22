using System.Collections;

namespace ObjectsCollection
{
    internal class ReadOnlyCollection<T>
    {
        readonly IList<T> list;

        public ReadOnlyCollection(IList<T> list)
        {
            this.list = list;
        }
    }
}
