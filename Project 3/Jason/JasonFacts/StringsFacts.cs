using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class StringsFacts
    {
        public static string Quoted(string text)
        {
            return $"\"{text}\"";
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenStringIsWrappedInDoubleQuotes()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted("abc"), 0);
            StringView failedIndex = new (Quoted("abc"), 0);

            Assert.True(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenStringMissesStartingQuote()
        {
            var s = new Strings();
            StringView currentIndex = new ("abc\"", 0);
            StringView failedIndex = new ("abc\"", 0);

            Assert.False(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('a', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('a', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenStringMissesEndingQuote()
        {
            var s = new Strings();
            StringView currentIndex = new ("\"abc", 0);
            StringView failedIndex = new ("\"abc", 0);

            Assert.False(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenTextIsNull()
        {
            var s = new Strings();
            StringView currentIndex = new (null, 0);
            StringView failedIndex = new (null, 0);

            Assert.False(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenTextIsEmptyString()
        {
            var s = new Strings();
            StringView currentIndex = new ("", 0);
            StringView failedIndex = new ("", 0);

            Assert.False(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenTextIsQuotedEmptyString()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted(""), 0);
            StringView failedIndex = new (Quoted(""), 0);

            Assert.True(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenContainsControlCharacters()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted("a\nb\rc"), 0);
            StringView failedIndex = new (Quoted("a\nb\rc"), 0);
            Assert.False(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\n', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedQuotationMarks()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted(@"\""a\"" b"), 0);
            StringView failedIndex = new (Quoted(@"\""a\"" b"), 0);

            Assert.True(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedBackslash()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted(@"a \\ b"), 0);
            StringView failedIndex = new (Quoted(@"a \\ b"), 0);

            Assert.True(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsLargeUnicodeCharacters()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted("⛅⚾"), 0);
            StringView failedIndex = new (Quoted("⛅⚾"), 0);

            Assert.True(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedSlash()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted(@"a \/ b"), 0);
            StringView failedIndex = new (Quoted(@"a \/ b"), 0);

            Assert.True(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedBackspace()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted(@"a \b b"), 0);
            StringView failedIndex = new (Quoted(@"a \b b"), 0);

            Assert.True(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedLinefeed()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted(@"a \n b"), 0);
            StringView failedIndex = new (Quoted(@"a \n b"), 0);

            Assert.True(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedCarrigeReturn()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted(@"a \r b"), 0);
            StringView failedIndex = new (Quoted(@"a \r b"), 0);

            Assert.True(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedHorizontalTab()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted(@"a \t b"), 0);
            StringView failedIndex = new (Quoted(@"a \t b"), 0);

            Assert.True(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedUnicodeCharacters()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted(@"a \u26Be b"), 0);
            StringView failedIndex = new (Quoted(@"a \u26Be b"), 0);

            Assert.True(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsAnyMultipleEscapedSequences()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted(@"\\\u1212\n\t\r\\\b"), 0);
            StringView failedIndex = new (Quoted(@"\\\u1212\n\t\r\\\b"), 0);

            Assert.True(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenContainsUnrecognisedEscapedCharacters()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted(@"a\x"), 0);
            StringView failedIndex = new (Quoted(@"a\x"), 0);

            Assert.False(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('x', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenTextEndsWithBackslash()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted(@"a\"), 0);
            StringView failedIndex = new (Quoted(@"a\"), 0);

            Assert.False(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenTextEndsWithUnfinishedHexNumber()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted(@"a\u123"), 0);
            StringView failedIndex = new (Quoted(@"a\u123"), 0);

            Assert.False(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextContainsFinishedAndUnfinishedHexSequences()
        {
            var s = new Strings();
            StringView currentIndex = new (Quoted(@"\u2634 \u11"), 0);
            StringView failedIndex = new (Quoted(@"\u2634 \u11"), 0);

            Assert.False(s.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\"', s.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }
    }
}
