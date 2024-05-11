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
            StringView stringView = new (Quoted("abc"), 0);
            Assert.True(s.Match(stringView).Success());
            Assert.Equal('\0', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenStringMissesStartingQuote()
        {
            var s = new Strings();
            StringView stringView = new ("abc\"", 0);
            Assert.False(s.Match(stringView).Success());
            Assert.Equal('a', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenStringMissesEndingQuote()
        {
            var s = new Strings();
            StringView stringView = new ("\"abc", 0);
            Assert.False(s.Match(stringView).Success());
            Assert.Equal('\"', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenTextIsNull()
        {
            var s = new Strings();
            StringView stringView = new (null, 0);
            Assert.False(s.Match(stringView).Success());
            Assert.Equal('\0', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenTextIsEmptyString()
        {
            var s = new Strings();
            StringView stringView = new ("", 0);
            Assert.False(s.Match(stringView).Success());
            Assert.Equal('\0', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenTextIsQuotedEmptyString()
        {
            var s = new Strings();
            StringView stringView = new (Quoted(""), 0);
            Assert.True(s.Match(stringView).Success());
            Assert.Equal('\0', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenContainsControlCharacters()
        {
            var s = new Strings();
            StringView stringView = new (Quoted("a\nb\rc"), 0);
            Assert.False(s.Match(stringView).Success());
            Assert.Equal('\"', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedQuotationMarks()
        {
            var s = new Strings();
            StringView stringView = new (Quoted(@"\""a\"" b"), 0);
            Assert.True(s.Match(stringView).Success());
            Assert.Equal('\0', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedBackslash()
        {
            var s = new Strings();
            StringView stringView = new (Quoted(@"a \\ b"), 0);
            Assert.True(s.Match(stringView).Success());
            Assert.Equal('\0', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsLargeUnicodeCharacters()
        {
            var s = new Strings();
            StringView stringView = new (Quoted("⛅⚾"), 0);
            Assert.True(s.Match(stringView).Success());
            Assert.Equal('\0', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedSlash()
        {
            var s = new Strings();
            StringView stringView = new (Quoted(@"a \/ b"), 0);
            Assert.True(s.Match(stringView).Success());
            Assert.Equal('\0', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedBackspace()
        {
            var s = new Strings();
            StringView stringView = new (Quoted(@"a \b b"), 0);
            Assert.True(s.Match(stringView).Success());
            Assert.Equal('\0', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedLinefeed()
        {
            var s = new Strings();
            StringView stringView = new (Quoted(@"a \n b"), 0);
            Assert.True(s.Match(stringView).Success());
            Assert.Equal('\0', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedCarrigeReturn()
        {
            var s = new Strings();
            StringView stringView = new (Quoted(@"a \r b"), 0);
            Assert.True(s.Match(stringView).Success());
            Assert.Equal('\0', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedHorizontalTab()
        {
            var s = new Strings();
            StringView stringView = new (Quoted(@"a \t b"), 0);
            Assert.True(s.Match(stringView).Success());
            Assert.Equal('\0', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedUnicodeCharacters()
        {
            var s = new Strings();
            StringView stringView = new (Quoted(@"a \u26Be b"), 0);
            Assert.True(s.Match(stringView).Success());
            Assert.Equal('\0', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsAnyMultipleEscapedSequences()
        {
            var s = new Strings();
            StringView stringView = new (Quoted(@"\\\u1212\n\t\r\\\b"), 0);
            Assert.True(s.Match(stringView).Success());
            Assert.Equal('\0', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenContainsUnrecognisedEscapedCharacters()
        {
            var s = new Strings();
            StringView stringView = new (Quoted(@"a\x"), 0);
            Assert.False(s.Match(stringView).Success());
            Assert.Equal('\"', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenTextEndsWithBackslash()
        {
            var s = new Strings();
            StringView stringView = new (Quoted(@"a\"), 0);
            Assert.False(s.Match(stringView).Success());
            Assert.Equal('\"', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenTextEndsWithUnfinishedHexNumber()
        {
            var s = new Strings();
            StringView stringView = new (Quoted(@"a\u123"), 0);
            Assert.False(s.Match(stringView).Success());
            Assert.Equal('\"', s.Match(stringView).StringView().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextContainsFinishedAndUnfinishedHexSequences()
        {
            var s = new Strings();
            StringView stringView = new (Quoted(@"\u2634 \u11"), 0);
            Assert.False(s.Match(stringView).Success());
            Assert.Equal('\"', s.Match(stringView).StringView().Peek());
        }
    }
}
