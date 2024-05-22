using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class TextFacts
    {
        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenTextIsTheSameAsPrefix()
        {
            var trueText = new Text("true");
            StringView currentIndex = new ("true", 0);
            StringView failedIndex = new ("true", 0);
            Assert.True(trueText.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', trueText.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPrefixIsValid()
        {
            var trueText = new Text("true");
            StringView currentIndex = new ("trueX", 0);
            StringView failedIndex = new ("trueX", 0);
            Assert.True(trueText.Match(currentIndex, failedIndex).Success());
            Assert.Equal('X', trueText.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenPrefixIsInvalid()
        {
            var trueText = new Text("true");
            StringView currentIndex = new ("false", 0);
            StringView failedIndex = new ("false", 0);
            Assert.False(trueText.Match(currentIndex, failedIndex).Success());
            Assert.Equal('f', trueText.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenStringIsEmptyString()
        {
            var trueText = new Text("true");
            StringView currentIndex = new ("", 0);
            StringView failedIndex = new ("", 0);
            Assert.False(trueText.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', trueText.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenStringIsNull()
        {
            var trueText = new Text("true");
            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);
            Assert.False(trueText.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', trueText.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyString()
        {
            var falseText = new Text("false");
            StringView currentIndex = new ("false", 0);
            StringView failedIndex = new ("false", 0);
            Assert.True(falseText.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', falseText.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingText()
        {
            var falseText = new Text("false");
            StringView currentIndex = new ("falseX", 0);
            StringView failedIndex = new ("falseX", 0);
            Assert.True(falseText.Match(currentIndex, failedIndex).Success());
            Assert.Equal('X', falseText.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeText()
        {
            var falseText = new Text("false");
            StringView currentIndex = new ("true", 0);
            StringView failedIndex = new ("true", 0);
            Assert.False(falseText.Match(currentIndex, failedIndex).Success());
            Assert.Equal('t', falseText.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyString()
        {
            var falseText = new Text("false");
            StringView currentIndex = new ("", 0);
            StringView failedIndex = new ("", 0);
            Assert.False(falseText.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', falseText.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNull()
        {
            var falseText = new Text("false");
            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);
            Assert.False(falseText.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', falseText.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPrefixIsEmptyString()
        {
            var empty = new Text("");
            StringView currentIndex = new ("true", 0);
            StringView failedIndex = new ("true", 0);
            Assert.True(empty.Match(currentIndex, failedIndex).Success());
            Assert.Equal('t', empty.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenPrefixIsEmptyStringAndTextIsNull()
        {
            var empty = new Text("");
            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);
            Assert.False(empty.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', empty.Match(currentIndex, failedIndex).CurrentIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFailedIndexWhenIndexIsNotLarger()
        {
            var trueText = new Text("true");
            StringView currentIndex = new ("abcd", 0);
            StringView failedIndex = new ("abcd", 0);

            Assert.False(trueText.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', trueText.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFailedIndexWhenIndexIsLarger()
        {
            var trueText = new Text("true");
            StringView currentIndex = new ("abcd", 0);
            StringView failedIndex = new ("abcd", 2);

            Assert.False(trueText.Match(currentIndex, failedIndex).Success());
            Assert.Equal('c', trueText.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }
    }
}
