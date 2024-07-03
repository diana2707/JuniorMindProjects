using System;

namespace ObjectsCollection
{
    internal class SortedList<T> : List<T>
        where T : IComparable<T>
    {
        public SortedList()
            : base()
        {
        }
    }
}
