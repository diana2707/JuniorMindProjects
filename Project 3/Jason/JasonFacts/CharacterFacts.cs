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

            Assert.Equivalent(match, pattern.Match(text));
        }

        [Fact]
        public void MatchShoudReturnFalseWhenPatternIsInvalid()
        {
            Character pattern = new ('a');
            const string text = "bc";
            Match match = new (text, false);

            Assert.Equivalent(match, pattern.Match(text));
        }

        [Fact]
        public void MatchShoudReturnFalseWhenStringIsNull()
        {
            Character pattern = new ('a');
            const string text = null;
            Match match = new (text, false);

            Assert.Equivalent(match, pattern.Match(text));
        }

        [Fact]
        public void MatchShoudReturnFalseWhenStringIsEmpty()
        {
            Character pattern = new ('a');
            const string text = "";
            Match match = new (text, false);

            Assert.Equivalent(match, pattern.Match(text));
        }
    }
}
