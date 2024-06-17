using System;

namespace ObjectsCollection
{
    internal class ObjectArray
    {
        readonly object[] array;

        public ObjectArray()
        {
            array = new object[4];
        }

        public object this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }
    }
}
