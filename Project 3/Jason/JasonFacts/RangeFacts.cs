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
            StringView failedIndex = new (null, 0);

            Assert.False(digit.Match(stringView, failedIndex).Success());
            Assert.Equal('\0', digit.Match(stringView, failedIndex).StringView().Peek());
        }

        [Fact]
        public void MatchShouldReturnFalseWhenStringIsEmpty()
        {
            Range digit = new ('a', 'f');
            StringView stringView = new ("", 0);
            StringView failedIndex = new ("", 0);

            Assert.False(digit.Match(stringView, failedIndex).Success());
            Assert.Equal('\0', digit.Match(stringView, failedIndex).StringView().Peek());
        }

        [Fact]
        public void MatchShouldReturnFalseWhenStringStartsWithInvalidCharacters()
        {
            Range digit = new ('a', 'f');
            StringView stringView = new ("1acfd", 0);
            StringView failedIndex = new ("1acfd", 0);

            Assert.False(digit.Match(stringView, failedIndex).Success());
            Assert.Equal('1', digit.Match(stringView, failedIndex).StringView().Peek());
        }

        [Fact]
        public void MatchShouldReturnTrueWhenStringStartsWithValidCharacters()
        {
            Range digit = new ('a', 'f');
            StringView stringView = new ("acfdb", 0);
            StringView failedIndex = new ("acfdb", 0);

            Assert.True(digit.Match(stringView, failedIndex).Success());
            Assert.Equal('c', digit.Match(stringView, failedIndex).StringView().Peek());
        }

        [Fact]
        public void MatchShouldReturnFalseWhenStringStartsWithValidCharactersAndIndexIsOutOfRange()
        {
            Range digit = new ('a', 'f');
            StringView stringView = new ("acfdb", 5);
            StringView failedIndex = new ("acfdb", 5);

            Assert.False(digit.Match(stringView, failedIndex).Success());
            Assert.Equal('\0', digit.Match(stringView, failedIndex).StringView().Peek());
        }

        [Fact]
        public void MatchShouldReturnCorrectFailedIndexWhenIsNotLarger()
        {
            Range digit = new ('a', 'f');
            StringView stringView = new ("1acfdb", 0);
            StringView failedIndex = new ("1acfdb", 0);

            Assert.False(digit.Match(stringView, failedIndex).Success());
            Assert.Equal('1', digit.Match(stringView, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void MatchShouldReturnCorrectFailedIndexWhenIsLarger()
        {
            Range digit = new ('a', 'f');
            StringView stringView = new ("1acfdb", 0);
            StringView failedIndex = new ("1acfdb", 3);

            Assert.False(digit.Match(stringView, failedIndex).Success());
            Assert.Equal('f', digit.Match(stringView, failedIndex).FailedIndex().Peek());
        }
    }
}
