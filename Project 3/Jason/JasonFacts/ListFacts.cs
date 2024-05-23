using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class ListFacts
    {
        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenPatternsConsumesWholeText()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            StringView currentIndex = new ("1,2,3", 0);
            StringView failedIndex = new ("1,2,3", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndSeparatorWhenTextEndsWithSeparator()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            StringView currentIndex = new ("1,2,", 0);
            StringView failedIndex = new ("1,2,", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal(',', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemaininText()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            StringView currentIndex = new ("1a", 0);
            StringView failedIndex = new ("1a", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('a', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenThePatternDoesNotMatch()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            StringView currentIndex = new ("abc", 0);
            StringView failedIndex = new ("abc", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('a', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenTheTextIsEmptyString()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            StringView currentIndex = new ("", 0);
            StringView failedIndex = new ("", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndNullWhenTheTextIsNull()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenPatternIsConsumed()
        {
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), whitespace);
            var list = new List(digits, separator);

            StringView currentIndex = new ("1; 22  ;\n 333 \t; 22", 0);
            StringView failedIndex = new ("1; 22  ;\n 333 \t; 22", 0);

            Assert.True(list.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', list.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', list.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternIsConsumed()
        {
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), whitespace);
            var list = new List(digits, separator);

            StringView currentIndex = new ("1 \n;", 0);
            StringView failedIndex = new ("1 \n;", 0);

            Assert.True(list.Match(currentIndex, failedIndex).Success());
            Assert.Equal(' ', list.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', list.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternDoNotMatch()
        {
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), whitespace);
            var list = new List(digits, separator);

            StringView currentIndex = new ("abc", 0);
            StringView failedIndex = new ("abc", 0);

            Assert.True(list.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', list.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('a', list.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }
    }
}
