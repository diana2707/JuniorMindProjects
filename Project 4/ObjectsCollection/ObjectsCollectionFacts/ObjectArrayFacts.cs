using System;

namespace ObjectsCollection
{
    public class ObjectArrayFacts
    {
        [Fact]
        public void IndexerShouldSetAndGetIntegerElementAsObject()
        {
            object number = 5;

            ObjectArray array = new ObjectArray();
            array[0] = number;

            Assert.Equal(5, array[0]);
        }

        [Fact]
        public void IndexerShouldSetAndGetFloatElementAsObject()
        {
            object number = 7.897;

            ObjectArray array = new ObjectArray();
            array[0] = number;

            Assert.Equal(7.897, array[0]);
        }

        [Fact]
        public void IndexerShouldSetAndGetStringElementAsObject()
        {
            object text = "abc";

            ObjectArray array = new ObjectArray();
            array[0] = text;

            Assert.Equal("abc", array[0]);
        }

        [Fact]
        public void IndexerShouldSetAndGetArrayElementAsObject()
        {
            int[] integerArray = new int[1];

            ObjectArray array = new ObjectArray();
            array[0] = integerArray;

            Assert.Equal(integerArray, array[0]);
        }

        [Fact]
        public void AddMethodShouldAddOneElement()
        {
            object text = "abc";
            ObjectArray array = new ObjectArray();
            array.Add(text);

            Assert.Equal("abc", array[0]);
        }

        [Fact]
        public void AddMethodShouldAddOneElementWhenArrayHasOver4Elements()
        {
            ObjectArray array = new ObjectArray();
            array.Add(1);
            array.Add(2);
            array.Add(3);
            array.Add(4);
            array.Add(5);

            Assert.Equal(5, array[4]);
        }

        [Fact]
        public void CountShouldReturnCorrectNumberOfElements()
        {
            ObjectArray array = new ObjectArray();
            array.Add(5);
            array.Add(6);

            Assert.Equal(2, array.Count);
        }

        [Fact]
        public void IndexOfMethodShouldReturnCorrectIndexOfElementWhenElementExists()
        {
            ObjectArray array = new ObjectArray();
            array.Add("a");
            array.Add("b");
            array.Add("c");

            Assert.Equal(1, array.IndexOf("b"));
        }

        [Fact]
        public void IndexOfMethodShouldReturnMinus1WhenElementDoNotExists()
        {
            ObjectArray array = new ObjectArray();
            array.Add("a");
            array.Add("b");
            array.Add("c");

            Assert.Equal(-1, array.IndexOf("z"));
        }
    }
}
