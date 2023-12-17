using Jason.Jason;
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

            const string text = "012";

            Assert.True(digit.Match(text).Success());
            Assert.Equal("12", digit.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueWhenStringRanges1To9()
        {
            var digit = new Choice(
                    new Character('0'),
                    new Range('1', '9'));

            const string text = "92";

            Assert.True(digit.Match(text).Success());
            Assert.Equal("2", digit.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseWhenStringContainsLetters()
        {
            var digit = new Choice(
                    new Character('0'),
                    new Range('1', '9'));

            const string text = "a34";

            Assert.False(digit.Match(text).Success());
            Assert.Equal(text, digit.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseWhenStringIsNull()
        {
            var digit = new Choice(
                    new Character('0'),
                    new Range('1', '9'));

            const string text = null;

            Assert.False(digit.Match(null).Success());
            Assert.Equal(text, digit.Match(text).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseWhenStringIsEmpty()
        {
            var digit = new Choice(
                    new Character('0'),
                    new Range('1', '9'));

            const string text = "";

            Assert.False(digit.Match(text).Success());
            Assert.Equal(text, digit.Match(text).RemainingText());
        }
    }
}