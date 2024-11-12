using System.Linq;

namespace ObjectsCollection
{
    public class RadixTreeFacts
    {
        [Fact]
        public void RadixTreeClassExists()
        {
            var tree = new RadixTree<string>();
            Assert.IsType<RadixTree<string>>(tree);
        }

        [Fact]
        public void CountShouldReturn0WhenTreeIsEmpty()
        {
            var tree = new RadixTree<string>();
            Assert.Equal(0, tree.Count);
        }

        [Fact]
        public void IsReadOnlyShouldReturnFalse()
        {
            var tree = new RadixTree<string>();
            Assert.False(tree.IsReadOnly);
        }

        [Fact]
        public void AddShouldAddFirstElementWhenTreeIsEmpty()
        {
            var tree = new RadixTree<string>();
            tree.Add("cat");
            Assert.Equal(1, tree.Count);
        }

        [Fact]
        public void AddShouldAddElementWhenPrefixIsCommon()
        {
            var tree = new RadixTree<string>();
            tree.Add("cat");
            tree.Add("car");
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void AddShouldAddMultipleElementsWhenPrefixIscommon()
        {
            var tree = new RadixTree<string>();
            tree.Add("cat");
            tree.Add("car");
            tree.Add("cabage");
            tree.Add("candle");
            Assert.True(tree.Contains("cat"));
            Assert.True(tree.Contains("car"));
            Assert.True(tree.Contains("cabage"));
            Assert.True(tree.Contains("candle"));

            Assert.Equal(4, tree.Count);
        }

        [Fact]
        public void AddShouldAddElementWhenNewStringHasAnotherWordAsPrefix()
        {
            var tree = new RadixTree<string>();
            tree.Add("cat");
            tree.Add("catnip");
            Assert.True(tree.Contains("cat"));
            Assert.True(tree.Contains("catnip"));
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void AddShouldAddElementWhenNewStringIsPartOfAWordFromTree()
        {
            var tree = new RadixTree<string>();
            tree.Add("catnip");
            tree.Add("cat");
            Assert.True(tree.Contains("cat"));
            Assert.True(tree.Contains("catnip"));
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void AddShouldAddElementWhenNewStringIsPrefixFor2WordsInTree()
        {
            var tree = new RadixTree<string>();
            tree.Add("catnip");
            tree.Add("category");
            tree.Add("cat");
            Assert.True(tree.Contains("cat"));
            Assert.True(tree.Contains("catnip"));
            Assert.True(tree.Contains("category"));
            Assert.Equal(3, tree.Count);
        }

        [Fact]
        public void AddShouldNotAddElementWhen2StringsHaveWordAsCommonPrefix()
        {
            var tree = new RadixTree<string>();
            tree.Add("catnip");
            tree.Add("category");

            Assert.False(tree.Contains("cat"));
            Assert.True(tree.Contains("catnip"));
            Assert.True(tree.Contains("category"));
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void AddShouldAddElementsWithDifferntLengthsAndCommonPrefixes()
        {
            var tree = new RadixTree<string>();
            tree.Add("romane");
            tree.Add("romanus");
            tree.Add("rom");
            tree.Add("romanum");
            tree.Add("romulus");
            tree.Add("rub");
            tree.Add("rubens");
            tree.Add("ben");
            tree.Add("ruber");
            tree.Add("rubicundus");

            Assert.True(tree.Contains("romane"));
            Assert.True(tree.Contains("romanus"));
            Assert.True(tree.Contains("rom"));
            Assert.True(tree.Contains("romulus"));
            Assert.True(tree.Contains("romanum"));
            Assert.True(tree.Contains("rub"));
            Assert.True(tree.Contains("rubens"));
            Assert.True(tree.Contains("ben"));
            Assert.True(tree.Contains("ruber"));
            Assert.True(tree.Contains("rubicundus"));
            Assert.Equal(10, tree.Count);
        }

        [Fact]
        public void AddShouldAddListsOfCharsElements()
        {
            var tree = new RadixTree<List<char>>();
            var romane = new List<char>() { 'r', 'o', 'm', 'a', 'n', 'e' };
            var romanus = new List<char>() { 'r', 'o', 'm', 'a', 'n', 'u', 's' };
            var rom = new List<char>() { 'r', 'o', 'm' };
            var romulus = new List<char>() { 'r', 'o', 'm', 'u', 'l', 'u', 's' };
            var rub = new List<char>() { 'r', 'u', 'b' };
            var rubens = new List<char>() { 'r', 'u', 'b', 'e', 'n', 's' };
            var ben = new List<char>() { 'b', 'e', 'n' };
            var ruber = new List<char>() { 'r', 'u', 'b', 'e', 'r' };
            var rubicundus = new List<char>() { 'r', 'u', 'b', 'i', 'c', 'u', 'n', 'd', 'u', 's' };

            tree.Add(romane);
            tree.Add(romanus);
            tree.Add(rom);
            tree.Add(romulus);
            tree.Add(rub);
            tree.Add(rubens);
            tree.Add(ben);
            tree.Add(ruber);
            tree.Add(rubicundus);

            Assert.True(tree.Contains(romane));
            Assert.True(tree.Contains(romanus));
            Assert.True(tree.Contains(rom));
            Assert.True(tree.Contains(romulus));
            Assert.True(tree.Contains(rub));
            Assert.True(tree.Contains(rubens));
            Assert.True(tree.Contains(ben));
            Assert.True(tree.Contains(ruber));
            Assert.True(tree.Contains(rubicundus));
            Assert.Equal(9, tree.Count);
        }

        [Fact]
        public void AddShouldAddArrayOfCharsElements()
        {
            var tree = new RadixTree<char[]>();
            char[] romane = ['r', 'o', 'm', 'a', 'n', 'e'];
            char[] romanus = ['r', 'o', 'm', 'a', 'n', 'u', 's'];
            char[] rom = ['r', 'o', 'm'];
            char[] romulus = ['r', 'o', 'm', 'u', 'l', 'u', 's'];
            char[] rub = ['r', 'u', 'b'];
            char[] rubens = ['r', 'u', 'b', 'e', 'n', 's'];
            char[] ben = ['b', 'e', 'n'];
            char[] ruber = ['r', 'u', 'b', 'e', 'r'];
            char[] rubicundus = ['r', 'u', 'b', 'i', 'c', 'u', 'n', 'd', 'u', 's'];

            tree.Add(romane);
            tree.Add(romanus);
            tree.Add(rom);
            tree.Add(romulus);
            tree.Add(rub);
            tree.Add(rubens);
            tree.Add(ben);
            tree.Add(ruber);
            tree.Add(rubicundus);

            Assert.True(tree.Contains(romane));
            Assert.True(tree.Contains(romanus));
            Assert.True(tree.Contains(rom));
            Assert.True(tree.Contains(romulus));
            Assert.True(tree.Contains(rub));
            Assert.True(tree.Contains(rubens));
            Assert.True(tree.Contains(ben));
            Assert.True(tree.Contains(ruber));
            Assert.True(tree.Contains(rubicundus));
            Assert.Equal(9, tree.Count);
        }

        [Fact]
        public void ContainsShouldReturnTrueIfStringExistsInTree()
        {
            var tree = new RadixTree<string>();
            tree.Add("cat");
            tree.Add("car");
            Assert.True(tree.Contains("car"));
            Assert.True(tree.Contains("cat"));
        }

        [Fact]
        public void ContainsShouldReturnFalseIfStringDoNotExistsInTree()
        {
            var tree = new RadixTree<string>();
            tree.Add("cat");
            tree.Add("car");
            Assert.False(tree.Contains("camel"));
            Assert.False(tree.Contains("ca"));
            Assert.False(tree.Contains("container"));
            Assert.False(tree.Contains("book"));
        }

        [Fact]
        public void ContainsShouldReturnTrueWhenNewStringHasAnotherWordAsPrefix()
        {
            var tree = new RadixTree<string>();
            tree.Add("cab");
            tree.Add("cabage");
            Assert.True(tree.Contains("cab"));
            Assert.True(tree.Contains("cabage"));
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void ContainsShouldReturnTrueWhenEndOfStringIsTheSameAsPrefix()
        {
            var tree = new RadixTree<string>() { "Ana", "Anda", "Alina", "Ana-Maria", "Maria", "Mara" };

            Assert.True(tree.Contains("Ana"));
            Assert.True(tree.Contains("Ana-Maria"));
            Assert.Equal(6, tree.Count);
        }

        [Fact]
        public void ContainsShouldReturnTrueWhenElementsAreListOfChars()
        {
            var ana = new List<char>() { 'A', 'n', 'a' };
            var anda = new List<char>() { 'A', 'n', 'd', 'a' };
            var alina = new List<char>() { 'A', 'l', 'i', 'n', 'a' };
            var anaMaria = new List<char>() { 'A', 'n', 'a', '-', 'M', 'a', 'r', 'i', 'a' };
            var maria = new List<char>() { 'M', 'a', 'r', 'i', 'a' };
            var mara = new List<char>() { 'M', 'a', 'r', 'a' };
            var tree = new RadixTree<List<char>>() { ana, anda, alina, anaMaria, maria, mara };

            Assert.True(tree.Contains(ana));
            Assert.True(tree.Contains(anaMaria));
            Assert.Equal(6, tree.Count);
        }

        [Fact]
        public void ContainsShouldReturnTrueWhenElementsAreArrayOfChars()
        {
            char[] ana = ['A', 'n', 'a'];
            char[] anda = ['A', 'n', 'd', 'a'];
            char[] alina = ['A', 'l', 'i', 'n', 'a'];
            char[] anaMaria = ['A', 'n', 'a', '-', 'M', 'a', 'r', 'i', 'a'];
            char[] maria = ['M', 'a', 'r', 'i', 'a'];
            char[] mara = ['M', 'a', 'r', 'a'];
            var tree = new RadixTree<char[]>() { ana, anda, alina, anaMaria, maria, mara };

            Assert.True(tree.Contains(ana));
            Assert.True(tree.Contains(anaMaria));
            Assert.Equal(6, tree.Count);
        }

        [Fact]
        public void RemoveShouldReturnTrueWhenRemovesWordsThatArePrefixForOtherWords()
        {
            var tree = new RadixTree<string>();
            tree.Add("cat");
            tree.Add("catnip");
            tree.Remove("cat");

            Assert.False(tree.Contains("cat"));
            Assert.True(tree.Contains("catnip"));
            Assert.Equal(1, tree.Count);
        }

        [Fact]
        public void RemoveShouldReturnTrueWhenRemovesWordsWithNoCommonSufix()
        {
            var tree = new RadixTree<string>();
            tree.Add("cat");
            tree.Add("car");

            Assert.True(tree.Remove("car"));
            Assert.False(tree.Contains("car"));
            Assert.True(tree.Contains("cat"));
            Assert.Equal(1, tree.Count);
        }

        [Fact]
        public void RemoveShouldReturnFalseWhenTriesToRemoveWordsThatAreNotInTree()
        {
            var tree = new RadixTree<string>();
            tree.Add("cat");
            tree.Add("car");

            Assert.False(tree.Remove("cab"));
            Assert.False(tree.Remove("bag"));
            Assert.True(tree.Contains("car"));
            Assert.True(tree.Contains("cat"));
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void RemoveShouldReturnTrueAndRemoveStringsWhenTreeHasMultipleStrings()
        {
            var tree = new RadixTree<string>();
            tree.Add("romane");
            tree.Add("romanus");
            tree.Add("rom");
            tree.Add("romulus");
            tree.Add("rub");
            tree.Add("rubens");
            tree.Add("ben");
            tree.Add("ruber");
            tree.Add("rubicundus");

            Assert.True(tree.Remove("rom"));
            Assert.True(tree.Remove("rubicundus"));
            Assert.True(tree.Remove("ben"));
            Assert.True(tree.Remove("romanus"));

            Assert.True(tree.Contains("romane"));
            Assert.True(tree.Contains("romulus"));
            Assert.True(tree.Contains("rub"));
            Assert.True(tree.Contains("rubens"));
            Assert.True(tree.Contains("ruber"));

            Assert.False(tree.Contains("rom"));
            Assert.False(tree.Contains("rubicundus"));
            Assert.False(tree.Contains("ben"));
            Assert.False(tree.Contains("romanus"));

            Assert.False(tree.Contains("roman"));

            Assert.Equal(5, tree.Count);
        }

        [Fact]
        public void RemoveShouldRemoveListsOfCharsElements()
        {
            var tree = new RadixTree<List<char>>();
            var romane = new List<char>() { 'r', 'o', 'm', 'a', 'n', 'e' };
            var romanus = new List<char>() { 'r', 'o', 'm', 'a', 'n', 'u', 's' };
            var rom = new List<char>() { 'r', 'o', 'm' };
            var romulus = new List<char>() { 'r', 'o', 'm', 'u', 'l', 'u', 's' };
            var rub = new List<char>() { 'r', 'u', 'b' };
            var rubens = new List<char>() { 'r', 'u', 'b', 'e', 'n', 's' };
            var ben = new List<char>() { 'b', 'e', 'n' };
            var ruber = new List<char>() { 'r', 'u', 'b', 'e', 'r' };
            var rubicundus = new List<char>() { 'r', 'u', 'b', 'i', 'c', 'u', 'n', 'd', 'u', 's' };
            var roman = new List<char>() { 'r', 'o', 'm', 'a', 'n' };

            tree.Add(romane);
            tree.Add(romanus);
            tree.Add(rom);
            tree.Add(romulus);
            tree.Add(rub);
            tree.Add(rubens);
            tree.Add(ben);
            tree.Add(ruber);
            tree.Add(rubicundus);

            Assert.True(tree.Remove(rom));
            Assert.True(tree.Remove(rubicundus));
            Assert.True(tree.Remove(ben));
            Assert.True(tree.Remove(romanus));

            Assert.True(tree.Contains(romane));
            Assert.True(tree.Contains(romulus));
            Assert.True(tree.Contains(rub));
            Assert.True(tree.Contains(rubens));
            Assert.True(tree.Contains(ruber));

            Assert.False(tree.Contains(rom));
            Assert.False(tree.Contains(rubicundus));
            Assert.False(tree.Contains(ben));
            Assert.False(tree.Contains(romanus));

            Assert.False(tree.Contains(roman));

            Assert.Equal(5, tree.Count);
        }

        [Fact]
        public void RemoveShoulRemoveArrayOfCharsElements()
        {
            var tree = new RadixTree<char[]>();
            char[] romane = ['r', 'o', 'm', 'a', 'n', 'e'];
            char[] romanus = ['r', 'o', 'm', 'a', 'n', 'u', 's'];
            char[] rom = ['r', 'o', 'm'];
            char[] romulus = ['r', 'o', 'm', 'u', 'l', 'u', 's'];
            char[] rub = ['r', 'u', 'b'];
            char[] rubens = ['r', 'u', 'b', 'e', 'n', 's'];
            char[] ben = ['b', 'e', 'n'];
            char[] ruber = ['r', 'u', 'b', 'e', 'r'];
            char[] rubicundus = ['r', 'u', 'b', 'i', 'c', 'u', 'n', 'd', 'u', 's'];
            char[] roman = ['r', 'o', 'm', 'a', 'n'];

            tree.Add(romane);
            tree.Add(romanus);
            tree.Add(rom);
            tree.Add(romulus);
            tree.Add(rub);
            tree.Add(rubens);
            tree.Add(ben);
            tree.Add(ruber);
            tree.Add(rubicundus);

            Assert.True(tree.Remove(rom));
            Assert.True(tree.Remove(rubicundus));
            Assert.True(tree.Remove(ben));
            Assert.True(tree.Remove(romanus));

            Assert.True(tree.Contains(romane));
            Assert.True(tree.Contains(romulus));
            Assert.True(tree.Contains(rub));
            Assert.True(tree.Contains(rubens));
            Assert.True(tree.Contains(ruber));

            Assert.False(tree.Contains(rom));
            Assert.False(tree.Contains(rubicundus));
            Assert.False(tree.Contains(ben));
            Assert.False(tree.Contains(romanus));

            Assert.False(tree.Contains(roman));

            Assert.Equal(5, tree.Count);
        }

        [Fact]
        public void GetEnumeratorShouldEnumerateThroughAllStrings()
        {
            var tree = new RadixTree<string>();
            tree.Add("romane");
            tree.Add("romanus");
            tree.Add("rom");
            tree.Add("romulus");
            tree.Add("rub");
            tree.Add("rubens");
            tree.Add("ben");
            tree.Add("ruber");
            tree.Add("rubicundus");

            string[] array = { "romane", "romanus", "rom", "romulus", "rub", "rubens", "ben", "ruber", "rubicundus" };
            int count = 0;

            foreach (var item in tree)
            {
                Assert.Contains(item, array);
                count++;
            }

            Assert.Equal(count, tree.Count);
        }

        [Fact]
        public void GetEnumeratorShouldEnumerateThroughAllListOfChars()
        {
            var tree = new RadixTree<List<char>>();

            var romane = new List<char>() { 'r', 'o', 'm', 'a', 'n', 'e' };
            var romanus = new List<char>() { 'r', 'o', 'm', 'a', 'n', 'u', 's' };
            var rom = new List<char>() { 'r', 'o', 'm' };
            var romulus = new List<char>() { 'r', 'o', 'm', 'u', 'l', 'u', 's' };
            var rub = new List<char>() { 'r', 'u', 'b' };
            var rubens = new List<char>() { 'r', 'u', 'b', 'e', 'n', 's' };
            var ben = new List<char>() { 'b', 'e', 'n' };
            var ruber = new List<char>() { 'r', 'u', 'b', 'e', 'r' };
            var rubicundus = new List<char>() { 'r', 'u', 'b', 'i', 'c', 'u', 'n', 'd', 'u', 's' };

            tree.Add(romane);
            tree.Add(romanus);
            tree.Add(rom);
            tree.Add(romulus);
            tree.Add(rub);
            tree.Add(rubens);
            tree.Add(ben);
            tree.Add(ruber);
            tree.Add(rubicundus);

            List<char>[] array = { romane, romanus, rom, romulus, rub, rubens, ben, ruber, rubicundus };
            int count = 0;

            foreach (var item in tree)
            {
                Assert.Contains(item, array);
                count++;
            }

            Assert.Equal(count, tree.Count);
        }

        [Fact]
        public void GetEnumeratorShouldEnumerateThroughAllArraysOfChars()
        {
            var tree = new RadixTree<char[]>();
            char[] romane = ['r', 'o', 'm', 'a', 'n', 'e'];
            char[] romanus = ['r', 'o', 'm', 'a', 'n', 'u', 's'];
            char[] rom = ['r', 'o', 'm'];
            char[] romulus = ['r', 'o', 'm', 'u', 'l', 'u', 's'];
            char[] rub = ['r', 'u', 'b'];
            char[] rubens = ['r', 'u', 'b', 'e', 'n', 's'];
            char[] ben = ['b', 'e', 'n'];
            char[] ruber = ['r', 'u', 'b', 'e', 'r'];
            char[] rubicundus = ['r', 'u', 'b', 'i', 'c', 'u', 'n', 'd', 'u', 's'];

            tree.Add(romane);
            tree.Add(romanus);
            tree.Add(rom);
            tree.Add(romulus);
            tree.Add(rub);
            tree.Add(rubens);
            tree.Add(ben);
            tree.Add(ruber);
            tree.Add(rubicundus);

            char[][] array = { romane, romanus, rom, romulus, rub, rubens, ben, ruber, rubicundus };
            int count = 0;

            foreach (var item in tree)
            {
                Assert.Contains(item, array);
                count++;
            }

            Assert.Equal(count, tree.Count);
        }

        [Fact]
        public void GetAllItemsByPrefixShouldReturnsListWithAllStringsStartingWithPrefix()
        {
            var tree = new RadixTree<string>();
            tree.Add("romane");
            tree.Add("romanus");
            tree.Add("rom");
            tree.Add("romulus");
            tree.Add("rub");
            tree.Add("rubens");
            tree.Add("ben");
            tree.Add("ruber");
            tree.Add("rubicundus");

            string[] array = { "romane", "romanus", "rom", "romulus" };

            int count = 0;

            foreach (var item in tree.GetAllItemsByPrefix("ro"))
            {
                Assert.Contains(item, array);
                count++;
            }

            Assert.Equal(count, array.Length);
        }

        [Fact]
        public void GetAllItemsByPrefixShouldReturnsListWithAllListsOfCharsStartingWithPrefix()
        {
            var tree = new RadixTree<List<char>>();

            var romane = new List<char>() { 'r', 'o', 'm', 'a', 'n', 'e' };
            var romanus = new List<char>() { 'r', 'o', 'm', 'a', 'n', 'u', 's' };
            var rom = new List<char>() { 'r', 'o', 'm' };
            var romulus = new List<char>() { 'r', 'o', 'm', 'u', 'l', 'u', 's' };
            var rub = new List<char>() { 'r', 'u', 'b' };
            var rubens = new List<char>() { 'r', 'u', 'b', 'e', 'n', 's' };
            var ben = new List<char>() { 'b', 'e', 'n' };
            var ruber = new List<char>() { 'r', 'u', 'b', 'e', 'r' };
            var rubicundus = new List<char>() { 'r', 'u', 'b', 'i', 'c', 'u', 'n', 'd', 'u', 's' };

            tree.Add(romane);
            tree.Add(romanus);
            tree.Add(rom);
            tree.Add(romulus);
            tree.Add(rub);
            tree.Add(rubens);
            tree.Add(ben);
            tree.Add(ruber);
            tree.Add(rubicundus);

            List<char>[] array = { romane, romanus, rom, romulus };

            int count = 0;

            foreach (var item in tree.GetAllItemsByPrefix("ro"))
            {
                Assert.Contains(item, array);
                count++;
            }

            Assert.Equal(count, array.Length);
        }

        [Fact]
        public void GetAllItemsByPrefixShouldReturnsListWithAllArraysOfCharsStartingWithPrefix()
        {
            var tree = new RadixTree<char[]>();
            char[] romane = ['r', 'o', 'm', 'a', 'n', 'e'];
            char[] romanus = ['r', 'o', 'm', 'a', 'n', 'u', 's'];
            char[] rom = ['r', 'o', 'm'];
            char[] romulus = ['r', 'o', 'm', 'u', 'l', 'u', 's'];
            char[] rub = ['r', 'u', 'b'];
            char[] rubens = ['r', 'u', 'b', 'e', 'n', 's'];
            char[] ben = ['b', 'e', 'n'];
            char[] ruber = ['r', 'u', 'b', 'e', 'r'];
            char[] rubicundus = ['r', 'u', 'b', 'i', 'c', 'u', 'n', 'd', 'u', 's'];

            tree.Add(romane);
            tree.Add(romanus);
            tree.Add(rom);
            tree.Add(romulus);
            tree.Add(rub);
            tree.Add(rubens);
            tree.Add(ben);
            tree.Add(ruber);
            tree.Add(rubicundus);

            char[][] array = { romane, romanus, rom, romulus };

            int count = 0;

            foreach (var item in tree.GetAllItemsByPrefix("ro"))
            {
                Assert.Contains(item, array);
                count++;
            }

            Assert.Equal(count, array.Length);
        }

        [Fact]
        public void GetAllItemsByPrefixShouldReturnsEmptyListWhenPrefixIfNotPresent()
        {
            var tree = new RadixTree<string>();
            tree.Add("romane");
            tree.Add("romanus");
            tree.Add("rom");
            tree.Add("romulus");
            tree.Add("rub");
            tree.Add("rubens");
            tree.Add("ben");
            tree.Add("ruber");
            tree.Add("rubicundus");

            Assert.Empty(tree.GetAllItemsByPrefix("ron"));
        }

        [Fact]
        public void GetLongestPrefixMatchShouldReturnsStringWithLongestMatchFromTree()
        {
            var tree = new RadixTree<string>();
            tree.Add("romane");
            tree.Add("romanus");
            tree.Add("rom");
            tree.Add("romulus");
            tree.Add("rub");
            tree.Add("rubens");
            tree.Add("ben");
            tree.Add("ruber");
            tree.Add("rubicundus");

            Assert.Equal("rubic", tree.GetLongestPrefixMatch("rubicon"));
            Assert.Equal("rube", tree.GetLongestPrefixMatch("rubeus"));
            Assert.Equal("rub", tree.GetLongestPrefixMatch("ruby"));
            Assert.Equal("", tree.GetLongestPrefixMatch("dog"));
        }

        [Fact]
        public void AddShouldThrowArgumentNullExceptionIfItemIsNull()
        {
            var tree = new RadixTree<string>();
            Assert.Throws<ArgumentNullException>(() => tree.Add(null));
        }

        [Fact]
        public void AddShouldThrowArgumentsExceptionWhenItemIsAlreadyInTheTree()
        {
            var tree = new RadixTree<string>();
            tree.Add("romane");
            tree.Add("romanus");

            Assert.Throws<ArgumentException>(() => tree.Add("romane"));
        }

        [Fact]
        public void ContainsShouldThrowArgumentNullExceptionIfItemIsNull()
        {
            var tree = new RadixTree<string>();
            Assert.Throws<ArgumentNullException>(() => tree.Contains(null));
        }

        [Fact]
        public void RemoveShouldThrowArgumentNullExceptionIfItemIsNull()
        {
            var tree = new RadixTree<string>();
            Assert.Throws<ArgumentNullException>(() => tree.Remove(null));
        }

        [Fact]
        public void GetAllItemsByPrefixShouldThrowArgumentNullExceptionIfItemIsNull()
        {
            var tree = new RadixTree<string>();
            Assert.Throws<ArgumentNullException>(() => tree.GetAllItemsByPrefix(null));
        }

        [Fact]
        public void GetLongestPrefixMatchShouldThrowArgumentNullExceptionIfItemIsNull()
        {
            var tree = new RadixTree<string>();
            Assert.Throws<ArgumentNullException>(() => tree.GetLongestPrefixMatch(null));
        }

        [Fact]
        public void ClearShouldSetCounTo0AndClearRootNodeChildren()
        {
            var tree = new RadixTree<string>() { "Ana", "Anda", "Alina", "Maria" };
            tree.Clear();

            int count = 0;
            foreach (var item in tree)
            {
                count++;
            }

            Assert.Equal(0, count);
            Assert.Empty(tree);
        }

        [Fact]
        public void CopyToShouldCopyItemsToArray()
        {
            var tree = new RadixTree<string>() { "Ana", "Anda", "Alina", "Maria", "Mara" };
            string[] array = new string[tree.Count];
            tree.CopyTo(array, 0);

            foreach (var item in tree)
            {
                Assert.True(array.Contains(item));
            }
        }

        [Fact]
        public void CopyToShouldCopyItemsToArrayWhenIndexIsLargerThan0()
        {
            var tree = new RadixTree<string>() { "Ana", "Anda", "Alina", "Maria", "Mara" };
            string[] array = new string[10];
            tree.CopyTo(array, 5);

            foreach (var item in tree)
            {
                Assert.True(array.Contains(item));
            }
        }

        [Fact]
        public void CopyToShouldThrowArgumentNullExceptionWhenArrayIsNull()
        {
            var tree = new RadixTree<string>() { "Ana", "Anda", "Alina", "Maria", "Mara" };

            Assert.Throws<ArgumentNullException>(() => tree.CopyTo(null, 0));
        }

        [Fact]
        public void CopyToShouldThrowArgumentOutOfRangeExceptionWhenIndexIsLessThan0()
        {
            var tree = new RadixTree<string>() { "Ana", "Anda", "Alina", "Maria", "Mara" };
            string[] array = new string[5];

            Assert.Throws<ArgumentOutOfRangeException>(() => tree.CopyTo(array, -1));
        }

        [Fact]
        public void CopyToShouldThrowArgumentExceptionWhenNumberOfItemsInTreeIsGreaterThanSpaceFromIndexToArrayEnd()
        {
            var tree = new RadixTree<string>() { "Ana", "Anda", "Alina", "Maria", "Mara" };
            string[] array = new string[5];

            Assert.Throws<ArgumentException>(() => tree.CopyTo(array, 1));
        }
    }
}
