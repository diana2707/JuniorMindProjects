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

            StringView stringView = new ("012", 0);

            Assert.True(digit.Match(stringView).Success());
            Assert.Equal('1', digit.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueWhenStringRanges1To9()
        {
            var digit = new Choice(
                    new Character('0'),
                    new Range('1', '9'));

            StringView stringView = new ("92", 0);

            Assert.True(digit.Match(stringView).Success());
            Assert.Equal('2', digit.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseWhenStringContainsLetters()
        {
            var digit = new Choice(
                    new Character('0'),
                    new Range('1', '9'));

            StringView stringView = new ("a34", 0);

            Assert.False(digit.Match(stringView).Success());
            Assert.Equal('a', digit.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseWhenStringIsNull()
        {
            var digit = new Choice(
                    new Character('0'),
                    new Range('1', '9'));

            StringView stringView = new (null, 0);

            Assert.False(digit.Match(stringView).Success());
            Assert.Equal('\0', digit.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseWhenStringIsEmpty()
        {
            var digit = new Choice(
                    new Character('0'),
                    new Range('1', '9'));

            StringView stringView = new ("", 0);

            Assert.False(digit.Match(stringView).Success());
            Assert.Equal(0, digit.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldAddPatternToChoice()
        {
            var choice = new Choice(
                new Character('a'),
                new Character('b'));

            var pattern = new Character('c');
            choice.Add(pattern);

            StringView stringView = new ("cde", 0);

            Assert.True(choice.Match(stringView).Success());
            Assert.Equal('d', choice.Match(stringView).StringView().Peek());
        }
    }
}