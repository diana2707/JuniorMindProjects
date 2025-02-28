
namespace Extension_methods.Extension_methods
{
    public static class Exercises
    {
        public static (int vowels, int consonants) CountVowelsAndConsonants(this string source)
        {
            ArgumentNullException.ThrowIfNull(source);

            const string vowels = "aeiouAEIOU";

            return source.Aggregate((vowels: 0, consonants: 0), (acc, c) =>
            {
                if (!char.IsLetter(c))
                {
                    return acc;
                }

                return vowels.Contains(c)
                    ? (acc.vowels + 1, acc.consonants)
                    : (acc.vowels, acc.consonants + 1);
            });
        }

        public static char FirstUnique(this string source)
        {
            ArgumentNullException.ThrowIfNull(source);

            char firstUnique = source.GroupBy(e => e)
                                     .Where(group => group.Count() == 1)
                                     .Select(group => group.Key)
                                     .FirstOrDefault();

            return firstUnique != '\0'
                    ? firstUnique
                    : throw new ArgumentException("String has no unique characters.");
        }

        public static int ConvertToInt(this string source)
        {
            ArgumentException.ThrowIfNullOrEmpty(source);

            int result = 0;
            int sign = source[0] == '-' ? -1 : 1;

            var number = (source[0] == '-' || source[0] == '+') ? source.Skip(1) : source;

            int placeValue = number.Count();

            result = number.Aggregate(0, (result, e) => char.IsDigit(e)
                                ? (result * 10) + (e - '0')
                                : throw new ArgumentException("The string is not integer"));

            return result * sign;
        }

        public static char MostFrequentChar(this string source)
        {
            ArgumentException.ThrowIfNullOrEmpty(source);

            return source.GroupBy(character => character)
                .MaxBy(charGroup => charGroup.Count())
                .Key;
        }

        public static IEnumerable<string> GetAllPalindroms(this string source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return Enumerable.Range(0, source.Length)
                .SelectMany(i => Enumerable.Range(1, source.Length - i)
                     .Select(j => source[i..(i + j)]))
                .Where(segment => segment.SequenceEqual(segment.Reverse()));
        }

        public static IEnumerable<IEnumerable<int>> GetSubarrays(this IEnumerable<int> source, int maxSum)
        {
            ArgumentNullException.ThrowIfNull(source);

            return Enumerable.Range(0, source.Count())
                .SelectMany(i => Enumerable.Range(1, source.Count() - i)
                     .Select(j => source.Skip(i).Take(j)))
                .Where(subarray => subarray.Sum() <= maxSum)
                .Select(subarray => subarray);
        }

        public static IEnumerable<string> GetSignCombinations(int count, int totalValue)
        {
            if (count == 0)
            {
                return [];
            }

            return Enumerable.Range(1, count)
                    .Select(n => new[] { (n, "+" + n.ToString()), (n * (-1), "-" + n.ToString()) })
                    .Aggregate((accCombinations, nextNewElements) => accCombinations
                            .SelectMany(
                                combination => nextNewElements,
                                (combination, newElement) => (combination.Item1 + newElement.Item1,
                                                                combination.Item2 + newElement.Item2))
                            .ToArray())
                    .Where(element => element.Item1 == totalValue)
                    .Select(element => element.Item2);
        }

        public static IEnumerable<IEnumerable<int>> GetPythagoreanTriplets(List<int> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            IEnumerable<int> orderedSource = source.Order();
            int count = orderedSource.Count();

            if (count < 3)
            {
                return [];
            }

            return Enumerable.Range(0, count - 2)
                .SelectMany(i => Enumerable.Range(i + 1, count - i - 1)
                    .SelectMany(j => Enumerable.Range(j + 1, count - j - 1)
                        .Select(k => new List<int> { source[i], source[j], source[k] }.ToArray())))
                .Where(triplet => (int)Math.Pow(triplet[0], 2)
                                + (int)Math.Pow(triplet[1], 2)
                                == (int)Math.Pow(triplet[2], 2));
        }

