using System;

namespace ObjectsCollection
{
    public class ListFacts
    {
        [Fact]
        public void IndexerShouldSetAndGetIntegerElementAsObject()
        {
            const int number = 5;

            List<int> array = new List<int>();
            array[0] = number;

            Assert.Equal(5, array[0]);
        }

        [Fact]
        public void IndexerShouldSetAndGetFloatElementAsObject()
        {
            const double number = 7.897;

            List<double> array = new List<double>();
            array[0] = number;

            Assert.Equal(7.897, array[0]);
        }

        [Fact]
        public void IndexerShouldSetAndGetStringElementAsObject()
        {
            const string text = "abc";

            List<string> array = new List<string>();
            array[0] = text;

            Assert.Equal("abc", array[0]);
        }

        [Fact]
        public void IndexerShouldSetAndGetArrayElementAsObject()
        {
            int[] integerArray = new int[1];

            List<int[]> array = new List<int[]>();
            array[0] = integerArray;

            Assert.Equal(integerArray, array[0]);
        }

        [Fact]
        public void AddMethodShouldAddOneElement()
        {
            const string text = "abc";
            List<string> array = new List<string>();
            array.Add(text);

            Assert.Equal("abc", array[0]);
        }

        [Fact]
        public void AddMethodShouldAddOneElementWhenArrayHasOver4Elements()
        {
            List<int> array = new List<int>();
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
            List<int> array = new List<int>();
            array.Add(5);
            array.Add(6);

            Assert.Equal(2, array.Count);
        }

        [Fact]
        public void IndexOfMethodShouldReturnCorrectIndexOfElementWhenStringElementExists()
        {
            List<string> array = new List<string>();
            array.Add("a");
            array.Add("b");
            array.Add("c");

            Assert.Equal(1, array.IndexOf("b"));
        }

        [Fact]
        public void IndexOfMethodShouldReturnCorrectIndexOfElementWhenIntegerElementExists()
        {
            List<int> array = new List<int>();
            array.Add(1);
            array.Add(2);
            array.Add(3);

            Assert.Equal(2, array.IndexOf(3));
        }

        [Fact]
        public void IndexOfMethodShouldReturnCorrectIndexOfElementWhenDoubleElementExists()
        {
            List<double> array = new List<double>();
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

            List<int[]> array = new List<int[]>();
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

            List<IntArray> array = new List<IntArray>();
            array.Add(arr1);
            array.Add(arr2);
            array.Add(arr3);

            Assert.Equal(1, array.IndexOf(arr2));
        }

        [Fact]
        public void IndexOfMethodShouldReturnMinus1WhenStringElementDoNotExists()
        {
            List<string> array = new List<string>();
            array.Add("a");
            array.Add("b");
            array.Add("c");

            Assert.Equal(-1, array.IndexOf("z"));
        }

        [Fact]
        public void IndexOfMethodShouldReturnMinus1WhenIntegerElementDoNotExists()
        {
            List<int> array = new List<int>();
            array.Add(1);
            array.Add(2);
            array.Add(3);

            Assert.Equal(-1, array.IndexOf(4));
        }

        [Fact]
        public void IndexOfMethodShouldReturnMinus1WhenDoubleElementDoNotExists()
        {
            List<double> array = new List<double>();
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

            List<int[]> array = new List<int[]>();
            array.Add(arr1);
            array.Add(arr2);
            array.Add(arr3);

            Assert.Equal(-1, array.IndexOf(arr4));
        }

        [Fact]
        public void InsertMethodShouldInsertCorrectIntegerElement()
        {
            List<int> array = new List<int>();
            array.Add(3);
            array.Add(5);
            array.Add(6);
            array.Insert(1, 4);

            Assert.Equal(4, array[1]);
            Assert.Equal(6, array[3]);
        }

        [Fact]
        public void InsertMethodShouldInsertCorrectStringElement()
        {
            List<string> array = new List<string>();
            array.Add("a");
            array.Add("b");
            array.Add("c");
            array.Insert(1, "?");

            Assert.Equal("?", array[1]);
            Assert.Equal("c", array[3]);
        }

        [Fact]
        public void InsertMethodShouldInsertCorrectDoubleElement()
        {
            List<double> array = new List<double>();
            array.Add(3.4);
            array.Add(2.1);
            array.Add(7.8);
            array.Insert(1, 4.7);

            Assert.Equal(4.7, array[1]);
            Assert.Equal(7.8, array[3]);
        }

