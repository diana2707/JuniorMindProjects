using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Extension_methods
{
    public static class Exercises
    {
        public static int CountVowels(this string source)
        {
            ArgumentNullException.ThrowIfNull(source);

            const string vowels = "aeiouAEIOU";

            return source.Count(e => vowels.Contains(e));
        }

        public static int CountConsonants(this string source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.Count(e => char.IsLetter(e)) - source.CountVowels();
        }

        public static char FirstUnique(this string source)
        {
            ArgumentNullException.ThrowIfNull(source);

            IEnumerable<char> unique = source.Where(e => source.Count(x => x == e) == 1);

            if (!unique.Any())
            {
                throw new ArgumentException("String has no unique characters.");
            }

            return unique.First();
        }

        public static int ConvertToInt(this string source)
        {
            ArgumentException.ThrowIfNullOrEmpty(source);

            int result = 0;
            int sign = source[0] == '-' ? -1 : 1;

            var number = source.SkipWhile((e, i) => i == 0 && (e == '-' || e == '+'));

            if (number.Any(e => !char.IsDigit(e)))
            {
                throw new ArgumentException("The string is not integer");
            }

            int placeValue = number.Count();

            result = number.Aggregate(0, (result, e) => result + ((e - '0') * (int)Math.Pow(10, --placeValue)));

            return result * sign;
        }

        public static char MostFrequentChar(this string source)
        {
            ArgumentException.ThrowIfNullOrEmpty(source);

            return source.GroupBy(character => character)
                .MaxBy(charGroup => charGroup.Count())
                .Key;
        }

        public static List<IEnumerable<char>> GetAllPalindroms(this string source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return Enumerable.Range(0, source.Length)
                .SelectMany(i => Enumerable.Range(1, source.Length - i)
                     .Select(j => source.Skip(i).Take(j)))
                .Where(segment => segment.SequenceEqual(segment.Reverse()))
                .ToList();
        }

        public static List<List<int>> GetSubarrays(this List<int> source, int maxSum)
        {
            ArgumentNullException.ThrowIfNull(source);

            return Enumerable.Range(0, source.Count)
                .SelectMany(i => Enumerable.Range(1, source.Count - i)
                     .Select(j => source.Skip(i).Take(j)))
                .Where(subarray => subarray.Aggregate(0, (sum, number) => sum + number) <= maxSum)
                .Select(subarray => subarray.ToList())
                .ToList();
        }

        public static List<string> GetSignCombinations(int count)
        {
            if (count == 0)
            {
                return [];
            }

            return Enumerable.Range(0, (int)Math.Pow(2, count))
                .Select(intNumber => Convert.ToString(intNumber, 2).PadLeft(count))
                .Select(binaryNumber => Enumerable.Range(1, count)
                       .Zip(binaryNumber, (number, bit) => bit == '0' ? $"-{number}" : $"+{number}")
                           .Aggregate(string.Empty, (combination, signedNumber) => combination += signedNumber))
                .ToList();
        }

        public static List<List<int>> GetPythagoreanTriplets(List<int> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            if (!source.Any())
            {
                return [];
            }

            return Enumerable.Range(0, source.Count - 2)
                .SelectMany(i => Enumerable.Range(i + 1, source.Count - i - 1)
                    .SelectMany(j => Enumerable.Range(j + 1, source.Count - j - 1)
                        .Select(k => new List<int> { source[i], source[j], source[k] }.Order())))
                .Where(triplet => (int)Math.Pow(triplet.ElementAt(0), 2)
                                + (int)Math.Pow(triplet.ElementAt(1), 2)
                                == (int)Math.Pow(triplet.ElementAt(2), 2))
                .Select(e => e.ToList())
                .ToList();
        }

        public static List<Product> FilterByAnyFeatures(List<Product> products, List<Feature> features)
        {
            return products.Where(product => product.Features
                                .Any(prodFeature => features.Select(feature => feature.Id)
                                        .Any(id => prodFeature.Id == id)))
                           .ToList();
        }

        public static List<Product> FilterByAllFeatures(List<Product> products, List<Feature> features)
        {
            return products.Where(product => features
                                    .All(feature => product.Features
                                        .Select(prodFeature => prodFeature.Id)
                                             .Any(id => id == feature.Id)))
                                  .ToList();
        }

        public static List<Product> FilterByExcludedFeatures(List<Product> products, List<Feature> features)
        {
            return products.Where(product => features
                                .All(feature => product.Features
                                    .Select(prodFeature => prodFeature.Id)
                                        .Any(id => id != feature.Id)))
            .ToList();
        }

        public static List<Product> SumProductQuantity(IEnumerable<Product> first, IEnumerable<Product> second)
        {
            return first.Concat(second)
                .GroupBy(product => product.Name)
                .Select(group => new Product()
                {
                    Name = group.Key,
                    Quantity = group.Sum(product => product.Quantity),
                })
                .ToList();
        }

        public static List<TestResults> GetMaxResultsByFamily(IEnumerable<TestResults> results)
        {
            return results.GroupBy(result => result.FamilyId)
                          .Select(group => group.MaxBy(result => result.Score))
                          .ToList();
        }
    }
}
