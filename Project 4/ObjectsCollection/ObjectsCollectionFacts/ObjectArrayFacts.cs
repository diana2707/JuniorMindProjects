using System;
using System.Numerics;

namespace ObjectsCollection
{
    public class ObjectArrayFacts
    {
        [Fact]
        public void AddMethodShouldAddOneElement()
        {
            object number = 5;

            ObjectArray array = new ObjectArray();
            array[0] = number;

            Assert.Equal(5, array[0]);
        }
    }
}
