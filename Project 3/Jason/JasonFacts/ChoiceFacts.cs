using Xunit;

namespace Jason.JasonFacts
{
    public class ChoiceFacts
    {
        [Fact]
        public void ShouldReturnTrueWhenStringStartsWith0()
        {
            var digit = new Choice(
                    new Character('0'),
                    new Range('1', '9'));

            Assert.True(digit.Match("012"));
        }

        [Fact]
        public void ShouldReturnTrueWhenStringRanges1To9()
        {
            var digit = new Choice(
                    new Character('0'),
                    new Range('1', '9'));

            Assert.True(digit.Match("92"));
            Assert.True(digit.Match("2378"));
        }

        [Fact]
        public void ShouldReturnFalseWhenStringContainsLetters()
        {
            var digit = new Choice(
                    new Character('0'),
                    new Range('1', '9'));

            Assert.False(digit.Match("a34"));
        }

    }
}
