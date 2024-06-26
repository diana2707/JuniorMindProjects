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

            ObjectArrayEnum arrayEnum = new ObjectArrayEnum(array);

            Assert.True(arrayEnum.MoveNext());
        }
    }
}
