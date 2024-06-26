using System.Collections;

namespace ObjectsCollection
{
    internal class ObjectArrayEnum : IEnumerator
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

        public void Reset()
        {
            position = -1;
        }
    }
}
