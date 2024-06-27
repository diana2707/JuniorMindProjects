namespace ObjectsCollection
{
    public class ObjectArrayEnumFacts
    {
        [Fact]
        public void MoveNextMethodShouldReturnTrueWhenPositionIsSmallerThanArrayLength()
        {
            object[] array =
            {
             new ObjectArray(),
             new IntArray()
            };

            ObjectArrayEnum arrayEnum = new ObjectArrayEnum(array, array.Length);

            Assert.True(arrayEnum.MoveNext());
        }

        [Fact]
        public void MoveNextMethodShouldReturnFalseWhenPositionIsEqualWithArrayLength()
        {
            object[] array =
            {
             new ObjectArray(),
             new IntArray()
            };

            ObjectArrayEnum arrayEnum = new ObjectArrayEnum(array, array.Length);
            arrayEnum.MoveNext();
            arrayEnum.MoveNext();

            Assert.False(arrayEnum.MoveNext());
        }

        [Fact]
        public void CurrentPropertyShouldReturnCurrentPositionWhenIsSmallerThanArrayLength()
        {
            object[] array = { 1, 2, 3 };

            ObjectArrayEnum arrayEnum = new ObjectArrayEnum(array, array.Length);

            arrayEnum.MoveNext();

            Assert.Equal(1, arrayEnum.Current);
        }

        [Fact]
        public void ResetMethodShouldReturnCurrentPositionWhenIsSmallerThanArrayLength()
        {
            object[] array = { 1, 2 };

            ObjectArrayEnum arrayEnum = new ObjectArrayEnum(array, array.Length);

            arrayEnum.MoveNext();
            arrayEnum.MoveNext();
            arrayEnum.Reset();

            Assert.True(arrayEnum.MoveNext());
        }
    }
}
