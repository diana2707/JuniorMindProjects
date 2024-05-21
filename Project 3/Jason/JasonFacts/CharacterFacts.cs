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
            StringView stringView = new ("abc", 0);
            StringView failedIndex = new ("abc", 0);
            Assert.True(pattern.Match(stringView, failedIndex).Success());
            Assert.Equal('b', pattern.Match(stringView, failedIndex).StringView().Peek());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenPatternIsInvalid()
        {
            Character pattern = new ('a');
            StringView stringView = new ("bc", 0);
            StringView failedIndex = new ("bc", 0);
            Assert.False(pattern.Match(stringView, failedIndex).Success());
            Assert.Equal('b', pattern.Match(stringView, failedIndex).StringView().Peek());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenStringIsNull()
        {
            Character pattern = new ('a');
            StringView stringView = new (null, 0);
            StringView failedIndex = new (null, 0);
            Assert.False(pattern.Match(stringView, failedIndex).Success());
            Assert.Equal('\0', pattern.Match(stringView, failedIndex).StringView().Peek());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenStringIsEmpty()
        {
            Character pattern = new ('a');
            StringView stringView = new ("", 0);
            StringView failedIndex = new ("", 0);
            Assert.False(pattern.Match(stringView, failedIndex).Success());
            Assert.Equal('\0', pattern.Match(stringView, failedIndex).StringView().Peek());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenPatternIsValidAndIndexIsOutOfRange()
        {
            Character pattern = new ('a');
            StringView stringView = new ("abc", 3);
            StringView failedIndex = new ("abc", 3);
            Assert.False(pattern.Match(stringView, failedIndex).Success());
            Assert.Equal('\0', pattern.Match(stringView, failedIndex).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFailedIndexWhenIndexIsNotLarger()
        {
            Character pattern = new ('a');
            StringView stringView = new ("bcd", 0);
            StringView failedIndex = new ("bcd", 0);

            Assert.False(pattern.Match(stringView, failedIndex).Success());
            Assert.Equal('b', pattern.Match(stringView, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFailedIndexWhenIndexIsLarger()
        {
            Character pattern = new ('a');
            StringView stringView = new ("bcd", 0);
            StringView failedIndex = new ("bcd", 1);

            Assert.False(pattern.Match(stringView, failedIndex).Success());
            Assert.Equal('c', pattern.Match(stringView, failedIndex).FailedIndex().Peek());
        }
    }
}
