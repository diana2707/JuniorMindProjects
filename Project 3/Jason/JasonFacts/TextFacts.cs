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
            StringView stringView = new ("true", 0);
            Assert.True(trueText.Match(stringView).Success());
            Assert.Equal('\0', trueText.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenPrefixIsValid()
        {
            var trueText = new Text("true");
            StringView stringView = new ("trueX", 0);
            Assert.True(trueText.Match(stringView).Success());
            Assert.Equal('X', trueText.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenPrefixIsInvalid()
        {
            var trueText = new Text("true");
            StringView stringView = new ("false", 0);
            Assert.False(trueText.Match(stringView).Success());
            Assert.Equal('f', trueText.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenStringIsEmptyString()
        {
            var trueText = new Text("true");
            StringView stringView = new ("", 0);
            Assert.False(trueText.Match(stringView).Success());
            Assert.Equal('\0', trueText.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenStringIsNull()
        {
            var trueText = new Text("true");
            StringView stringView = new (null, 0);
            Assert.False(trueText.Match(stringView).Success());
            Assert.Equal('\0', trueText.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyString()
        {
            var falseText = new Text("false");
            StringView stringView = new ("false", 0);
            Assert.True(falseText.Match(stringView).Success());
            Assert.Equal('\0', falseText.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingText()
        {
            var falseText = new Text("false");
            StringView stringView = new ("falseX", 0);
            Assert.True(falseText.Match(stringView).Success());
            Assert.Equal('X', falseText.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeText()
        {
            var falseText = new Text("false");
            StringView stringView = new ("true", 0);
            Assert.False(falseText.Match(stringView).Success());
            Assert.Equal('t', falseText.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyString()
        {
            var falseText = new Text("false");
            StringView stringView = new ("", 0);
            Assert.False(falseText.Match(stringView).Success());
            Assert.Equal('\0', falseText.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNull()
        {
            var falseText = new Text("false");
            StringView stringView = new (null, 0);
            Assert.False(falseText.Match(stringView).Success());
            Assert.Equal('\0', falseText.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndWholeTextWhenPrefixIsEmptyString()
        {
            var empty = new Text("");
            StringView stringView = new ("true", 0);
            Assert.True(empty.Match(stringView).Success());
            Assert.Equal('t', empty.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenPrefixIsEmptyStringAndTextIsNull()
        {
            var empty = new Text("");
            StringView stringView = new (null, 0);
            Assert.False(empty.Match(stringView).Success());
            Assert.Equal('\0', empty.Match(stringView).StringView().Peek());
        }
    }
}
