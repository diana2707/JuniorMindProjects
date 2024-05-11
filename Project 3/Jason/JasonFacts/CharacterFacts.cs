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

            Assert.True(pattern.Match(stringView).Success());
            Assert.Equal('b', pattern.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenPatternIsInvalid()
        {
            Character pattern = new ('a');
            StringView stringView = new ("bc", 0);

            Assert.False(pattern.Match(stringView).Success());
            Assert.Equal('b', pattern.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenStringIsNull()
        {
            Character pattern = new ('a');
            StringView stringView = new (null, 0);

            Assert.False(pattern.Match(stringView).Success());
            Assert.Equal('\0', pattern.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenStringIsEmpty()
        {
            Character pattern = new ('a');
            StringView stringView = new ("", 0);

            Assert.False(pattern.Match(stringView).Success());
            Assert.Equal('\0', pattern.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenPatternIsValidAndIndexIsOutOfRange()
        {
            Character pattern = new ('a');
            StringView stringView = new ("abc", 3);

            Assert.False(pattern.Match(stringView).Success());
            Assert.Equal('\0', pattern.Match(stringView).StringView().Peek());
        }
    }
}
