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

            StringView currentIndex = new ("012", 0);
            StringView failedIndex = new ("012", 0);

            Assert.True(digit.Match(currentIndex, failedIndex).Success());
            Assert.Equal('1', digit.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('0', digit.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueWhenStringRanges1To9()
        {
            var digit = new Choice(
                    new Character('0'),
                    new Range('1', '9'));

            StringView currentIndex = new ("92", 0);
            StringView failedIndex = new ("92", 0);

            Assert.True(digit.Match(currentIndex, failedIndex).Success());
            Assert.Equal('2', digit.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('9', digit.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseWhenStringContainsLetters()
        {
            var digit = new Choice(
                    new Character('0'),
                    new Range('1', '9'));

            StringView currentIndex = new ("a34", 0);
            StringView failedIndex = new ("a34", 0);

            Assert.False(digit.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', digit.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('a', digit.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseWhenStringIsNull()
        {
            var digit = new Choice(
                    new Character('0'),
                    new Range('1', '9'));

            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);

            Assert.False(digit.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', digit.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', digit.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseWhenStringIsEmpty()
        {
            var digit = new Choice(
                    new Character('0'),
                    new Range('1', '9'));

            StringView currentIndex = new ("", 0);
            StringView failedIndex = new ("", 0);

            Assert.False(digit.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', digit.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', digit.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldAddPatternToChoice()
        {
            var choice = new Choice(
                new Character('a'),
                new Character('b'));

            var pattern = new Character('c');
            choice.Add(pattern);

            StringView currentIndex = new ("cde", 0);
            StringView failedIndex = new ("cde", 0);

            Assert.True(choice.Match(currentIndex, failedIndex).Success());
            Assert.Equal('d', choice.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('c', choice.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }
    }
}