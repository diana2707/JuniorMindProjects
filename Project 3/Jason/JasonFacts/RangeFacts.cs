using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class RangeFacts
    {
        [Fact]
        public void MatchShouldReturnFalseWhenStringIsNull()
        {
            Range digit = new ('a', 'f');
            StringView stringView = new (null, 0);

            Assert.False(digit.Match(stringView).Success());
            Assert.Equal('\0', digit.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void MatchShouldReturnFalseWhenStringIsEmpty()
        {
            Range digit = new ('a', 'f');
            StringView stringView = new ("", 0);

            Assert.False(digit.Match(stringView).Success());
            Assert.Equal('\0', digit.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void MatchShouldReturnFalseWhenStringStartsWithInvalidCharacters()
        {
            Range digit = new ('a', 'f');
            StringView stringView = new ("1acfd", 0);

            Assert.False(digit.Match(stringView).Success());
            Assert.Equal('1', digit.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void MatchShouldReturnTrueWhenStringStartsWithValidCharacters()
        {
            Range digit = new ('a', 'f');
            StringView stringView = new ("acfdb", 0);

            Assert.True(digit.Match(stringView).Success());
            Assert.Equal('c', digit.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void MatchShouldReturnTrueWhenStringStartsWithValidCharactersAndIndexIsOutOfRange()
        {
            Range digit = new ('a', 'f');
            StringView stringView = new ("acfdb", 5);

            Assert.False(digit.Match(stringView).Success());
            Assert.Equal('\0', digit.Match(stringView).StringView().Peek());
        }
    }
}
