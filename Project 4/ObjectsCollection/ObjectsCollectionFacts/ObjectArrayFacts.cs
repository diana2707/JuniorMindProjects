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
        public void IndexOfMethodShouldReturnCorrectIndexOfElementWhenStringElementExists()
        {
            ObjectArray array = new ObjectArray();
            array.Add("a");
            array.Add("b");
            array.Add("c");

            Assert.Equal(1, array.IndexOf("b"));
        }

        [Fact]
        public void IndexOfMethodShouldReturnCorrectIndexOfElementWhenIntegerElementExists()
        {
            ObjectArray array = new ObjectArray();
            array.Add(1);
            array.Add(2);
            array.Add(3);

            Assert.Equal(2, array.IndexOf(3));
        }

        [Fact]
        public void IndexOfMethodShouldReturnCorrectIndexOfElementWhenFloatElementExists()
        {
            ObjectArray array = new ObjectArray();
            array.Add(1.5);
            array.Add(2.5);
            array.Add(3.5);

            Assert.Equal(2, array.IndexOf(3.5));
        }

        [Fact]
        public void IndexOfMethodShouldReturnCorrectIndexOfElementWhenArrayElementExists()
        {
            int[] arr1 = { 1, 2, 3 };
            int[] arr2 = { 4, 5, 6 };
            int[] arr3 = { 7, 8, 9 };

            ObjectArray array = new ObjectArray();
            array.Add(arr1);
            array.Add(arr2);
            array.Add(arr3);

            Assert.Equal(1, array.IndexOf(arr2));
        }

        [Fact]
        public void IndexOfMethodShouldReturnCorrectIndexOfElementWhenObjectElementExists()
        {
            IntArray arr1 = new IntArray();
            IntArray arr2 = new IntArray();
            IntArray arr3 = new IntArray();

            ObjectArray array = new ObjectArray();
            array.Add(arr1);
            array.Add(arr2);
            array.Add(arr3);

            Assert.Equal(1, array.IndexOf(arr2));
        }

        [Fact]
        public void IndexOfMethodShouldReturnMinus1WhenStringElementDoNotExists()
        {
            ObjectArray array = new ObjectArray();
            array.Add("a");
            array.Add("b");
            array.Add("c");

            Assert.Equal(-1, array.IndexOf("z"));
        }

        [Fact]
        public void IndexOfMethodShouldReturnMinus1WhenIntegerElementDoNotExists()
        {
            ObjectArray array = new ObjectArray();
            array.Add(1);
            array.Add(2);
            array.Add(3);

            Assert.Equal(-1, array.IndexOf(4));
        }

        [Fact]
        public void IndexOfMethodShouldReturnMinus1WhenFloatElementDoNotExists()
        {
            ObjectArray array = new ObjectArray();
            array.Add(1.5);
            array.Add(2.5);
            array.Add(3.5);

            Assert.Equal(-1, array.IndexOf(4.5));
        }

        [Fact]
        public void IndexOfMethodShouldReturnMinus1WhenArrayElementDoNotExists()
        {
            int[] arr1 = { 1, 2, 3 };
            int[] arr2 = { 4, 5, 6 };
            int[] arr3 = { 7, 8, 9 };
            int[] arr4 = { 10, 11, 12 };

            ObjectArray array = new ObjectArray();
            array.Add(arr1);
            array.Add(arr2);
            array.Add(arr3);

            Assert.Equal(-1, array.IndexOf(arr4));
        }

        [Fact]
        public void ContainsMethodShouldReturnTrueWhenElementExists()
        {
            int[] arr = { 1, 2, 3 };
            IntArray obj = new IntArray();

            ObjectArray array = new ObjectArray();
            array.Add("a");
            array.Add(1);
            array.Add(2.5);
            array.Add(arr);
            array.Add(obj);

            Assert.True(array.Contains("a"));
            Assert.True(array.Contains(1));
            Assert.True(array.Contains(2.5));
            Assert.True(array.Contains(arr));
            Assert.True(array.Contains(obj));
        }

        [Fact]
        public void ContainsMethodShouldReturnFalseWhenElementDoNotExist()
        {
            int[] arr1 = { 1, 2, 3 };
            int[] arr2 = { 4, 5, 6 };
            IntArray obj1 = new IntArray();
            IntArray obj2 = new IntArray();

            ObjectArray array = new ObjectArray();
            array.Add("a");
            array.Add(1);
            array.Add(2.5);
            array.Add(arr1);
            array.Add(obj1);

            Assert.False(array.Contains("b"));
            Assert.False(array.Contains(2));
            Assert.False(array.Contains(2.4));
            Assert.False(array.Contains(arr2));
            Assert.False(array.Contains(obj2));
        }
    }
}
