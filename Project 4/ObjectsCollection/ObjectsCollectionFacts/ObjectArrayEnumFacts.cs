namespace ObjectsCollection
{
    public class ObjectArrayEnumFacts
    {
        [Fact]
        public void MoveNextMethodShouldReturnTrueWhenPositionIsSmallerThanArrayLength()
        {
            ObjectArray objectArray = new ObjectArray() { 1, 2 };

            ObjectArrayEnum arrayEnum = new ObjectArrayEnum(objectArray);

            Assert.True(arrayEnum.MoveNext());
        }

        [Fact]
        public void MoveNextMethodShouldReturnFalseWhenPositionIsEqualWithArrayLength()
        {
            ObjectArray objectArray = new ObjectArray() { 1, 2 };

            ObjectArrayEnum arrayEnum = new ObjectArrayEnum(objectArray);
            arrayEnum.MoveNext();
            arrayEnum.MoveNext();

            Assert.False(arrayEnum.MoveNext());
        }

        [Fact]
        public void CurrentPropertyShouldReturnCurrentPositionWhenIsSmallerThanArrayLength()
        {
            ObjectArray objectArray = new ObjectArray() { 1, 2, 3 };

            ObjectArrayEnum arrayEnum = new ObjectArrayEnum(objectArray);

            arrayEnum.MoveNext();

            Assert.Equal(1, arrayEnum.Current);
        }

        [Fact]
        public void ResetMethodShouldReturnCurrentPositionWhenIsSmallerThanArrayLength()
        {
            ObjectArray objectArray = new ObjectArray() { 1, 2, 3 };

            ObjectArrayEnum arrayEnum = new ObjectArrayEnum(objectArray);

            arrayEnum.MoveNext();
            arrayEnum.MoveNext();
            arrayEnum.Reset();

            Assert.True(arrayEnum.MoveNext());
        }
    }
}
