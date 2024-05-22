using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class AnyFacts
    {
        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPatternMatches()
        {
            var e = new Any("eE");
            StringView currentIndex = new ("ea", 0);
            StringView failedIndex = new ("ea", 0);
            Assert.True(e.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', e.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingText()
        {
            var e = new Any("eE");
            StringView currentIndex = new ("Ea", 0);
            StringView failedIndex = new ("ea", 0);
            Assert.True(e.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', e.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenPatternDoesNotMatch()
        {
            var e = new Any("eE");
            StringView currentIndex = new ("a", 0);
            StringView failedIndex = new ("a", 0);
            Assert.False(e.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', e.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenTextIsEmptyString()
        {
            var e = new Any("eE");
            StringView currentIndex = new ("", 0);
            StringView failedIndex = new ("", 0);
            Assert.False(e.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', e.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenTextIsNull()
        {
            var e = new Any("eE");
            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);
            Assert.False(e.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', e.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenVerifyPositiveSign()
        {
            var sign = new Any("-+");
            StringView currentIndex = new ("+3", 0);
            StringView failedIndex = new ("+3", 0);
            Assert.True(sign.Match(currentIndex, failedIndex).Success());
            Assert.Equal('3', sign.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenVerifyNegativeSign()
        {
            var sign = new Any("-+");
            StringView currentIndex = new ("-2", 0);
            StringView failedIndex = new ("-2", 0);
            Assert.True(sign.Match(currentIndex, failedIndex).Success());
            Assert.Equal('2', sign.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenPatternDoesNotMatchForSigns()
        {
            var sign = new Any("-+");
            StringView currentIndex = new ("2", 0);
            StringView failedIndex = new ("2", 0);
            Assert.False(sign.Match(currentIndex, failedIndex).Success());
            Assert.Equal('2', sign.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenTheTextIsEmptyString()
        {
            var sign = new Any("-+");
            StringView currentIndex = new ("", 0);
            StringView failedIndex = new ("", 0);
            Assert.False(sign.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', sign.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenTheTextIsNull()
        {
            var sign = new Any("-+");
            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);
            Assert.False(sign.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', sign.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void MatchShouldReturnCorrectFailedIndexWhenIsNotLarger()
        {
            var num = new Any("12345");
            StringView currentIndex = new ("abc", 0);
            StringView failedIndex = new ("abc", 0);

            Assert.False(num.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', num.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void MatchShouldReturnCorrectFailedIndexWhenIsLarger()
        {
            var num = new Any("12345");
            StringView currentIndex = new ("acfdb", 0);
            StringView failedIndex = new ("acfdb", 3);

            Assert.False(num.Match(currentIndex, failedIndex).Success());
            Assert.Equal('d', num.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }
    }
}
