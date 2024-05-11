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
    }
}
