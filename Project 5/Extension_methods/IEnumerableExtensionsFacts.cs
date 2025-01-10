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

        [Fact]
        public void SelectManyMethodShouldReturnElementsInNestedColectionInFlattenedCollectionAndFormToMatchPredicate()
        {
            List<List<int>> lists = [[1, 2], [3, 4], [5]];

            List<int> expected = [1, 2, 3, 4, 5];
            var result = lists.SelectMany(list => list);

            foreach (var item in result)
            {
                Assert.Contains(item, expected);
            }
        }

        [Fact]
        public void WhereMethodShouldReturnAllElementsThatMatchPredicate()
        {
            List<int> list = [ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

            List<int> expected = [5, 10];
            var result = list.Where(e => e % 5 == 0);

            foreach (var item in result)
            {
                Assert.Contains(item, expected);
            }
        }

        [Fact]
        public void ToDictionaryMethodShouldReturnElementsThatMatchPredicateAsDictionary()
        {
            List<string> list = ["Ana", "Maria", "Elena", "Daniela"];

            Dictionary<char, string> expected = new Dictionary<char, string>
            {
                { 'A', "Ana" },
                { 'M', "Maria" },
                { 'E', "Elena" },
                { 'D', "Daniela" }
            };

            var result = list.ToDictionary(e => e[0], e => e);

            foreach (var item in result)
            {
                Assert.True(expected.ContainsKey(item.Key));
                Assert.True(expected.ContainsValue(item.Value));
            }
        }

        [Fact]
        public void ZipMethodShouldReturnCombinedCollectionFrom2Enumerations()
        {
            List<string> names = ["Ana", "Maria", "Elena", "Daniela"];
            List<int> ages = [45, 23, 33, 67];

            IEnumerable<(string Name, int Age)> expected = [("Ana", 45), ("Maria", 23), ("Elena", 33), ("Daniela", 67)];

            IEnumerable<(string Name, int Age)> result = names.Zip(ages, (name, age) => (name, age));

            foreach (var item in result)
            {
                Assert.Contains(item, expected);
            }
        }

        [Fact]
        public void AggregateMethodShouldReturnResultOfAllProcessedElementsThroughPredicate()
        {
            int[] numbers = { 1, 2, 3 };

            int result = numbers.Aggregate(0, (num, seed) => num + seed);

            Assert.Equal(6, result);
        }
    }
}