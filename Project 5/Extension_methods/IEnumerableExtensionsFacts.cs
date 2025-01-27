using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace Extension_methods
{
    public class IEnumerableExtensionsFacts
    {
        [Fact]
        public void AllMethodShouldReturnTrueWhenAllElementsMatchPredicate()
        {
            IEnumerable<int> values = [11, 13, 15];
            Assert.True(values.All(e => e > 10));
        }

        [Fact]
        public void AllMethodShouldReturnTrueWhenSourceIsEmpty()
        {
            IEnumerable<string> values = [];
            Assert.True(values.All(e => e.Length > 5));
        }

        [Fact]
        public void AllMethodShouldReturnArgumentNullExceptionWhenSourceIsNull()
        {
            IEnumerable<int> values = null;
            Assert.Throws<ArgumentNullException>(() => values.All(e => e > 10));
        }

        [Fact]
        public void AllMethodShouldReturnArgumentNullExceptionWhenPredicateIsNull()
        {
            IEnumerable<int> values = [11, 13, 15];
            Assert.Throws<ArgumentNullException>(() => values.All(null));
        }

        [Fact]
        public void AllMethodShouldReturnFalseWhenNotAllElementsMatchPredicate()
        {
            IEnumerable<string> values = ["red", "purple", "orange"];
            Assert.False(values.All(e => e.Length > 5));
        }

        [Fact]
        public void AllMethodShouldReturnFalseWhenElementIsNull()
        {
            IEnumerable<string> values = ["red", "purple", "orange", null];
            Assert.False(values.All(e => e.Length > 5));
        }

        [Fact]
        public void AnyMethodShouldReturnFalseWhenNoElementMatchesPredicate()
        {
            IEnumerable<int> values = [2, 9, 12, 18];
            Assert.True(values.Any(e => e % 3 == 0));
        }

        [Fact]
        public void AnyMethodShouldReturnFalseWhenCollectionIsEmpty()
        {
            IEnumerable<int> values = [];
            Assert.False(values.Any(e => e % 3 == 0));
        }

        [Fact]
        public void AnyMethodShouldReturnTrueWhenNotAtLeastOneElementMatchesPredicate()
        {
            IEnumerable<int> values = [5, 7, 13, 14];
            Assert.False(values.Any(e => e % 3 == 0));
        }

        [Fact]
        public void AnyMethodShouldIgnoreNullElementAsLongAsPredicateHandlesItCorrectly()
        {
            IEnumerable<string> values = [null, "red", "purple", "orange"];
            Assert.True(values.Any(e => (e?.Length ?? 0) > 5));
        }

        [Fact]
        public void FirstMethodShouldReturnFirstElementThatMatchesPredicate()
        {
            IEnumerable<int> values = [7, 9, 10, 11, 15];
            Assert.Equal(10, values.First(e => 30 % e == 0));
        }

        [Fact]
        public void FirstMethodShouldThrowIvalidOperationExceptionWhenNoElementMatchesPredicate()
        {
            IEnumerable<int> values = [7, 9, 11, 13, 17];

            Assert.Throws<InvalidOperationException>(() => values.First(e => 30 % e == 0));
        }

        [Fact]
        public void FirstMethodShouldThrowIvalidOperationExceptionWhenSourceIsEmpty()
        {
            IEnumerable<int> values = [];

            Assert.Throws<InvalidOperationException>(() => values.First(e => 30 % e == 0));
        }

        [Fact]
        public void FirstMethodShouldThrowArgumentNullExceptionWhenSourceIsNull()
        {
            IEnumerable<int> values = null;

            Assert.Throws<ArgumentNullException>(() => values.First(e => 30 % e == 0));
        }

        [Fact]
        public void FirstMethodShouldIgnoreNullElementAsLongAsPredicateHandlesItCorrectly()
        {
            IEnumerable<string> values = [null, "red", "purple", "orange"];
            Assert.Equal("purple", values.First(e => (e?.Length ?? 0) > 5));
        }

        [Fact]
        public void SelectMethodShouldReturnElementsInFormThatMatchesPredicate()
        {
            IEnumerable<string> values = ["apple", "grape", "orange", "banana"];
            List<char> expected = ['a', 'g', 'o', 'b'];
            IEnumerable<char> result = values.Select(e => e[0]);

            foreach (var item in expected)
            {
                Assert.Contains(item, result);
            }
        }

        [Fact]
        public void SelectMethodShouldThrowArgumentNullExceptionWhenSourceIsNull()
        {
            IEnumerable<string> values = null;

            var result = values.Select(e => e[0]);

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var item in result)
                {
                }
            });
        }

        [Fact]
        public void SelectMethodShouldReturnEmptyCollectionWhenSourceIsEmpty()
        {
            IEnumerable<int> values = [];

            List<int> result = [];

            result.AddRange(values.Select(e => e));

            Assert.Empty(result);
        }

        [Fact]
        public void SelectMethodShouldIgnoreNullElementsAsLongAsSelectorHandlesThem()
        {
            IEnumerable<string> values = [null, "apple", "grape", "orange", "banana"];
            List<char> expected = ['\0', 'a', 'g', 'o', 'b'];
            IEnumerable<char> result = values.Select(e => e?[0] ?? '\0');

            foreach (var item in expected)
            {
                Assert.Contains(item, result);
            }
        }

        [Fact]
        public void SelectManyMethodShouldReturnElementsFromNestedColectionInFlattenedCollectionAndFormToMatchPredicate()
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
        public void SelectManyMethodShouldReturnElementsFromFlatCollection()
        {
            List<int> list = [1, 2, 3, 4, 5];

            List<int> expected = [1, 2, 3, 4, 5];
            var result = list.SelectMany(e => new List<int> { e });

            foreach (var item in result)
            {
                Assert.Contains(item, expected);
            }
        }

        [Fact]
        public void SelectManyMethodShouldThrowArgumentNullExceptionWhenSourceIsNull()
        {
            List<List<int>> list = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var item in list.SelectMany(list => list))
                {
                }
            });
        }

        [Fact]
        public void SelectManyMethodShouldThrowArgumentNullExceptionWhenSelectorIsNull()
        {
            List<int> list = [1, 2, 3, 4, 5];
            Func<int, IEnumerable<int>> selector = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var item in list.SelectMany(selector))
                {
                }
            });
        }

        [Fact]
        public void SelectManyMethodShouldReturnEmptyCollectionWhenSourceIsEmpty()
        {
            List<int> list = [];

            List<int> result = [];
            result.AddRange(list.SelectMany(e => new List<int> { e }));

            Assert.Empty(result);
        }

        [Fact]
        public void SelectManyMethodShouldIgnoreNullElementAsLongAsSelectorHandlesIt()
        {
            List<List<string>> values = [null, ["grape", "orange"], ["banana"]];
            List<string> expected = [string.Empty, "grape", "banana"];
            IEnumerable<string> result = values.Select(e => e?[0] ?? string.Empty);

            int count = 0;

            foreach (var item in expected)
            {
                Assert.Contains(item, result);
                count++;
            }

            Assert.Equal(3, count);
        }

        [Fact]
        public void WhereMethodShouldReturnAllElementsThatMatchPredicate()
        {
            List<int> list = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

            List<int> expected = [5, 10];
            var result = list.Where(e => e % 5 == 0);

            foreach (var item in result)
            {
                Assert.Contains(item, expected);
            }
        }

        [Fact]
        public void WhereMethodShouldThrowArgumentNullExceptionWhenSourceIsNull()
        {
            List<int> list = null;

            var result = list.Where(e => e % 5 == 0);

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var item in result)
                {
                }
            });
        }

        [Fact]
        public void WhereMethodShouldThrowArgumentNullExceptionWhenPredicateIsNull()
        {
            List<int> list = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
            Func<int, bool> predicate = null;

            var result = list.Where(predicate);

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var item in result)
                {
                }
            });
        }

        [Fact]
        public void WhereMethodShouldReturnEmptyCollectionWhenSourceIsEmpty()
        {
            List<int> list = [];
            List<int> result = [];

            result.AddRange(list.Where(e => e % 5 == 0));

            Assert.Empty(result);
        }

        [Fact]
        public void WhereMethodShouldReturnIgnoreNullElementAsLongAsPredicateHandlesIt()
        {
            List<string> values = [null, "apple", "grape", "orange", "banana"];
            List<string> expected = ["apple"];

            var result = values.Where(e => (e?[0] ?? '\0') == 'a');

            int count = 0;

            foreach (var value in result)
            {
                Assert.Contains(value, expected);
                count++;
            }

            Assert.Equal(1, count);
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
        public void ToDictionaryMethodShouldRetuThrowArgumentNullExceptionIfSourceIsNull()
        {
            List<string> list = null;

            Assert.Throws<ArgumentNullException>(() => list.ToDictionary(e => e[0], e => e));
        }

        [Fact]
        public void ToDictionaryMethodShouldThrowArgumentNullExceptionIfKeySelectorIsNull()
        {
            List<string> list = ["Ana", "Maria", "Elena", "Daniela"];
            Func<string, char> keySelector = null;

            Assert.Throws<ArgumentNullException>(() => list.ToDictionary(keySelector, e => e));
        }

        [Fact]
        public void ToDictionaryMethodShouldThrowArgumentNullExceptionIfElementSelectorIsNull()
        {
            List<string> list = ["Ana", "Maria", "Elena", "Daniela"];
            Func<string, string> elementSelector = null;

            Assert.Throws<ArgumentNullException>(() => list.ToDictionary(e => e[0], elementSelector));
        }

        [Fact]
        public void ToDictionaryMethodShouldThrowArgumentExceptionIfKeySelectorCretesDuplicateKey()
        {
            List<string> list = ["Ana", "Anca", "Maria", "Elena", "Daniela"];

            Assert.Throws<ArgumentException>(() => list.ToDictionary(e => e[0], e => e));
        }

        [Fact]
        public void ToDictionaryMethodShouldReturnEmptyDctionaryIfSourceIsEmpty()
        {
            List<string> list = [];

            var result = list.ToDictionary(e => e[0], e => e);

            Assert.Empty(result);
        }

        [Fact]
        public void ToDictionaryMethodShouldLetKeySelectorHandleNullElements()
        {
            List<string> list = [null, "Ana", "Maria", "Elena", "Daniela"];

            Dictionary<char, string> expected = new Dictionary<char, string>
            {
                { '\0', null },
                { 'A', "Ana" },
                { 'M', "Maria" },
                { 'E', "Elena" },
                { 'D', "Daniela" },
            };

            var result = list.ToDictionary(e => e?[0] ?? '\0', e => e);

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
        public void ZipMethodShouldThrowArgumentNullExceptionWhenFirstCollectionIsNull()
        {
            List<string> names = null;
            List<int> ages = [45, 23, 33, 67];

            IEnumerable<(string Name, int Age)> result = names.Zip(ages, (name, age) => (name, age));

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var item in result) {}
            });
        }

        [Fact]
        public void ZipMethodShouldThrowArgumentNullExceptionWhenSecondCollectionIsNull()
        {
            List<string> names = ["Ana", "Maria", "Elena", "Daniela"];
            List<int> ages = null;

            IEnumerable<(string Name, int Age)> result = names.Zip(ages, (name, age) => (name, age));

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var item in result) {}
            });
        }

        [Fact]
        public void ZipMethodShouldThrowArgumentNullExceptionWhenResultSelectorIsNull()
        {
            List<string> names = ["Ana", "Maria", "Elena", "Daniela"];
            List<int> ages = [45, 23, 33, 67];
            Func<string, int, (string, int)> resultSelector = null;

            IEnumerable<(string Name, int Age)> result = names.Zip(ages, resultSelector);

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var item in result){}
            });
        }

        [Fact]
        public void ZipMethodShouldReturnEmptyCollectionIfFirstIsEmpty()
        {
            List<string> names = [];
            List<int> ages = [45, 23, 33, 67];

            List<(string Name, int Age)> result = [];
            result.AddRange(names.Zip(ages, (name, age) => (name, age)));

            Assert.Empty(result);
        }

        [Fact]
        public void ZipMethodShouldReturnEmptyCollectionIfSecondIsEmpty()
        {
            List<string> names = ["Ana", "Maria", "Elena", "Daniela"];
            List<int> ages = [];

            List<(string Name, int Age)> result = [];
            result.AddRange(names.Zip(ages, (name, age) => (name, age)));

            Assert.Empty(result);
        }

        [Fact]
        public void ZipMethodShouldIncludeNullElementInResult()
        {
            List<string> names = ["Ana", "Maria", null, "Daniela"];
            List<int> ages = [45, 23, 33, 67];

            IEnumerable<(string Name, int Age)> expected = [("Ana", 45), ("Maria", 23), (null, 33), ("Daniela", 67)];

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

        [Fact]
        public void AggregateMethodShouldThrowArgumentNullExceptionWhenSourceIsNull()
        {
            int[] numbers = null;

            Assert.Throws<ArgumentNullException>(() => numbers.Aggregate(0, (num, seed) => num + seed));
        }

        [Fact]
        public void AggregateMethodShouldThrowArgumentNullExceptionWhenFunctIsNull()
        {
            int[] numbers = { 1, 2, 3 };

            Assert.Throws<ArgumentNullException>(() => numbers.Aggregate(0, null));
        }

        [Fact]
        public void AggregateMethodShouldReturnResultEqualWithSeedWhenCollectionIsEmpty()
        {
            int[] numbers = [];
            int seed = 0;

            int result = numbers.Aggregate(seed, (num, seed) => num + seed);

            Assert.Equal(seed, result);
        }

        [Fact]
        public void AggregateMethodShouldReturnResultWhenElementIsNull()
        {
            string[] numbers = { "Ana", "Maria", null, "Daniela" };
            string expected = ", Ana, Maria, , Daniela";

            string result = numbers.Aggregate("", (seed, value) => seed + ", " + (value ?? ""));

            Assert.Equal(expected, result);
        }

        [Fact]
        public void JoinMethodShouldReturnElementsFrom2CollectionsJoinedByMatchingKeys()
        {
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Alice" },
                new Customer { Id = 2, Name = "Bob" },
                new Customer { Id = 3, Name = "Charlie" },
            };

            var orders = new List<Order>
            {
                new Order { Id = 101, CustomerId = 1, Amount = 250 },
                new Order { Id = 102, CustomerId = 2, Amount = 100 },
                new Order { Id = 103, CustomerId = 3, Amount = 300 },
            };

            IEnumerable<(string CustomerName, int OrderId, int Amount)> result = customers.Join(
                orders,
                customer => customer.Id,
                order => order.CustomerId,
                (customer, order) => (customer.Name, order.Id, order.Amount));

            IEnumerable<(string CustomerName, int OrderId, int intAmount)> expected =
                [
                    ("Alice", 101, 250),
                    ("Bob", 102, 100),
                    ("Charlie", 103, 300),
                ];

            foreach (var item in result)
            {
                Assert.Contains(item, expected);
            }
        }

        [Fact]
        public void JoinMethodShouldThrowArgumentNullExceptionWhenOuterIsNull()
        {
            IEnumerable<Customer> customers = null;

            var orders = new List<Order>
            {
                new Order { Id = 101, CustomerId = 1, Amount = 250 },
                new Order { Id = 102, CustomerId = 2, Amount = 100 },
                new Order { Id = 103, CustomerId = 3, Amount = 300 },
            };

            IEnumerable<(string CustomerName, int OrderId, int Amount)> result = customers.Join(
                orders,
                customer => customer.Id,
                order => order.CustomerId,
                (customer, order) => (customer.Name, order.Id, order.Amount));

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var item in result) {}
            });
        }

        [Fact]
        public void JoinMethodShouldThrowArgumentNullExceptionWhenInnerIsNull()
        {
            IEnumerable<Customer> customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Alice" },
                new Customer { Id = 2, Name = "Bob" },
                new Customer { Id = 3, Name = "Charlie" },
            };

            IEnumerable<Order> orders = new List<Order>
            {
                new Order { Id = 101, CustomerId = 1, Amount = 250 },
                new Order { Id = 102, CustomerId = 2, Amount = 100 },
                new Order { Id = 103, CustomerId = 3, Amount = 300 },
            };

            Func<Customer, int> outerKeySelector = null;

            IEnumerable<(string CustomerName, int OrderId, int Amount)> result = customers.Join(
                orders,
                outerKeySelector,
                order => order.CustomerId,
                (customer, order) => (customer.Name, order.Id, order.Amount));

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var item in result) { }
            });
        }

        [Fact]
        public void JoinMethodShouldReturnEmptyCollectionWhenOuterIsEmpty()
        {
            IEnumerable<Customer> customers = [];

            IEnumerable<Order> orders = new List<Order>
            {
                new Order { Id = 101, CustomerId = 1, Amount = 250 },
                new Order { Id = 102, CustomerId = 2, Amount = 100 },
                new Order { Id = 103, CustomerId = 3, Amount = 300 },
            };

            Func<Customer, int> outerKeySelector = null;

            IEnumerable<(string CustomerName, int OrderId, int Amount)> values = customers.Join(
                orders,
                customer => customer.Id,
                order => order.CustomerId,
                (customer, order) => (customer.Name, order.Id, order.Amount));

            List<(string CustomerName, int OrderId, int Amount)> result = [];
            result.AddRange(values);
            Assert.Empty(result);
        }

        [Fact]
        public void JoinMethodShouldReturnEmptyCollectionWhenInnerIsEmpty()
        {
            IEnumerable<Customer> customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Alice" },
                new Customer { Id = 2, Name = "Bob" },
                new Customer { Id = 3, Name = "Charlie" },
            }; ;

            IEnumerable<Order> orders = [];

            Func<Customer, int> outerKeySelector = null;

            IEnumerable<(string CustomerName, int OrderId, int Amount)> values = customers.Join(
                orders,
                customer => customer.Id,
                order => order.CustomerId,
                (customer, order) => (customer.Name, order.Id, order.Amount));

            List<(string CustomerName, int OrderId, int Amount)> result = [];
            result.AddRange(values);
            Assert.Empty(result);
        }

        [Fact]
        public void JoinMethodShouldHaveKeySelectorHandleTheNullElement()
        {
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Alice" },
                new Customer { Id = 2, Name = "Bob" },
                new Customer { Id = 3, Name = "Charlie" },
            };

            var orders = new List<Order>
            {
                null,
                new Order { Id = 102, CustomerId = 2, Amount = 100 },
                new Order { Id = 103, CustomerId = 3, Amount = 300 },
            };

            IEnumerable<(string CustomerName, int OrderId, int Amount)> result = customers.Join(
                orders,
                customer => customer.Id,
                order => order?.CustomerId ?? 0,
                (customer, order) => (customer.Name, order.Id, order.Amount));

            IEnumerable<(string CustomerName, int OrderId, int intAmount)> expected =
                [
                    ("Bob", 102, 100),
                    ("Charlie", 103, 300),
                ];

            foreach (var item in result)
            {
                Assert.Contains(item, expected);
            }
        }

        class Customer
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        class Order
        {
            public int Id { get; set; }

            public int CustomerId { get; set; }

            public int Amount { get; set; }
        }

        [Fact]
        public void DistinctMethodShouldReturnCollectionOfDistinctElements()
        {
            int[] source = { 1, 2, 3, 2, 5, 5, 1, 7, 9 };

            int[] expected = { 1, 2, 3, 5, 7, 9 };

            IEnumerable<int> result = source.Distinct(EqualityComparer<int>.Default);

            int count = 0;

            foreach (var item in result)
            {
                Assert.Contains(item, expected);
                count++;
            }

            Assert.Equal(6, count);
        }

        [Fact]
        public void DistinctMethodShouldThrowArgumentNullExceptionWhenSourceIsNull()
        {
            int[] source = null;

            IEnumerable<int> result = source.Distinct(EqualityComparer<int>.Default);

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var item in result) {}
            });
        }

        [Fact]
        public void DistinctMethodShouldReturnEmptyCollectionWhenSourceisEmpty()
        {
            int[] source = [];

            List<int> result = [];

            IEnumerable<int> values = source.Distinct(EqualityComparer<int>.Default);

            result.AddRange(values);

            Assert.Empty(result);
        }

        [Fact]
        public void DistinctMethodShouldIncludeNullElementOnce()
        {
            string[] source = [null, "Ana", "Maria", null, "Elena", "Ana", "Maria", null, "Daniela"];

            string[] expected = [null, "Ana", "Maria", "Elena", "Daniela"];

            IEnumerable<string> result = source.Distinct(EqualityComparer<string>.Default);

            int count = 0;

            foreach (var item in result)
            {
                Assert.Contains(item, expected);
                count++;
            }

            Assert.Equal(5, count);
        }

        [Fact]
        public void UnionMethodShouldReturnCollectionOfUnifiedDistinctElementsFromOther2Collections()
        {
            int[] first = { 1, 2, 3, 2, 5, 5, 1, 11, 9, 11 };

            int[] second = { 1, 2, 3, 5, 9, 7, 9, 3, 10, 10 };

            IEnumerable<int> expected = [1, 2, 3, 5, 7, 9, 10, 11];

            IEnumerable<int> result = first.Union(second, EqualityComparer<int>.Default);

            int count = 0;
            foreach (var item in result)
            {
                Assert.Contains(item, expected);
                count++;
            }

            Assert.Equal(8, count);
        }

        [Fact]
        public void UnionMethodShouldThrowArgumentNullExceptionIfFirstIsNull()
        {
            int[] first = null;

            int[] second = { 1, 2, 3, 5, 9, 7, 9, 3, 10, 10 };

            Assert.Throws<ArgumentNullException>(() => first.Union(second, EqualityComparer<int>.Default));
        }

        [Fact]
        public void UnionMethodShouldThrowArgumentNullExceptionIfSecondIsNull()
        {
            int[] first = { 1, 2, 3, 2, 5, 5, 1, 11, 9, 11 };

            int[] second = null;

            Assert.Throws<ArgumentNullException>(() => first.Union(second, EqualityComparer<int>.Default));
        }

        [Fact]
        public void UnionMethodShouldReturnSecondWithDistinctElementsWhenFirstIsEmpty()
        {
            int[] first = [];

            int[] second = { 1, 2, 3, 5, 9, 7, 9, 3, 10, 10 };

            IEnumerable<int> expected = [1, 2, 3, 5, 7, 9, 10 ];

            IEnumerable<int> result = first.Union(second, EqualityComparer<int>.Default);

            int count = 0;
            foreach (var item in result)
            {
                Assert.Contains(item, expected);
                count++;
            }

            Assert.Equal(7, count);
        }

        [Fact]
        public void UnionMethodShouldncludeInResultNullElement()
        {
            string[] first = [null, "Ana", "Maria", null, "Elena", "Ana", "Maria", null, "Daniela"];

            string[] second = ["Sonia", "Maria", null, "Elena", "Maria", "Alina"];

            IEnumerable<string> expected = [null, "Ana", "Maria", "Elena", "Daniela", "Sonia", "Alina"];

            IEnumerable<string> result = first.Union(second, EqualityComparer<string>.Default);

            int count = 0;
            foreach (var item in result)
            {
                Assert.Contains(item, expected);
                count++;
            }

            Assert.Equal(7, count);
        }

        [Fact]
        public void IntersectMethodShouldReturnCollectionOfIntersectedDistinctElementsFromOther2Collections()
        {
            int[] first = { 1, 2, 3, 2, 5, 5, 1, 11, 9, 11 };

            int[] second = { 1, 2, 3, 5, 9, 7, 9, 3, 10, 10 };

            IEnumerable<int> expected = [1, 2, 3, 5, 9];

            IEnumerable<int> result = first.Intersect(second, EqualityComparer<int>.Default);

            int count = 0;
            foreach (var item in result)
            {
                Assert.Contains(item, expected);
                count++;
            }

            Assert.Equal(5, count);
        }

        [Fact]
        public void IntersectMethodShouldThrowArgumentNullExceptionWhenFirstIsNull()
        {
            int[] first = null;

            int[] second = { 1, 2, 3, 5, 9, 7, 9, 3, 10, 10 };

            IEnumerable<int> result = first.Intersect(second, EqualityComparer<int>.Default);

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var item in result) { }
            });
        }

        [Fact]
        public void IntersectMethodShouldThrowArgumentNullExceptionWhenSecondIsNull()
        {
            int[] first = { 1, 2, 3, 2, 5, 5, 1, 11, 9, 11 };

            int[] second = null;

            IEnumerable<int> result = first.Intersect(second, EqualityComparer<int>.Default);

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var item in result) { }
            });
        }

        [Fact]
        public void IntersectMethodShouldReturnEmptyCollectionWhenOneOfTheSourcesIsEmpty()
        {
            int[] first = [];

            int[] second = { 1, 2, 3, 5, 9, 7, 9, 3, 10, 10 };

            List<int> result = [];

            IEnumerable<int> values = first.Intersect(second, EqualityComparer<int>.Default);

            result.AddRange(values);

            Assert.Empty(result);
        }

        [Fact]
        public void IntersectMethodShouldIncludeInResultNullElementIfExistsInBothCllections()
        {
            string[] first = { "1", "2", null, "3", "2", "5", "5", "1", "11", "9", "11" };

            string[] second = { "1", "2", "3", "5", "9", "7", "9", null, "3", "10", "10" };

            IEnumerable<string> expected = ["1", "2", null, "3", "5", "9"];

            IEnumerable<string> result = first.Intersect(second, EqualityComparer<string>.Default);

            int count = 0;
            foreach (var item in result)
            {
                Assert.Contains(item, expected);
                count++;
            }

            Assert.Equal(6, count);
        }

        [Fact]
        public void ExceptMethodShouldReturnElementsFromFirstCollectionThatDoNotExistInSecond()
        {
            int[] first = { 1, 2, 3, 2, 5, 5, 1, 11, 9, 11 };

            int[] second = { 1, 2, 3, 5, 9, 7, 9, 3, 10, 10 };

            IEnumerable<int> expected = [11];

            IEnumerable<int> result = first.Except(second, EqualityComparer<int>.Default);

            int count = 0;
            foreach (var item in result)
            {
                Assert.Contains(item, expected);
                count++;
            }

            Assert.Equal(1, count);
        }

        [Fact]
        public void ExceptMethodShouldThrowArgumentNullExceptionWhenFirstIsNull()
        {
            int[] first = null;

            int[] second = { 1, 2, 3, 5, 9, 7, 9, 3, 10, 10 };

            IEnumerable<int> result = first.Except(second, EqualityComparer<int>.Default);

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var item in result) { }
            });
        }

        [Fact]
        public void ExceptMethodShouldThrowArgumentNullExceptionWhenSecondIsNull()
        {
            int[] first = { 1, 2, 3, 2, 5, 5, 1, 11, 9, 11 };

            int[] second = null;

            IEnumerable<int> expected = [11];

            IEnumerable<int> result = first.Except(second, EqualityComparer<int>.Default);

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var item in result) { }
            });
        }

        [Fact]
        public void ExceptMethodShouldReturnEmptyCollectionWhenFirstIsEmpty()
        {
            int[] first = [];

            int[] second = { 1, 2, 3, 5, 9, 7, 9, 3, 10, 10 };

            List<int> result = [];

            IEnumerable<int> values = first.Except(second, EqualityComparer<int>.Default);

            result.AddRange(values);

            Assert.Empty(result);
        }

        [Fact]
        public void ExceptMethodShouldReturnFirstCollectionWhenSecondIsEmpty()
        {
            int[] first = { 1, 2, 3, 2, 5, 5, 1, 11, 9, 11 };

            int[] second = [];

            IEnumerable<int> expected = [1, 2, 3, 5, 11, 9];

            IEnumerable<int> result = first.Except(second, EqualityComparer<int>.Default);

            int count = 0;
            foreach (var item in result)
            {
                Assert.Contains(item, expected);
                count++;
            }

            Assert.Equal(6, count);
        }

        [Fact]
        public void ExceptMethodShouldIncludeNullElementInResultWhenExistsInFirstButNotSecond()
        {
            string[] first = { "1", "2", "3", "2", "5", null, "5", "1", "11", "9", "11" };

            string[] second = { "1", "2", "3", "5", "9", "7", "9", "3", "10", "10" };

            IEnumerable<string> expected = [null, "11"];

            IEnumerable<string> result = first.Except(second, EqualityComparer<string>.Default);

            int count = 0;
            foreach (var item in result)
            {
                Assert.Contains(item, expected);
                count++;
            }

            Assert.Equal(2, count);
        }

        [Fact]
        public void ExceptMethodShouldNotIncludeNullElementInResultWhenExistsInSecond()
        {
            string[] first = { "1", "2", "3", "2", "5", null, "5", "1", "11", "9", "11" };

            string[] second = { "1", "2", "3", "5", "9", "7", "9", "3", "10", "10", null };

            IEnumerable<string> expected = ["11"];

            IEnumerable<string> result = first.Except(second, EqualityComparer<string>.Default);

            int count = 0;
            foreach (var item in result)
            {
                Assert.Contains(item, expected);
                count++;
            }

            Assert.Equal(1, count);
        }

        [Fact]
        public void GroupByMethodShouldReturnCollectionOfGroupedElementsBasedOnAKey()
        {
            var students = new[]
            {
                new { Name = "Alice", Grade = "A" },
                new { Name = "Bob", Grade = "B" },
                new { Name = "Charlie", Grade = "A" },
                new { Name = "David", Grade = "B" },
                new { Name = "Eve", Grade = "A" },
            };

            List<Ranking> expected = [new Ranking("A", new List<string> { "Alice", "Charlie", "Eve" }),
                new Ranking("B", new List<string> { "Bob", "David" })];

            var groupedResult = students.GroupBy(
                student => student.Grade,
                student => student.Name,
                (grade, names) => new Ranking(grade, names.ToList()),
                EqualityComparer<string>.Default
            );

            int count = 0;

            foreach (var group in groupedResult)
            {
                Assert.Equal(group.Grade, expected[count].Grade);
                Assert.True(group.Names.SequenceEqual(expected[count].Names));

                count++;
            }
        }

        [Fact]
        public void GroupByMethodShouldThrowArgumentNullExceptionWhenSourceIsNull()
        {
            Student[] students = null;

            var groupedResult = students.GroupBy(
                student => student.Grade,
                student => student.Name,
                (grade, names) => new Ranking(grade, names.ToList()),
                EqualityComparer<string>.Default
            );

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var group in groupedResult) { }
            });
        }

        [Fact]
        public void GroupByMethodShouldThrowArgumentNullExceptionWhenKeySelectorIsNull()
        {
            var students = new[]
            {
                new { Name = "Alice", Grade = "A" },
                new { Name = "Bob", Grade = "B" },
                new { Name = "Charlie", Grade = "A" },
                new { Name = "David", Grade = "B" },
                new { Name = "Eve", Grade = "A" },
            };

            var groupedResult = students.GroupBy(
                null,
                student => student.Name,
                (grade, names) => new Ranking(grade, names.ToList()),
                EqualityComparer<string>.Default
            );

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var group in groupedResult) { }
            });
        }

        [Fact]
        public void GroupByMethodShouldThrowArgumentNullExceptionWhenElementSelectorIsNull()
        {
            List<Student> students = new List<Student>()
            {
                new() { Name = "Alice", Grade = "A" },
                new() { Name = "Bob", Grade = "B" },
                new() { Name = "Charlie", Grade = "A" },
                new() { Name = "David", Grade = "B" },
                new() { Name = "Eve", Grade = "A" },
            };

            Func<Student, string> elementSelector = null;

            var groupedResult = students.GroupBy(
                student => student.Grade,
                elementSelector,
                (grade, names) => new Ranking(grade, names.ToList()),
                EqualityComparer<string>.Default
            );

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var group in groupedResult) { }
            });
        }

        [Fact]
        public void GroupByMethodShouldThrowArgumentNullExceptionWhenResultSelectorIsNull()
        {
            List<Student> students = new List<Student>()
            {
                new() { Name = "Alice", Grade = "A" },
                new() { Name = "Bob", Grade = "B" },
                new() { Name = "Charlie", Grade = "A" },
                new() { Name = "David", Grade = "B" },
                new() { Name = "Eve", Grade = "A" },
            };

            Func<string, IEnumerable<string>, Ranking> resultSelector = null;

            var groupedResult = students.GroupBy(
                student => student.Grade,
                student => student.Name,
                resultSelector,
                EqualityComparer<string>.Default
            );

            Assert.Throws<ArgumentNullException>(() =>
            {
                foreach (var group in groupedResult) { }
            });
        }

        [Fact]
        public void GroupByMethodShouldReturnEmptyCollectionWhenSourceIsEmpty()
        {
            Student[] students = [];

            List<Ranking> expected = [new Ranking("A", new List<string> { "Alice", "Charlie", "Eve" }),
                new Ranking("B", new List<string> { "Bob", "David" })];

            var groupedResult = students.GroupBy(
                student => student.Grade,
                student => student.Name,
                (grade, names) => new Ranking(grade, names.ToList()),
                EqualityComparer<string>.Default
            );

            List<Ranking> result = [];
            result.AddRange(groupedResult);

            Assert.Empty(result);
        }

        [Fact]
        public void GroupByMethodShouldIncludeNullElementsAsTheyAreHandledBySelectors()
        {
            List<Student> students = new List<Student>()
            {
                new() { Name = "Alice", Grade = "A" },
                new() { Name = "Bob", Grade = "B" },
                null,
                new() { Name = "Charlie", Grade = "A" },
                new() { Name = "David", Grade = "B" },
                new() { Name = "Eve", Grade = "A" },
            };

            List<Ranking> expected = [new Ranking("A", new List<string> { "Alice", "Charlie", "Eve" }),
                new Ranking("B", new List<string> { "Bob", "David" }), new Ranking("N/A", new List<string> { "default" })];

            var groupedResult = students.GroupBy(
                student => student?.Grade ?? "N/A",
                student => student?.Name ?? "default",
                (grade, names) => new Ranking(grade, names.ToList()),
                EqualityComparer<string>.Default
            );

            int count = 0;

            foreach (var group in groupedResult)
            {
                Assert.Equal(group.Grade, expected[count].Grade);
                Assert.True(group.Names.SequenceEqual(expected[count].Names));

                count++;
            }
        }

        public class Student
        {
            public string Name { get; set; }

            public string Grade { get; set; }
        }

        public class Ranking
        {
            public Ranking(string grade, List<string> names)
            {
                Grade = grade;
                Names = names;
            }

            public string Grade { get; }

            public List<string> Names { get; }
        }

        [Fact]
        public void OrderByMethodReturnsCollectionorderedByKey()
        {
            List<(string Name, int Age)> persons = new ()
            {
                ("Maria", 22),
                ("Ana", 34),
                ("John", 22),
                ("Steve", 54),
                ("Elene", 50),
            };

            List<(string Name, int Age)> expected = new ()
            {
                ("Maria", 22),
                ("John", 22),
                ("Ana", 34),
                ("Elene", 50),
                ("Steve", 54),
            };

            IOrderedEnumerable<(string Name, int Age)> result = persons
                .OrderBy(person => person.Age, Comparer<int>.Default);

            int count = 0;

            foreach (var person in result)
            {
                Assert.Equal(expected[count].Name, person.Name);
                Assert.Equal(expected[count].Age, person.Age);

                count++;
            }
        }

        [Fact]
        public void OrderByMethodThrowsArgumentNullExceptionWhenSourceIsNull()
        {
            List<(string Name, int Age)> persons = null;

            Assert.Throws<ArgumentNullException>(() => persons
            .OrderBy(person => person.Age, Comparer<int>.Default));
        }

        [Fact]
        public void OrderByMethodThrowsArgumentNullExceptionWhenKeySelectorIsNull()
        {
            List<(string Name, int Age)> persons = new()
            {
                ("Maria", 22),
                ("Ana", 34),
                ("John", 22),
                ("Steve", 54),
                ("Elene", 50),
            };

            Func<(string, int), int> keySelector = null;

            Assert.Throws<ArgumentNullException>(() => persons
                .OrderBy(keySelector, Comparer<int>.Default));
        }

        [Fact]
        public void OrderByMethodReturnsEmptyCollectionWhenSourceIsEmpty()
        {
            List<(string Name, int Age)> persons = [];

            List<(string Name, int Age)> result = new();


            IOrderedEnumerable<(string Name, int Age)> values = persons
                .OrderBy(person => person.Age, Comparer<int>.Default);

            result.AddRange(values);

            Assert.Empty(result);
        }

        [Fact]
        public void OrderByMethodIncludesNullElementsWhenTheyAreHandledByKeySelectorAndComparer()
        {
            List<(string Name, int Age)?> persons = new()
            {
                ("Maria", 22),
                ("Ana", 34),
                null,
                ("John", 22),
                ("Steve", 54),
                ("Elene", 50),
            };

            List<(string Name, int Age)?> expected = new()
            {
                null,
                ("Maria", 22),
                ("John", 22),
                ("Ana", 34),
                ("Elene", 50),
                ("Steve", 54),
            };

            IOrderedEnumerable<(string Name, int Age)?> result = persons
                .OrderBy(person => person?.Age ?? 0, Comparer<int>.Default);

            int count = 0;

            foreach (var person in result)
            {
                Assert.Equal(expected[count]?.Name, person?.Name);
                Assert.Equal(expected[count]?.Age, person?.Age);

                count++;
            }
        }

        [Fact]
        public void ThenByMethodReturnsCollectionOrderedWithOrderByAndApplyAnotherLayerOfOrdering()
        {
            List<(string Name, int Age)> persons = new ()
            {
                ("Ana", 34),
                ("Zoe", 50),
                ("Steve", 54),
                ("Maria", 22),
                ("Elene", 50),
                ("John", 22),
                ("Alice", 22),
            };

            List<(string Name, int Age)> expected = new ()
            {
                ("Alice", 22),
                ("John", 22),
                ("Maria", 22),
                ("Ana", 34),
                ("Elene", 50),
                ("Zoe", 50),
                ("Steve", 54),
            };

            IOrderedEnumerable<(string Name, int Age)> result = persons
                .OrderBy(person => person.Age, Comparer<int>.Default)
                .ThenBy(person => person.Name, Comparer<string>.Default);

            int count = 0;

            foreach (var person in result)
            {
                Assert.Equal(expected[count].Name, person.Name);
                Assert.Equal(expected[count].Age, person.Age);

                count++;
            }
        }

        [Fact]
        public void ThenByMethodThrowsArgumentNullExceptionWhenSourceisNull()
        {
            IOrderedEnumerable<(string Name, int Age)> persons = null;

            Assert.Throws<ArgumentNullException>(() => persons.
                ThenBy(person => person.Name, Comparer<string>.Default));
        }

        [Fact]
        public void ThenByMethodThrowsArgumentNullExceptionWhenKeySelectorIsNull()
        {
            List<(string Name, int Age)> persons = new()
            {
                ("Ana", 34),
                ("Zoe", 50),
                ("Steve", 54),
                ("Maria", 22),
                ("Elene", 50),
                ("John", 22),
                ("Alice", 22),
            };

            Func<(string Name, int Age), string> keySelector = null;

            Assert.Throws<ArgumentNullException>(() => persons
                .OrderBy(person => person.Age, Comparer<int>.Default)
                .ThenBy(keySelector, Comparer<string>.Default));
        }

        [Fact]
        public void ThenByMethodReturnsCollectionEmptyWhenSourceIsEmpty()
        {
            List<(string Name, int Age)> persons = [];

            List<(string Name, int Age)> result = [];

            IOrderedEnumerable<(string Name, int Age)> values = persons
                .OrderBy(person => person.Age, Comparer<int>.Default)
                .ThenBy(person => person.Name, Comparer<string>.Default);

            result.AddRange(values);

            Assert.Empty(result);
        }
    }
}