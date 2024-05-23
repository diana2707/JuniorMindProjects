using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class OneOrMoreFacts
    {
        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenPatternDoesNotMatch()
        {
            var a = new OneOrMore(new Range('0', '9'));
            StringView currentIndex = new ("bc", 0);
            StringView failedIndex = new ("bc", 0);

            Assert.False(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('b', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('b', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenPatternConsumesWholeText()
        {
            var a = new OneOrMore(new Range('0', '9'));
            StringView currentIndex = new ("123", 0);
            StringView failedIndex = new ("123", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternIsConsumed()
        {
            var a = new OneOrMore(new Range('0', '9'));
            StringView currentIndex = new ("12a", 0);
            StringView failedIndex = new ("12a", 0);

            Assert.True(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('a', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenTextIsEmptyString()
        {
            var a = new OneOrMore(new Range('0', '9'));
            StringView currentIndex = new ("", 0);
            StringView failedIndex = new ("", 0);

            Assert.False(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenTextIsNull()
        {
            var a = new OneOrMore(new Range('0', '9'));
            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);

            Assert.False(a.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', a.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }
    }
}
