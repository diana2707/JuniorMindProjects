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
            Match match = new (text, true);

            Assert.True(pattern.Match(text).Success());
            Assert.Equal("bc", pattern.Match(text).RemainingText());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenPatternIsInvalid()
        {
            Character pattern = new ('a');
            const string text = "bc";
            Match match = new (text, false);

            Assert.False(pattern.Match(text).Success());
            Assert.Equal("bc", pattern.Match(text).RemainingText());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenStringIsNull()
        {
            Character pattern = new ('a');
            const string text = null;
            Match match = new (text, false);

            Assert.False(pattern.Match(text).Success());
            Assert.Equal(text, pattern.Match(text).RemainingText());
        }

        [Fact]
        public void MatchShoudReturnFalseWhenStringIsEmpty()
        {
            Character pattern = new ('a');
            const string text = "";
            Match match = new (text, false);

            Assert.False(pattern.Match(text).Success());
            Assert.Equal(text, pattern.Match(text).RemainingText());
        }
    }
}