        [Fact]
        public void InsertMethodShouldInsertCorrectArrayElement()
        {
            int[] arr1 = { 1, 2, 3 };
            int[] arr2 = { 4, 5, 6 };
            int[] arr3 = { 7, 8, 9 };

            List<int[]> test = new List<int[]>();
            test.Add(arr1);
            test.Add(arr2);
            test.Insert(1, arr3);

            Assert.Equal(arr3, test[1]);
            Assert.Equal(arr2, test[2]);
        }

        [Fact]
        public void InsertMethodShouldInsertCorrectObjectElement()
        {
            List<IntArray> arr1 = new List<IntArray>();
            List<IntArray> arr2 = new List<IntArray>();
            List<IntArray> arr3 = new List<IntArray>();

            List<List<IntArray>> test = new List<List<IntArray>>();
            test.Add(arr1);
            test.Add(arr2);
            test.Insert(1, arr3);

            Assert.Equal(arr3, test[1]);
            Assert.Equal(arr2, test[2]);
        }

        [Fact]
        public void ClearMethodShouldSetAllElementToDefault()
        {
            List<int> array = new List<int>();
            array.Add(3);
            array.Add(4);
            array.Clear();

            Assert.Equal(0, array.Count);
        }

        [Fact]
        public void RemoveAtMethodShouldRemoveTheCorrectIntegerElement()
        {
            List<int> array = new List<int>();
            array.Add(3);
            array.Add(7);
            array.Add(5);
            array.RemoveAt(1);

            Assert.Equal(3, array[0]);
            Assert.Equal(5, array[1]);
        }

        [Fact]
        public void RemoveAtMethodShouldRemoveTheCorrectArrayElement()
        {
            int[] arr1 = { 1, 2, 3 };
            int[] arr2 = { 4, 5, 6 };
            int[] arr3 = { 7, 8, 9 };

            List<int[]> array = new List<int[]>();
            array.Add(arr1);
            array.Add(arr2);
            array.Add(arr3);
            array.RemoveAt(1);

            Assert.Equal(arr1, array[0]);
            Assert.Equal(arr3, array[1]);
        }

        [Fact]
        public void RemoveAtMethodShouldRemoveTheCorrectStringElement()
        {
            List<string> array = new List<string>();
            array.Add("a");
            array.Add("b");
            array.Add("c");
            array.RemoveAt(1);

            Assert.Equal("a", array[0]);
            Assert.Equal("c", array[1]);
        }

        [Fact]
        public void RemoveAtMethodShouldNotChangeIntegerWhenIndexDoesNotExist()
        {
            List<int> array = new List<int>();
            array.Add(3);
            array.Add(4);
            array.Add(5);
            array.RemoveAt(5);

            Assert.Equal(3, array[0]);
            Assert.Equal(4, array[1]);
            Assert.Equal(5, array[2]);
        }

        [Fact]
        public void RemoveMethodShouldRemoveTheCorrectIntegerElement()
        {
            List<int> array = new List<int>();
            array.Add(3);
            array.Add(4);
            array.Add(5);
            array.Remove(4);

            Assert.Equal(3, array[0]);
            Assert.Equal(5, array[1]);
        }

        [Fact]
        public void RemoveMethodShouldRemoveTheCorrectStringElement()
        {
            List<string> array = new List<string>();
            array.Add("a");
            array.Add("b");
            array.Add("c");
            array.Remove("a");

            Assert.Equal("b", array[0]);
            Assert.Equal("c", array[1]);
        }

        [Fact]
        public void RemoveMethodShouldRemoveTheCorrectArrayElement()
        {
            int[] arr1 = { 1, 2, 3 };
            int[] arr2 = { 4, 5, 6 };
            int[] arr3 = { 7, 8, 9 };

            List<int[]> array = new List<int[]>();
            array.Add(arr1);
            array.Add(arr2);
            array.Add(arr3);
            array.Remove(arr1);

            Assert.Equal(arr2, array[0]);
            Assert.Equal(arr3, array[1]);
        }

        [Fact]
        public void VerifyIEnumerableInterfaceAllowsForeachUse()
        {
            foreach (var item in new List<int> { 1, 2, 3 })
            {
                Assert.NotNull(item);
                Assert.Equal(item, item);
            }
        }
    }
}