        public static IEnumerable<Product> FilterByAnyFeatures(IEnumerable<Product> products, IEnumerable<Feature> features)
        {
            ArgumentNullException.ThrowIfNull(products);
            ArgumentNullException.ThrowIfNull(features);

            return products.Where(product => product.Features
                                .Any(prodFeature => features.Select(feature => feature.Id)
                                        .Any(id => prodFeature.Id == id)));
        }

        public static IEnumerable<Product> FilterByAllFeatures(IEnumerable<Product> products, IEnumerable<Feature> features)
        {
            ArgumentNullException.ThrowIfNull(products);
            ArgumentNullException.ThrowIfNull(features);

            return products.Where(product => features
                                    .All(feature => product.Features
                                        .Select(prodFeature => prodFeature.Id)
                                             .Any(id => id == feature.Id)));
        }

        public static IEnumerable<Product> FilterByExcludedFeatures(IEnumerable<Product> products, IEnumerable<Feature> features)
        {
            ArgumentNullException.ThrowIfNull(products);
            ArgumentNullException.ThrowIfNull(features);

            return products.Where(product => product.Features
                                     .All(prodFeature => features
                                           .All(feature => feature.Id != prodFeature.Id)));
        }

        public static IEnumerable<Product> SumProductQuantity(IEnumerable<Product> first, IEnumerable<Product> second)
        {
            ArgumentNullException.ThrowIfNull(first);
            ArgumentNullException.ThrowIfNull(second);

            return first.Concat(second)
                .GroupBy(product => product.Name)
                .Select(group => new Product()
                {
                    Name = group.Key,
                    Quantity = group.Sum(product => product.Quantity),
                });
        }

        public static IEnumerable<TestResults> GetMaxResultsByFamily(IEnumerable<TestResults> results)
        {
            ArgumentNullException.ThrowIfNull(results);

            return results.GroupBy(result => result.FamilyId)
                          .Select(group => group.MaxBy(result => result.Score));
        }

        public static IEnumerable<string> MostFrequentWords(string input)
        {
            ArgumentNullException.ThrowIfNull(input);

            return input.Split([' ', '.', ',', '?', '!', ';'], StringSplitOptions.RemoveEmptyEntries)
                        .Select(word => word.ToLower())
                        .GroupBy(word => word)
                        .OrderByDescending(group => group.Count())
                        .Take(5)
                        .Select(group => group.Key);
        }

        public static bool IsValidSudoku(IEnumerable<int> input)
        {
            ArgumentNullException.ThrowIfNull(input);

            if (input.Count() != 81 || input.Any(e => e < 1 || e > 9))
            {
                throw new ArgumentException("The grid contains invalid numbers.");
            }

            var rows = input.Select((value, index) => new { value, index })
                            .GroupBy(item => item.index / 9);

            var columns = input.Select((value, index) => new { value, index })
                               .GroupBy(item => item.index % 9);

            var subgrids = input.Select((value, index) => new { value, index })
                                .GroupBy(item => (item.index / 9 / 3 * 3) + (item.index % 9 / 3));

            return rows.Concat(columns)
                       .Concat(subgrids)
                       .All(group => group
                            .Select(item => item.value)
                            .Distinct()
                            .Count() == 9);
        }

        public static int CalculateRPN(string input)
        {
            ArgumentException.ThrowIfNullOrEmpty(input);

            string[] expression = input.Split(' ');

            Stack<int> operands = new Stack<int>();

            Dictionary<string, Func<int, int, int>> operations = new()
            {
                { "+", (a, b) => a + b },
                { "-", (a, b) => a - b },
                { "*", (a, b) => a * b },
                { "/", (a, b) => a / b },
            };

            return expression.Aggregate(operands, (operands, element) =>
            {
                if (int.TryParse(element, out int value))
                {
                    operands.Push(value);
                }
                else if (operations.TryGetValue(element, out Func<int, int, int> operation))
                {
                    if (operands.Count() < 2)
                    {
                        throw new ArgumentException("Expression is invalid.");
                    }

                    int b = operands.Pop();
                    int a = operands.Pop();
                    operands.Push(operation(a, b));
                }
                else
                {
                    throw new ArgumentException("The argument contains invalid characters.");
                }

                return operands;
            }).Pop();
        }
    }
}
