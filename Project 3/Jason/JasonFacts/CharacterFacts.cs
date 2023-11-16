using Xunit;
using static Jason.Character;

namespace Jason
{
    public class CharacterFacts
    {
        [Fact]
        public void MatchShoudReturnTrueWhenPatternIsValid()
        {
            Character pattern = new ('a');
            const string text = "abc";
            Assert.True(pattern.Match(text));
        }

        [Fact]
        public void MatchShoudReturnFalseWhenPatternIsInvalid()
        {
            Character pattern = new ('a');
            const string text = "bc";
            Assert.False(pattern.Match(text));
        }

        [Fact]
        public void MatchShoudReturnFalseWhenStringIsNull()
        {
            Character pattern = new ('a');
            const string text = null;
            Assert.False(pattern.Match(text));
        }

        [Fact]
        public void MatchShoudReturnFalseWhenStringIsEmpty()
        {
            Character pattern = new ('a');
            const string text = "";
            Assert.False(pattern.Match(text));
        }
    }
}