using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class StringViewFacts
    {
        [Fact]
        public void IsEmptyShouldReturnTrueWhenTextIsEmpty()
        {
            StringView stringView = new ("", 0);
            Assert.True(stringView.IsEmpty());
        }

        [Fact]
        public void IsEmptyShouldReturnTrueWhenTextIsNull()
        {
            StringView stringView = new (null, 0);
            Assert.True(stringView.IsEmpty());
        }

        [Fact]
        public void IsEmptyShouldReturnTrueWhenIndexIsOutOfRange()
        {
            StringView stringView = new ("abc", 3);
            Assert.True(stringView.IsEmpty());
        }

        [Fact]
        public void IsEmptyShouldReturnFalseWhenTextIsValid()
        {
            StringView stringView = new ("abc", 0);
            Assert.False(stringView.IsEmpty());
        }

        [Fact]
        public void PeekShouldReturnCorrectCharWhenTextIsValid()
        {
            StringView stringView = new ("abc", 1);
            Assert.Equal('b', stringView.Peek());
        }

        [Fact]
        public void PeekShouldReturnNullCharWhenTextIsEmpy()
        {
            StringView stringView = new ("", 0);
            Assert.Equal('\0', stringView.Peek());
        }

        [Fact]
        public void PeekShouldReturnNullCharWhenTextIsNull()
        {
            StringView stringView = new (null, 0);
            Assert.Equal('\0', stringView.Peek());
        }

        [Fact]
        public void PeekShouldReturnNullCharWhenIndexIsOutOfRange()
        {
            StringView stringView = new ("abc", 5);
            Assert.Equal('\0', stringView.Peek());
        }

        [Fact]
        public void AdvanceShouldReturnNextCharWhenTextIsValid()
        {
            StringView stringView = new ("abc", 0);
            StringView result = stringView.Advance();
            Assert.Equal('b', result.Peek());
        }

        [Fact]
        public void AdvanceShouldReturnNullCharWhenTextIsEmpty()
        {
            StringView stringView = new ("", 0);
            StringView result = stringView.Advance();
            Assert.Equal('\0', result.Peek());
        }

        [Fact]
        public void AdvanceShouldReturnNullCharWhenTextIsNull()
        {
            StringView stringView = new (null, 0);
            StringView result = stringView.Advance();
            Assert.Equal('\0', result.Peek());
        }

        [Fact]
        public void AdvanceShouldReturnNullCharWhenIndexIsOutOfRange()
        {
            StringView stringView = new ("abc", 2);
            StringView result = stringView.Advance();
            Assert.Equal('\0', result.Peek());
        }

        [Fact]
        public void StartsWithShouldReturnTrueWhenPrefixMatchesWhenIndexIs0()
        {
            StringView stringView = new ("abc", 0);
            Assert.True(stringView.StartsWith("ab"));
        }

        [Fact]
        public void StartsWithShouldReturnTrueWhenPrefixMatchesWhenIndexIsNot0()
        {
            StringView stringView = new ("abc", 1);
            Assert.True(stringView.StartsWith("bc"));
        }

        [Fact]
        public void StartsWithShouldReturnFalseWhenPrefixDoNotMatch()
        {
            StringView stringView = new ("abc", 0);
            Assert.False(stringView.StartsWith("b"));
        }

        [Fact]
        public void StartsWithShouldReturnFalseWhenIndexIsOutOfRange()
        {
            StringView stringView = new ("abc", 2);
            Assert.False(stringView.StartsWith("cd"));
        }

        [Fact]
        public void GetLocationShouldReturnRowAndColumnWhenTextHasOneLine()
        {
            StringView stringView = new ("abc", 2);
            Assert.Equal((1, 4), stringView.GetLocation());
        }

        [Fact]
        public void GetLocationShouldReturnRowAndColumnWhenTextHasMultipleLines()
        {
            StringView stringView = new ("abc\r\ndef\r\nghi", 11);
            Assert.Equal((3, 3), stringView.GetLocation());
        }

        [Fact]
        public void IsIndexLargerThanShouldReturnTrueWhenIndexIsLarger()
        {
            StringView stringView = new ("abcdefgh", 6);
            StringView failedStringView = new ("abcdefgh", 4);
            Assert.True(stringView.IsIndexLargerThan(failedStringView));
        }

        [Fact]
        public void IsIndexLargerThanShouldReturnFalseWhenIndexIsSmaller()
        {
            StringView stringView = new ("abcdefgh", 6);
            StringView failedStringView = new ("abcdefgh", 7);
            Assert.False(stringView.IsIndexLargerThan(failedStringView));
        }
    }
}
