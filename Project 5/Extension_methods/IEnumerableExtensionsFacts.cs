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

        [Fact]
        public void AnyMethodShouldReturnFalseWhenNoElementMatchesPredicate()
        {
            IEnumerable<int> values = [2, 9, 12, 18];
            Assert.True(IEnumerableExtensions.Any(values, e => e % 3 == 0));
        }

        [Fact]
        public void AnyMethodShouldReturnTrueWhenNotAtLeastOneElementsMatchesPredicate()
        {
            IEnumerable<int> values = [5, 7, 13, 14];
            Assert.False(IEnumerableExtensions.Any(values, e => e % 3 == 0));
        }

        [Fact]
        public void FirstMethodShouldReturnFirstElementThatMatchesPredicate()
        {
            IEnumerable<int> values = [7, 9, 10, 11, 15];
            Assert.Equal(10, IEnumerableExtensions.First(values, e => 30 % e == 0));
        }

        [Fact]
        public void FirstMethodShouldThrowIvalidOperationExceptionWhenNoElementMatchesPredicate()
        {
            IEnumerable<int> values = [7, 9, 11, 13, 17];

            Assert.Throws<InvalidOperationException>(() => IEnumerableExtensions.First(values, e => 30 % e == 0));
        }

        [Fact]
        public void SelectMethodShouldReturnElementsInFormThatMatchesPredicate()
        {
            IEnumerable<string> values = [ "apple", "grape", "orange", "banana"];
            List<char> expected = ['a', 'g', 'o', 'b'];
            IEnumerable<char> result = IEnumerableExtensions.Select(values, e => e[0]);

            foreach (var item in expected)
            {
                Assert.Contains(item, result);
            }
        }
    }
}