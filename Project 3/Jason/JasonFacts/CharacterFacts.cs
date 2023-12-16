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
            const string text = "abc";

            Assert.True(pattern.Match(text).Success());
            Assert.Equal("bc", pattern.Match(text).RemainingText());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenPatternIsInvalid()
        {
            Character pattern = new ('a');
            const string text = "bc";

            Assert.False(pattern.Match(text).Success());
            Assert.Equal("bc", pattern.Match(text).RemainingText());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenStringIsNull()
        {
            Character pattern = new ('a');
            const string text = null;

            Assert.False(pattern.Match(text).Success());
            Assert.Equal(text, pattern.Match(text).RemainingText());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenStringIsEmpty()
        {
            Character pattern = new ('a');
            const string text = "";

            Assert.False(pattern.Match(text).Success());
            Assert.Equal(text, pattern.Match(text).RemainingText());
        }
    }
}
