using System;

namespace Extension_methods
{
    public class IEnumerableExtensionsFacts
    {
        [Fact]
        public void AllMethodShouldReturnTrueWhenAllElementsMatchPredicate()
        {
            IEnumerable<int> values = [11, 13, 15];
            Assert.True(IEnumerableExtensions.All(values, e => e > 10));
        }

        [Fact]
        public void AllMethodShouldReturnFalseWhenNotAllElementsMatchPredicate()
        {
            IEnumerable<string> values = ["red", "purple", "orange"];
            Assert.False(IEnumerableExtensions.All(values, e => e.Length > 5));
        }
    }
}