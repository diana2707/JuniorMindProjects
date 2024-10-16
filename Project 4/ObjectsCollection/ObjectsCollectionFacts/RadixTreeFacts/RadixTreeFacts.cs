namespace ObjectsCollection
{
    public class RadixTreeFacts
    {
        [Fact]
        public void RadixTreeClassExists()
        {
            var tree = new RadixTree();
            Assert.IsType<RadixTree>(tree);
        }

        [Fact]
        public void CountShouldReturn0WhenTreeIsEmpty()
        {
            var tree = new RadixTree();
            Assert.Equal(0, tree.Count);
        }

        [Fact]
        public void AddShouldAddFirstElementWhenTreeIsEmpty()
        {
            var tree = new RadixTree();
            tree.Add("cat");
            Assert.Equal(1, tree.Count);
        }

        [Fact]
        public void AddShouldAddElementWhenPrefixIsCommon()
        {
            var tree = new RadixTree();
            tree.Add("cat");
            tree.Add("car");
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void AddShouldAddMultipleElementsWhenPrefixIscommon()
        {
            var tree = new RadixTree();
            tree.Add("cat");
            tree.Add("car");
            tree.Add("cabage");
            tree.Add("candle");
            Assert.True(tree.Contains("cabage"));
            Assert.True(tree.Contains("candle"));
            Assert.Equal(4, tree.Count);
        }

        [Fact]
        public void AddShouldAddElementWhenNewStringHasAnotherWordAsPrefix()
        {
            var tree = new RadixTree();
            tree.Add("cat");
            tree.Add("catnip");
            Assert.True(tree.Contains("cat"));
            Assert.True(tree.Contains("catnip"));
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void AddShouldAddElementWhenNewStringIsPartOfAWordFromTree()
        {
            var tree = new RadixTree();
            tree.Add("catnip");
            tree.Add("cat");
            Assert.True(tree.Contains("cat"));
            Assert.True(tree.Contains("catnip"));
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void AddShouldAddElementWhenNewStringIsPrefixFor2WordsInTree()
        {
            var tree = new RadixTree();
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
            var tree = new RadixTree();
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
            var tree = new RadixTree();
            tree.Add("romane");
            tree.Add("romanus");
            tree.Add("rom");
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
            Assert.True(tree.Contains("rub"));
            Assert.True(tree.Contains("rubens"));
            Assert.True(tree.Contains("ben"));
            Assert.True(tree.Contains("ruber"));
            Assert.True(tree.Contains("rubicundus"));
            Assert.Equal(9, tree.Count);
        }

        [Fact]
        public void ContainsShouldReturnTrueIfStringExistsInTree()
        {
            var tree = new RadixTree();
            tree.Add("cat");
            tree.Add("car");
            Assert.True(tree.Contains("car"));
            Assert.True(tree.Contains("cat"));
        }

        [Fact]
        public void ContainsShouldReturnFalseIfStringDoNotExistsInTree()
        {
            var tree = new RadixTree();
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
            var tree = new RadixTree();
            tree.Add("cab");
            tree.Add("cabage");
            Assert.True(tree.Contains("cab"));
            Assert.True(tree.Contains("cabage"));
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void RemoveShouldReturnTrueWhenRemovesWordsThatArePrefixForOtherWords()
        {
            var tree = new RadixTree();
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
            var tree = new RadixTree();
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
            var tree = new RadixTree();
            tree.Add("cat");
            tree.Add("car");

            Assert.False(tree.Remove("cab"));
            Assert.True(tree.Contains("car"));
            Assert.True(tree.Contains("cat"));
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void RemoveShouldReturnTrueAndRemoveStringsWhenTreeHasMultipleStrings()
        {
            var tree = new RadixTree();
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

            Assert.Equal(5, tree.Count);
        }
    }
}
