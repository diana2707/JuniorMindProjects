using System.Collections.Generic;

namespace ObjectsCollection
{
    public class Element<TKey, TValue>
    {
        public Element(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Next = -1;
        }

        public TKey Key { get; set; }

        public TValue Value { get; set; }

        internal int Next { get; set; }

        internal bool IsActive { get; set; }
    }
}
