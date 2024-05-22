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
            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);

            Assert.False(digit.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', digit.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void MatchShouldReturnFalseWhenStringIsEmpty()
        {
            Range digit = new ('a', 'f');
            StringView currentIndex = new ("", 0);
            StringView failedIndex = new ("", 0);

            Assert.False(digit.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', digit.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void MatchShouldReturnFalseWhenStringStartsWithInvalidCharacters()
        {
            Range digit = new ('a', 'f');
            StringView currentIndex = new ("1acfd", 0);
            StringView failedIndex = new ("1acfd", 0);

            Assert.False(digit.Match(currentIndex, failedIndex).Success());
            Assert.Equal('1', digit.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void MatchShouldReturnTrueWhenStringStartsWithValidCharacters()
        {
            Range digit = new ('a', 'f');
            StringView currentIndex = new ("acfdb", 0);
            StringView failedIndex = new ("acfdb", 0);

            Assert.True(digit.Match(currentIndex, failedIndex).Success());
            Assert.Equal('c', digit.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void MatchShouldReturnFalseWhenStringStartsWithValidCharactersAndIndexIsOutOfRange()
        {
            Range digit = new ('a', 'f');
            StringView currentIndex = new ("acfdb", 5);
            StringView failedIndex = new ("acfdb", 5);

            Assert.False(digit.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', digit.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void MatchShouldReturnCorrectFailedIndexWhenIsNotLarger()
        {
            Range digit = new ('a', 'f');
            StringView currentIndex = new ("1acfdb", 0);
            StringView failedIndex = new ("1acfdb", 0);

            Assert.False(digit.Match(currentIndex, failedIndex).Success());
            Assert.Equal('1', digit.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void MatchShouldReturnCorrectFailedIndexWhenIsLarger()
        {
            Range digit = new ('a', 'f');
            StringView currentIndex = new ("1acfdb", 0);
            StringView failedIndex = new ("1acfdb", 3);

            Assert.False(digit.Match(currentIndex, failedIndex).Success());
            Assert.Equal('f', digit.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }
    }
}
