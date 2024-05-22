using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class CharacterFacts
    {
        [Fact]
        public void MatchShoudReturnTrueWhenPatternIsValid()
        {
            Character pattern = new ('a');
            StringView currentIndex = new ("abc", 0);
            StringView failedIndex = new ("abc", 0);
            Assert.True(pattern.Match(currentIndex, failedIndex).Success());
            Assert.Equal('b', pattern.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenPatternIsInvalid()
        {
            Character pattern = new ('a');
            StringView currentIndex = new ("bc", 0);
            StringView failedIndex = new ("bc", 0);
            Assert.False(pattern.Match(currentIndex, failedIndex).Success());
            Assert.Equal('b', pattern.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenStringIsNull()
        {
            Character pattern = new ('a');
            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);
            Assert.False(pattern.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', pattern.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenStringIsEmpty()
        {
            Character pattern = new ('a');
            StringView currentIndex = new ("", 0);
            StringView failedIndex = new ("", 0);
            Assert.False(pattern.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', pattern.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenPatternIsValidAndIndexIsOutOfRange()
        {
            Character pattern = new ('a');
            StringView currentIndex = new ("abc", 3);
            StringView failedIndex = new ("abc", 3);
            Assert.False(pattern.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', pattern.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFailedIndexWhenIndexIsNotLarger()
        {
            Character pattern = new ('a');
            StringView currentIndex = new ("bcd", 0);
            StringView failedIndex = new ("bcd", 0);

            Assert.False(pattern.Match(currentIndex, failedIndex).Success());
            Assert.Equal('b', pattern.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFailedIndexWhenIndexIsLarger()
        {
            Character pattern = new ('a');
            StringView currentIndex = new ("bcd", 0);
            StringView failedIndex = new ("bcd", 1);

            Assert.False(pattern.Match(currentIndex, failedIndex).Success());
            Assert.Equal('c', pattern.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }
    }
}
