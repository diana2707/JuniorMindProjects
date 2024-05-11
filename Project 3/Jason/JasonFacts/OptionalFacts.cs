using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class OptionalFacts
    {
        [Fact]
        public void ShouldReturnTrueAndRemainingText()
        {
            var a = new Optional(new Character('a'));
            StringView stringView = new ("abc", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('b', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternRepeats()
        {
            var a = new Optional(new Character('a'));
            StringView stringView = new ("aabc", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('a', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternDoNotMatch()
        {
            var a = new Optional(new Character('a'));
            StringView stringView = new ("bc", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('b', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenReceivesEmptyString()
        {
            var a = new Optional(new Character('a'));
            StringView stringView = new ("", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('\0', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndNullgWhenReceivesNull()
        {
            var a = new Optional(new Character('a'));
            StringView stringView = new (null, 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('\0', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternIsASignCharacter()
        {
            var a = new Optional(new Character('-'));
            StringView stringView = new ("123", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('1', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainigTextWhenPatternIsASignCharacter()
        {
            var a = new Optional(new Character('-'));
            StringView stringView = new ("-123", 0);
            Assert.True(a.Match(stringView).Success());
            Assert.Equal('1', a.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainigTextWhenPatternIsASequence()
        {
            var a = new Optional(new Sequence(
                new Character('/'),
                new Range('1', '5'),
                new Choice(new Character('@'), new Character('#')),
                new Text("key")));

            StringView stringView = new ("/5@keyone", 0);

            Assert.True(a.Match(stringView).Success());
            Assert.Equal('o', a.Match(stringView).StringView().Peek());
        }
    }
}
