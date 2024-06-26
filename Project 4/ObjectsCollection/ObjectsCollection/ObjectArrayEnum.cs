using System;

namespace ObjectsCollection
{
    internal class ObjectArrayEnum
    {
        readonly object[] array;
        private int position = -1;

        public ObjectArrayEnum(object[] array)
        {
            this.array = array;
        }

        public object Current
        {
            get => array[position];
        }

        public bool MoveNext()
        {
            position++;
            return position < array.Length;
        }
    }
}
