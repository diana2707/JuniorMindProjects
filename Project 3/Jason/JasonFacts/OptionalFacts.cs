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
            StringView currentIndex = new ("abc", 0);
            StringView failedIndex = new ("abc", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('b', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('a', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternRepeats()
        {
            var a = new Optional(new Character('a'));
            StringView currentIndex = new ("aabc", 0);
            StringView failedIndex = new ("aabc", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('a', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternDoNotMatch()
        {
            var a = new Optional(new Character('a'));
            StringView currentIndex = new ("bc", 0);
            StringView failedIndex = new ("bc", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('b', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('b', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenReceivesEmptyString()
        {
            var a = new Optional(new Character('a'));
            StringView currentIndex = new ("", 0);
            StringView failedIndex = new ("", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndNullgWhenReceivesNull()
        {
            var a = new Optional(new Character('a'));
            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPatternIsASignCharacter()
        {
            var a = new Optional(new Character('-'));
            StringView currentIndex = new ("123", 0);
            StringView failedIndex = new ("123", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('1', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('1', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainigTextWhenPatternIsASignCharacter()
        {
            var a = new Optional(new Character('-'));
            StringView currentIndex = new ("-123", 0);
            StringView failedIndex = new ("-123", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('1', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('-', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainigTextWhenPatternIsASequence()
        {
            var a = new Optional(new Sequence(
                new Character('/'),
                new Range('1', '5'),
                new Choice(new Character('@'), new Character('#')),
                new Text("key")));

            StringView currentIndex = new ("/5@keyone", 0);
            StringView failedIndex = new ("/5@keyone", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('o', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('/', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }
    }
}
