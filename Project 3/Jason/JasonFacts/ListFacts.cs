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
            StringView stringView = new ("1,2,3", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('\0', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndSeparatorWhenTextEndsWithSeparator()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            StringView stringView = new ("1,2,3,", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal(',', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemaininText()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            StringView stringView = new ("1a", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('a', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenThePatternDoesNotMatch()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            StringView stringView = new ("abc", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('a', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenTheTextIsEmptyString()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            StringView stringView = new ("", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('\0', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndNullWhenTheTextIsNull()
        {
            var a = new List(new Range('0', '9'), new Character(','));
            StringView stringView = new (null, 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('\0', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenPatternIsConsumed()
        {
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), whitespace);
            var list = new List(digits, separator);

            StringView stringView = new ("1; 22  ;\n 333 \t; 22", 0);

            Assert.True(list.Match(stringView).Success());
            Assert.Equal('\0', list.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternIsConsumed()
        {
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), whitespace);
            var list = new List(digits, separator);

            StringView stringView = new ("1 \n;", 0);

            Assert.True(list.Match(stringView).Success());
            Assert.Equal(' ', list.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternDoNotMatch()
        {
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), whitespace);
            var list = new List(digits, separator);

            StringView stringView = new ("abc", 0);

            Assert.True(list.Match(stringView).Success());
            Assert.Equal('a', list.Match(stringView).StringView().Peek());
        }
    }
}
