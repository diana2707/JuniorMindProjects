using System.Xml.Linq;

namespace ObjectsCollection
{
    internal class IntArray
    {
        private int[] array;

        public IntArray()
        {
            array = new int[1];
        }

        public void Add(int element)
        {
            array = new int[array.Length + 1];
            array[array.Length - 1] = element;
        }
    }
}
