using System.Collections;

namespace ObjectsCollection
{
    internal class ObjectArrayEnum : IEnumerator
    {
        readonly object[] array;
        readonly int count;
        private int position = -1;

        public ObjectArrayEnum(object[] array, int count)
        {
            this.array = array;
            this.count = count;
        }

        public object Current
        {
            get => array[position];
        }

        public bool MoveNext()
        {
            position++;
            return position < count;
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
