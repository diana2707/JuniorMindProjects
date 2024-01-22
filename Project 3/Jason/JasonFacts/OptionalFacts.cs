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
            Assert.True(a.Match("abc").Success());
            Assert.Equal("bc", a.Match("abc").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternRepeats()
        {
            var a = new Optional(new Character('a'));
            Assert.True(a.Match("aabc").Success());
            Assert.Equal("abc", a.Match("aabc").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternDoNotMatch()
        {
            var a = new Optional(new Character('a'));
            Assert.True(a.Match("bc").Success());
            Assert.Equal("bc", a.Match("bc").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenReceivesEmptyString()
        {
            var a = new Optional(new Character('a'));
            Assert.True(a.Match("").Success());
            Assert.Equal("", a.Match("").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndNullgWhenReceivesNull()
        {
            var a = new Optional(new Character('a'));
            Assert.True(a.Match(null).Success());
            Assert.Null(a.Match(null).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternIsASignCharacter()
        {
            var a = new Optional(new Character('-'));
            Assert.True(a.Match("123").Success());
            Assert.Equal("123", a.Match("123").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainigTextWhenPatternIsASignCharacter()
        {
            var a = new Optional(new Character('-'));
            Assert.True(a.Match("-123").Success());
            Assert.Equal("123", a.Match("-123").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainigTextWhenPatternIsASequence()
        {
            var a = new Optional(new Sequence(
                new Character('/'),
                new Range('1', '5'),
                new Choice(new Character('@'), new Character('#')),
                new Text("key")));

            Assert.True(a.Match("/5@keyone").Success());
            Assert.Equal("one", a.Match("/5@keyone").RemainingText());
        }
    }
}
