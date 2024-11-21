namespace ObjectsCollection
{
    public class RadixTreeFacts
    {
        [Fact]
        public void RadixTreeClassExists()
        {
            var tree = new RadixTree<char>();
            Assert.IsType<RadixTree<char>>(tree);
        }

        [Fact]
        public void CountShouldReturn0WhenTreeIsEmpty()
        {
            var tree = new RadixTree<char>();
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
            var tree = new RadixTree<char>();
            tree.Add("cat");
            Assert.Equal(1, tree.Count);
        }

        [Fact]
        public void AddShouldAddElementWhenPrefixIsCommon()
        {
            var tree = new RadixTree<char>();
            tree.Add("cat");
            tree.Add("car");
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void AddShouldAddMultipleElementsWhenPrefixIscommon()
        {
            var tree = new RadixTree<char>();
            tree.Add("cat");
            tree.Add("car");
            tree.Add("cabage");
            tree.Add("candle");
            Assert.Contains("cat", tree);
            Assert.Contains("car", tree);
            Assert.Contains("cabage", tree);
            Assert.Contains("candle", tree);

            Assert.Equal(4, tree.Count);
        }

        [Fact]
        public void AddShouldAddElementWhenNewStringHasAnotherWordAsPrefix()
        {
            var tree = new RadixTree<char>();
            tree.Add("cat");
            tree.Add("catnip");
            Assert.Contains("cat", tree);
            Assert.Contains("catnip", tree);
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void AddShouldAddElementWhenNewStringIsPartOfAWordFromTree()
        {
            var tree = new RadixTree<char>();
            tree.Add("catnip");
            tree.Add("cat");
            Assert.Contains("cat", tree);
            Assert.Contains("catnip", tree);
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void AddShouldAddElementWhenNewStringIsPrefixFor2WordsInTree()
        {
            var tree = new RadixTree<char>();
            tree.Add("catnip");
            tree.Add("category");
            tree.Add("cat");
            Assert.Contains("cat", tree);
            Assert.Contains("catnip", tree);
            Assert.Contains("category", tree);
            Assert.Equal(3, tree.Count);
        }

        [Fact]
        public void AddShouldNotAddElementWhen2StringsHaveWordAsCommonPrefix()
        {
            var tree = new RadixTree<char>();
            tree.Add("catnip");
            tree.Add("category");

            Assert.DoesNotContain("cat", tree);
            Assert.Contains("catnip", tree);
            Assert.Contains("category", tree);
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void AddShouldAddElementsWithDifferntLengthsAndCommonPrefixes()
        {
            var tree = new RadixTree<char>();
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

            Assert.Contains("romane", tree);
            Assert.Contains("romanus", tree);
            Assert.Contains("rom", tree);
            Assert.Contains("romulus", tree);
            Assert.Contains("romanum", tree);
            Assert.Contains("rub", tree);
            Assert.Contains("rubens", tree);
            Assert.Contains("ben", tree);
            Assert.Contains("ruber", tree);
            Assert.Contains("rubicundus", tree);
            Assert.Equal(10, tree.Count);
        }

        [Fact]
        public void AddShouldAddListsOfIntElements()
        {
            var tree = new RadixTree<int>();
            var list1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            var list2 = new List<int>() { 1, 2, 3, 4, 5, 7, 8 };
            var list3 = new List<int>() { 1, 2, 3 };
            var list4 = new List<int>() { 1, 2, 3, 7, 9, 7, 8 };
            var list5 = new List<int>() { 1, 7, 10 };
            var list6 = new List<int>() { 1, 7, 10, 6, 5, 8 };
            var list7 = new List<int>() { 10, 6, 5 };
            var list8 = new List<int>() { 1, 7, 10, 6, 1 };
            var list9 = new List<int>() { 1, 7, 10, 12, 13, 7, 5, 14, 7, 8 };

            tree.Add(list1);
            tree.Add(list2);
            tree.Add(list3);
            tree.Add(list4);
            tree.Add(list5);
            tree.Add(list6);
            tree.Add(list7);
            tree.Add(list8);
            tree.Add(list9);

            Assert.Contains(list1, tree);
            Assert.Contains(list2, tree);
            Assert.Contains(list3, tree);
            Assert.Contains(list4, tree);
            Assert.Contains(list5, tree);
            Assert.Contains(list6, tree);
            Assert.Contains(list7, tree);
            Assert.Contains(list8, tree);
            Assert.Contains(list9, tree);
            Assert.Equal(9, tree.Count);
        }

        [Fact]
        public void AddShouldAddListsOfCharsElementsw()
        {
            var tree = new RadixTree<char>();
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

            Assert.Contains(romane, tree);
            Assert.Contains(romanus, tree);
            Assert.Contains(rom, tree);
            Assert.Contains(romulus, tree);
            Assert.Contains(rub, tree);
            Assert.Contains(rubens, tree);
            Assert.Contains(ben, tree);
            Assert.Contains(ruber, tree);
            Assert.Contains(rubicundus, tree);
            Assert.Equal(9, tree.Count);
        }

        [Fact]
        public void AddShouldAddArrayOfCharsElements()
        {
            var tree = new RadixTree<char>();
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

            Assert.Contains(romane, tree);
            Assert.Contains(romanus, tree);
            Assert.Contains(rom, tree);
            Assert.Contains(romulus, tree);
            Assert.Contains(rub, tree);
            Assert.Contains(rubens, tree);
            Assert.Contains(ben, tree);
            Assert.Contains(ruber, tree);
            Assert.Contains(rubicundus, tree);
            Assert.Equal(9, tree.Count);
        }

        [Fact]
        public void ContainsShouldReturnTrueIfStringExistsInTree()
        {
            var tree = new RadixTree<char>();
            tree.Add("cat");
            tree.Add("car");
            Assert.True(tree.Contains("car"));
            Assert.True(tree.Contains("cat"));
        }

        [Fact]
        public void ContainsShouldReturnFalseIfStringDoNotExistsInTree()
        {
            var tree = new RadixTree<char>();
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
            var tree = new RadixTree<char>();
            tree.Add("cab");
            tree.Add("cabage");
            Assert.True(tree.Contains("cab"));
            Assert.True(tree.Contains("cabage"));
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void ContainsShouldReturnTrueWhenEndOfStringIsTheSameAsPrefix()
        {
            var tree = new RadixTree<char>() { "Ana", "Anda", "Alina", "Ana-Maria", "Maria", "Mara" };

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
            var tree = new RadixTree<char>() { ana, anda, alina, anaMaria, maria, mara };

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
            var tree = new RadixTree<char>() { ana, anda, alina, anaMaria, maria, mara };

            Assert.True(tree.Contains(ana));
            Assert.True(tree.Contains(anaMaria));
            Assert.Equal(6, tree.Count);
        }

        [Fact]
        public void RemoveShouldReturnTrueWhenRemovesWordsThatArePrefixForOtherWords()
        {
            var tree = new RadixTree<char>();
            tree.Add("cat");
            tree.Add("catnip");
            tree.Remove("cat");

            Assert.DoesNotContain("cat", tree);
            Assert.Contains("catnip", tree);
            Assert.Single(tree);
        }

        [Fact]
        public void RemoveShouldReturnTrueWhenRemovesWordsWithNoCommonSufix()
        {
            var tree = new RadixTree<char>();
            tree.Add("cat");
            tree.Add("car");

            Assert.True(tree.Remove("car"));
            Assert.DoesNotContain("car", tree);
            Assert.Contains("cat", tree);
            Assert.Single(tree);
        }

        [Fact]
        public void RemoveShouldReturnFalseWhenTriesToRemoveWordsThatAreNotInTree()
        {
            var tree = new RadixTree<char>();
            tree.Add("cat");
            tree.Add("car");

            Assert.False(tree.Remove("cab"));
            Assert.False(tree.Remove("bag"));
            Assert.Contains("car", tree);
            Assert.Contains("cat", tree);
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void RemoveShouldReturnTrueAndRemoveStringsWhenTreeHasMultipleStrings()
        {
            var tree = new RadixTree<char>();
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

            Assert.Contains("romane", tree);
            Assert.Contains("romulus", tree);
            Assert.Contains("rub", tree);
            Assert.Contains("rubens", tree);
            Assert.Contains("ruber", tree);

            Assert.DoesNotContain("rom", tree);
            Assert.DoesNotContain("rubicundus", tree);
            Assert.DoesNotContain("ben", tree);
            Assert.DoesNotContain("romanus", tree);

            Assert.DoesNotContain("roman", tree);

            Assert.Equal(5, tree.Count);
        }

        [Fact]
        public void RemoveShouldRemoveListsOfCharsElements()
        {
            var tree = new RadixTree<char>();
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

            Assert.Contains(romane, tree);
            Assert.Contains(romulus, tree);
            Assert.Contains(rub, tree);
            Assert.Contains(rubens, tree);
            Assert.Contains(ruber, tree);

            Assert.DoesNotContain(rom, tree);
            Assert.DoesNotContain(rubicundus, tree);
            Assert.DoesNotContain(ben, tree);
            Assert.DoesNotContain(romanus, tree);

            Assert.DoesNotContain(roman, tree);

            Assert.Equal(5, tree.Count);
        }

        [Fact]
        public void RemoveShouldRemoveListsOfIntElements()
        {
            var tree = new RadixTree<int>();
            var list1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            var list2 = new List<int>() { 1, 2, 3, 4, 5, 7, 8 };
            var list3 = new List<int>() { 1, 2, 3 };
            var list4 = new List<int>() { 1, 2, 3, 7, 9, 7, 8 };
            var list5 = new List<int>() { 1, 7, 10 };
            var list6 = new List<int>() { 1, 7, 10, 6, 5, 8 };
            var list7 = new List<int>() { 10, 6, 5 };
            var list8 = new List<int>() { 1, 7, 10, 6, 1 };
            var list9 = new List<int>() { 1, 7, 10, 12, 13, 7, 5, 14, 7, 8 };
            var list10 = new List<int>() { 1, 2, 3, 4, 5 };

            tree.Add(list1);
            tree.Add(list2);
            tree.Add(list3);
            tree.Add(list4);
            tree.Add(list5);
            tree.Add(list6);
            tree.Add(list7);
            tree.Add(list8);
            tree.Add(list9);

            Assert.True(tree.Remove(list3));
            Assert.True(tree.Remove(list9));
            Assert.True(tree.Remove(list7));
            Assert.True(tree.Remove(list2));

            Assert.Contains(list1, tree);
            Assert.Contains(list4, tree);
            Assert.Contains(list5, tree);
            Assert.Contains(list6, tree);
            Assert.Contains(list8, tree);

            Assert.DoesNotContain(list3, tree);
            Assert.DoesNotContain(list9, tree);
            Assert.DoesNotContain(list7, tree);
            Assert.DoesNotContain(list2, tree);

            Assert.DoesNotContain(list10, tree);

            Assert.Equal(5, tree.Count);
        }

        [Fact]
        public void RemoveShoulRemoveArrayOfCharsElements()
        {
            var tree = new RadixTree<char>();
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

            Assert.Contains(romane, tree);
            Assert.Contains(romulus, tree);
            Assert.Contains(rub, tree);
            Assert.Contains(rubens, tree);
            Assert.Contains(ruber, tree);

            Assert.DoesNotContain(rom, tree);
            Assert.DoesNotContain(rubicundus, tree);
            Assert.DoesNotContain(ben, tree);
            Assert.DoesNotContain(romanus, tree);

            Assert.DoesNotContain(roman, tree);

            Assert.Equal(5, tree.Count);
        }

        [Fact]
        public void GetEnumeratorShouldEnumerateThroughAllStrings()
        {
            var tree = new RadixTree<char>();
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
            var tree = new RadixTree<char>();

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
            var tree = new RadixTree<char>();
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
        public void FindByPrefixShouldReturnsListWithAllStringsStartingWithPrefix()
        {
            var tree = new RadixTree<char>();
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

            foreach (var item in tree.FindByPrefix("ro"))
            {
                Assert.Contains(item, array);
                count++;
            }

            Assert.Equal(count, array.Length);
        }

        [Fact]
        public void FindIByPrefixShouldReturnsListWithAllListsOfCharsStartingWithPrefix()
        {
            var tree = new RadixTree<char>();

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

            foreach (var item in tree.FindByPrefix("ro"))
            {
                Assert.Contains(item, array);
                count++;
            }

            Assert.Equal(count, array.Length);
        }

        [Fact]
        public void FindByPrefixShouldReturnsListWithAllArraysOfCharsStartingWithPrefix()
        {
            var tree = new RadixTree<char>();
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

            foreach (var item in tree.FindByPrefix("ro"))
            {
                Assert.Contains(item, array);
                count++;
            }

            Assert.Equal(count, array.Length);
        }

        [Fact]
        public void FindByPrefixShouldReturnsEmptyListWhenPrefixIfNotPresent()
        {
            var tree = new RadixTree<char>();
            tree.Add("romane");
            tree.Add("romanus");
            tree.Add("rom");
            tree.Add("romulus");
            tree.Add("rub");
            tree.Add("rubens");
            tree.Add("ben");
            tree.Add("ruber");
            tree.Add("rubicundus");

            Assert.Empty(tree.FindByPrefix("ron"));
        }

        [Fact]
        public void GetPrefixMatchShouldReturnsStringWithLongestMatchFromTree()
        {
            var tree = new RadixTree<char>();
            tree.Add("romane");
            tree.Add("romanus");
            tree.Add("rom");
            tree.Add("romulus");
            tree.Add("rub");
            tree.Add("rubens");
            tree.Add("ben");
            tree.Add("ruber");
            tree.Add("rubicundus");

            Assert.Equal(['r', 'u', 'b', 'i', 'c'], tree.GetPrefixMatch("rubicon"));
            Assert.Equal(['r', 'u', 'b', 'e'], tree.GetPrefixMatch("rubeus"));
            Assert.Equal(['r', 'u', 'b'], tree.GetPrefixMatch("ruby"));
            Assert.Equal([], tree.GetPrefixMatch("dog"));
        }

        //[Fact]
        //public void AddShouldThrowArgumentNullExceptionIfItemIsNull()
        //{
        //    var tree = new RadixTree<char>();
        //    Assert.Throws<ArgumentNullException>(() => tree.Add(null));
        //}

        //[Fact]
        //public void AddShouldThrowArgumentsExceptionWhenItemIsAlreadyInTheTree()
        //{
        //    var tree = new RadixTree<char>();
        //    tree.Add("romane");
        //    tree.Add("romanus");

        //    Assert.Throws<ArgumentException>(() => tree.Add("romane"));
        //}

        //[Fact]
        //public void ContainsShouldThrowArgumentNullExceptionIfItemIsNull()
        //{
        //    var tree = new RadixTree<string>();
        //    Assert.Throws<ArgumentNullException>(() => tree.Contains(null));
        //}

        //[Fact]
        //public void RemoveShouldThrowArgumentNullExceptionIfItemIsNull()
        //{
        //    var tree = new RadixTree<char>();
        //    Assert.Throws<ArgumentNullException>(() => tree.Remove(null));
        //}

        //[Fact]
        //public void GetAllItemsByPrefixShouldThrowArgumentNullExceptionIfItemIsNull()
        //{
        //    var tree = new RadixTree<char>();
        //    Assert.Throws<ArgumentNullException>(() => tree.FindByPrefix(null));
        //}

        //[Fact]
        //public void GetLongestPrefixMatchShouldThrowArgumentNullExceptionIfItemIsNull()
        //{
        //    var tree = new RadixTree<char>();
        //    Assert.Throws<ArgumentNullException>(() => tree.GetPrefixMatch(null));
        //}

        //[Fact]
        //public void ClearShouldSetCounTo0AndClearRootNodeChildren()
        //{
        //    var tree = new RadixTree<char>() { "Ana", "Anda", "Alina", "Maria" };
        //    tree.Clear();

        //    int count = 0;
        //    foreach (var item in tree)
        //    {
        //        count++;
        //    }

        //    Assert.Equal(0, count);
        //    Assert.Empty(tree);
        //}

        //[Fact]
        //public void CopyToShouldCopyItemsToArray()
        //{
        //    var tree = new RadixTree<char>() { "Ana", "Anda", "Alina", "Maria", "Mara" };
        //    IEnumerable<char>[] array = new IEnumerable<char>[tree.Count];
        //    tree.CopyTo(array, 0);

        //    foreach (var item in tree)
        //    {
        //        Assert.Contains(item, array);
        //    }
        //}

        //[Fact]
        //public void CopyToShouldCopyItemsToArrayWhenIndexIsLargerThan0()
        //{
        //    var tree = new RadixTree<char>() { "Ana", "Anda", "Alina", "Maria", "Mara" };
        //    IEnumerable<char>[] array = new IEnumerable<char>[10];
        //    tree.CopyTo(array, 5);

        //    foreach (var item in tree)
        //    {
        //        Assert.Contains(item, array);
        //    }
        //}

        //[Fact]
        //public void CopyToShouldThrowArgumentNullExceptionWhenArrayIsNull()
        //{
        //    var tree = new RadixTree<char>() { "Ana", "Anda", "Alina", "Maria", "Mara" };

        //    Assert.Throws<ArgumentNullException>(() => tree.CopyTo(null, 0));
        //}

        //[Fact]
        //public void CopyToShouldThrowArgumentOutOfRangeExceptionWhenIndexIsLessThan0()
        //{
        //    var tree = new RadixTree<char>() { "Ana", "Anda", "Alina", "Maria", "Mara" };
        //    IEnumerable<char>[] array = new IEnumerable<char>[5];

        //    Assert.Throws<ArgumentOutOfRangeException>(() => tree.CopyTo(array, -1));
        //}

        //[Fact]
        //public void CopyToShouldThrowArgumentExceptionWhenNumberOfItemsInTreeIsGreaterThanSpaceFromIndexToArrayEnd()
        //{
        //    var tree = new RadixTree<char>() { "Ana", "Anda", "Alina", "Maria", "Mara" };
        //    IEnumerable<char>[] array = new IEnumerable<char>[5];

        //    Assert.Throws<ArgumentException>(() => tree.CopyTo(array, 1));
        //}
    }
}
