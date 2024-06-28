using System.Collections;

namespace ObjectsCollection
{
    internal class ObjectArrayEnum : IEnumerator
    {
        readonly ObjectArray objectArray;
        private int position = -1;

        public ObjectArrayEnum(ObjectArray objectArray)
        {
            this.objectArray = objectArray;
        }

        public object Current
        {
            get => objectArray[position];
        }

        public bool MoveNext()
        {
            position++;
            return position < objectArray.Count;
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
