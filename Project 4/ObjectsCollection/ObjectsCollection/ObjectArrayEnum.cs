using System;
using System.Collections;

namespace ObjectsCollection.ObjectsCollection
{
    internal class ObjectArrayEnum : IEnumerator
    {
        readonly object[] array;

        public ObjectArrayEnum(object[] array)
        {
            this.array = array;
        }
    }
}
