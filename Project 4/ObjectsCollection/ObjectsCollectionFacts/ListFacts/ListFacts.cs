namespace ObjectsCollection
{
    public class ListFacts
    {
        [Fact]
        public void IndexerShouldSetAndGetIntegerElementAsObject()
        {
            const int number = 5;
            CustomList<int> array = new CustomList<int> { 0 };
            array[0] = number;

            Assert.Equal(5, array[0]);
        }

        [Fact]
        public void IndexerShouldSetAndGetFloatElementAsObject()
        {
            const double number = 7.897;

            CustomList<double> array = new CustomList<double> { 0 };
            array[0] = number;

            Assert.Equal(7.897, array[0]);
        }

        [Fact]
        public void IndexerShouldSetAndGetStringElementAsObject()
        {
            const string text = "abc";

            CustomList<string> array = new CustomList<string> { "" };
            array[0] = text;

            Assert.Equal("abc", array[0]);
        }

        [Fact]
        public void IndexerShouldSetAndGetArrayElementAsObject()
        {
            int[] integerArray = new int[1];

            CustomList<int[]> array = new CustomList<int[]> { new[] { 0 } };
            array[0] = integerArray;

            Assert.Equal(integerArray, array[0]);
        }

        [Fact]
        public void AddMethodShouldAddOneElement()
        {
            const string text = "abc";
            CustomList<string> array = new CustomList<string>();
            array.Add(text);

            Assert.Equal("abc", array[0]);
        }

        [Fact]
        public void AddMethodShouldAddOneElementWhenArrayHasOver4Elements()
        {
            CustomList<int> array = new CustomList<int>();
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
            CustomList<int> array = new CustomList<int>();
            array.Add(5);
            array.Add(6);

            Assert.Equal(2, array.Count);
        }

        [Fact]
        public void IndexOfMethodShouldReturnCorrectIndexOfElementWhenStringElementExists()
        {
            CustomList<string> array = new CustomList<string>();
            array.Add("a");
            array.Add("b");
            array.Add("c");

            Assert.Equal(1, array.IndexOf("b"));
        }

        [Fact]
        public void IndexOfMethodShouldReturnCorrectIndexOfElementWhenIntegerElementExists()
        {
            CustomList<int> array = new CustomList<int>();
            array.Add(1);
            array.Add(2);
            array.Add(3);

            Assert.Equal(2, array.IndexOf(3));
        }

        [Fact]
        public void IndexOfMethodShouldReturnCorrectIndexOfElementWhenDoubleElementExists()
        {
            CustomList<double> array = new CustomList<double>();
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

            CustomList<int[]> array = new CustomList<int[]>();
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

            CustomList<IntArray> array = new CustomList<IntArray>();
            array.Add(arr1);
            array.Add(arr2);
            array.Add(arr3);

            Assert.Equal(1, array.IndexOf(arr2));
        }

        [Fact]
        public void IndexOfMethodShouldReturnMinus1WhenStringElementDoNotExists()
        {
            CustomList<string> array = new CustomList<string>();
            array.Add("a");
            array.Add("b");
            array.Add("c");

            Assert.Equal(-1, array.IndexOf("z"));
        }

        [Fact]
        public void IndexOfMethodShouldReturnMinus1WhenIntegerElementDoNotExists()
        {
            CustomList<int> array = new CustomList<int>();
            array.Add(1);
            array.Add(2);
            array.Add(3);

            Assert.Equal(-1, array.IndexOf(4));
        }

        [Fact]
        public void IndexOfMethodShouldReturnMinus1WhenDoubleElementDoNotExists()
        {
            CustomList<double> array = new CustomList<double>();
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

            CustomList<int[]> array = new CustomList<int[]>();
            array.Add(arr1);
            array.Add(arr2);
            array.Add(arr3);

            Assert.Equal(-1, array.IndexOf(arr4));
        }

        [Fact]
        public void InsertMethodShouldInsertCorrectIntegerElement()
        {
            CustomList<int> array = new CustomList<int>();
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
            CustomList<string> array = new CustomList<string>();
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
            CustomList<double> array = new CustomList<double>();
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

            CustomList<int[]> test = new CustomList<int[]>();
            test.Add(arr1);
            test.Add(arr2);
            test.Insert(1, arr3);

            Assert.Equal(arr3, test[1]);
            Assert.Equal(arr2, test[2]);
        }

        [Fact]
        public void InsertMethodShouldInsertCorrectObjectElement()
        {
            CustomList<IntArray> arr1 = new CustomList<IntArray>();
            CustomList<IntArray> arr2 = new CustomList<IntArray>();
            CustomList<IntArray> arr3 = new CustomList<IntArray>();

            CustomList<CustomList<IntArray>> test = new CustomList<CustomList<IntArray>>();
            test.Add(arr1);
            test.Add(arr2);
            test.Insert(1, arr3);

            Assert.Equal(arr3, test[1]);
            Assert.Equal(arr2, test[2]);
        }

        [Fact]
        public void ClearMethodShouldSetAllElementToDefault()
        {
            CustomList<int> array = new CustomList<int>();
            array.Add(3);
            array.Add(4);
            array.Clear();

            Assert.Equal(0, array.Count);
        }

        [Fact]
        public void RemoveAtMethodShouldRemoveTheCorrectIntegerElement()
        {
            CustomList<int> array = new CustomList<int>();
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

            CustomList<int[]> array = new CustomList<int[]>();
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
            CustomList<string> array = new CustomList<string>();
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
            CustomList<int> array = new CustomList<int>();
            array.Add(3);
            array.Add(4);
            array.Add(5);

            Assert.Throws<ArgumentOutOfRangeException>(() => array.RemoveAt(5));
            Assert.Equal(3, array[0]);
            Assert.Equal(4, array[1]);
            Assert.Equal(5, array[2]);
        }

        [Fact]
        public void RemoveMethodShouldRemoveTheCorrectIntegerElement()
        {
            CustomList<int> array = new CustomList<int>();
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
            CustomList<string> array = new CustomList<string>();
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

            CustomList<int[]> array = new CustomList<int[]>();
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
            foreach (var item in new CustomList<int> { 1, 2, 3 })
            {
                Assert.NotNull(item);
                Assert.Equal(item, item);
            }
        }

        [Fact]
        public void IsReadOnlyPropertyShouldReturnFalse()
        {
            CustomList<int> list = new CustomList<int> { 1, 3, 5, 7 };
            Assert.False(list.IsReadOnly);
        }

        [Fact]
        public void CopyToMethodShouldCopyElementsToSpecifiedArrayStartingFrom0()
        {
            CustomList<int> list = new CustomList<int> { 1, 3, 5, 7, 8 };
            int[] array = new int[5];
            list.CopyTo(array, 0);
            Assert.Equal(1, array[0]);
            Assert.Equal(3, array[1]);
            Assert.Equal(5, array[2]);
            Assert.Equal(7, array[3]);
            Assert.Equal(8, array[4]);
        }

        [Fact]
        public void CopyToMethodShouldCopyElementsToSpecifiedArrayStartingFromMiddleIndex()
        {
            CustomList<int> list = new CustomList<int> { 1, 3, 5, 7 };
            int[] array = new int[10];
            list.CopyTo(array, 6);
            Assert.Equal(1, array[6]);
            Assert.Equal(3, array[7]);
            Assert.Equal(5, array[8]);
            Assert.Equal(7, array[9]);
        }

        [Fact]
        public void RemoveMethodShouldReturnTrueWhenElementIsRemoved()
        {
            CustomList<int> list = new CustomList<int> { 1, 3, 5, 7 };
            Assert.True(list.Remove(1));
        }

        [Fact]
        public void RemoveMethodShouldReturnFalseWhenElementIsNotFound()
        {
            CustomList<int> list = new CustomList<int> { 1, 3, 5, 7 };
            Assert.False(list.Remove(2));
        }

        [Fact]
        public void ListObjectIsAlsoAnIListObject()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7 };
            Assert.NotEmpty(list);
        }

        [Fact]
        public void GetterThrowsArgumentOutOfRangeExceptionWhenIndexIsEqualToCount()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7, 9 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list[list.Count]);
        }

        [Fact]
        public void GetterThrowsArgumentOutOfRangeExceptionWhenIndexIsLessThan0()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7, 9 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list[-1]);
        }

        [Fact]
        public void GetterThrowsArgumentOutOfRangeExceptionWhenIndexIsLargerThanCount()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7, 9 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list[10]);
        }

        [Fact]
        public void SetterThrowsArgumentOutOfRangeExceptionWhenIndexIsEqualToCount()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7, 9 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list[list.Count] = 3);
        }

        [Fact]
        public void SetterThrowsArgumentOutOfRangeExceptionWhenIndexIsLargerThanCount()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7, 9 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list[10] = 3);
        }

        [Fact]
        public void SetterThrowsArgumentOutOfRangeExceptionWhenIndexIsLessThan0()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7, 9 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list[-3] = 3);
        }

        [Fact]
        public void CopyToMethodThrowsArgumentOutOfRangeExceptionWhenIndexIsLessThan0()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7 };
            int[] array = new int[4];
            Assert.Throws<ArgumentOutOfRangeException>(() => list.CopyTo(array, -1));
        }

        [Fact]
        public void CopyToMethodThrowsArgumentNullExceptionWhenArrayIsNull()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7 };
            int[] array = null;
            Assert.Throws<ArgumentNullException>(() => list.CopyTo(array, 0));
        }

        [Fact]
        public void CopyToMethodThrowsArgumentExceptionWhenNumberofElementsIsGreaterThanAllocatedSapce()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7, 8 };
            int[] array = new int[6];
            Assert.Throws<ArgumentException>(() => list.CopyTo(array, 2));
        }

        [Fact]
        public void CopyToMethodThrowsArgumentExceptionWhenNumberofElementsIsGreaterThanAllocatedSapcejj()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7, 8 };
            int[] array = new int[5];
            Assert.Throws<ArgumentException>(() => list.CopyTo(array, 10));
        }

        [Fact]
        public void InsertMethodThrowsArgumentOutOfRangeExceptionWhenIndexIsNegative()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Insert(-1, 0));
        }

        [Fact]
        public void InsertMethodThrowsArgumentOutOfRangeExceptionWhenIndexIsEqualToCount()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Insert(list.Count, 0));
        }

        [Fact]
        public void InsertMethodThrowsArgumentOutOfRangeExceptionWhenIndexIsGreaterThanCount()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Insert(10, 0));
        }

        [Fact]
        public void RemoveAtMethodThrowsArgumentOutOfRangeExceptionWhenIndexIsNegative()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
        }

        [Fact]
        public void RemoveAtMethodThrowsArgumentOutOfRangeExceptionWhenIndexIsEqualToCount()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(list.Count));
        }

        [Fact]
        public void RemoveAtMethodThrowsArgumentOutOfRangeExceptionWhenIndexIsGreaterThanCount()
        {
            IList<int> list = new CustomList<int> { 1, 3, 5, 7 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(10));
        }
    }
}
