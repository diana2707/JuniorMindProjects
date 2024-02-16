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
            Assert.True(s.Match(Quoted("abc")).Success());
            Assert.Equal("", s.Match(Quoted("abc")).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenStringMissesStartingQuote()
        {
            var s = new Strings();
            Assert.False(s.Match("abc\"").Success());
            Assert.Equal("abc\"", s.Match("abc\"").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenStringMissesEndingQuote()
        {
            var s = new Strings();
            Assert.False(s.Match("\"abc").Success());
            Assert.Equal("\"abc", s.Match("\"abc").RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndNullWhenTextIsNull()
        {
            var s = new Strings();
            Assert.False(s.Match(null).Success());
            Assert.Null(s.Match(null).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndEmptyStringWhenTextIsEmptyString()
        {
            var s = new Strings();
            Assert.False(s.Match("").Success());
            Assert.Equal("", s.Match("").RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenTextIsQuotedEmptyString()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted("")).Success());
            Assert.Equal("", s.Match(Quoted("")).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenContainsControlCharacters()
        {
            var s = new Strings();
            Assert.False(s.Match(Quoted("a\nb\rc")).Success());
            Assert.Equal(Quoted("a\nb\rc"), s.Match(Quoted("a\nb\rc")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedQuotationMarks()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted(@"\""a\"" b")).Success());
            Assert.Equal("", s.Match(Quoted(@"\""a\"" b")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedBackslash()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted(@"a \\ b")).Success());
            Assert.Equal("", s.Match(Quoted(@"a \\ b")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsLargeUnicodeCharacters()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted("⛅⚾")).Success());
            Assert.Equal("", s.Match(Quoted("⛅⚾")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedSlash()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted(@"a \/ b")).Success());
            Assert.Equal("", s.Match(Quoted(@"a \/ b")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedBackspace()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted(@"a \b b")).Success());
            Assert.Equal("", s.Match(Quoted(@"a \b b")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedLinefeed()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted(@"a \n b")).Success());
            Assert.Equal("", s.Match(Quoted(@"a \n b")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedCarrigeReturn()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted(@"a \r b")).Success());
            Assert.Equal("", s.Match(Quoted(@"a \r b")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedHorizontalTab()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted(@"a \t b")).Success());
            Assert.Equal("", s.Match(Quoted(@"a \t b")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsEscapedUnicodeCharacters()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted(@"a \u26Be b")).Success());
            Assert.Equal("", s.Match(Quoted(@"a \u26Bet b")).RemainingText());
        }

        [Fact]
        public void ShouldReturnTrueAndEmptyStringWhenContainsAnyMultipleEscapedSequences()
        {
            var s = new Strings();
            Assert.True(s.Match(Quoted(@"\\\u1212\n\t\r\\\b")).Success());
            Assert.Equal("", s.Match(Quoted(@"\\\u1212\n\t\r\\\b")).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenContainsUnrecognisedEscapedCharacters()
        {
            var s = new Strings();
            Assert.False(s.Match(Quoted(@"a\x")).Success());
            Assert.Equal(Quoted(@"a\x"), s.Match(Quoted(@"a\x")).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenTextEndsWithBackslash()
        {
            var s = new Strings();
            Assert.False(s.Match(Quoted(@"a\")).Success());
            Assert.Equal(Quoted(@"a\"), s.Match(Quoted(@"a\")).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextWhenTextEndsWithUnfinishedHexNumber()
        {
            var s = new Strings();
            Assert.False(s.Match(Quoted(@"a\u123")).Success());
            Assert.Equal(Quoted(@"a\u123"), s.Match(Quoted(@"a\u123")).RemainingText());
        }

        [Fact]
        public void ShouldReturnFalseAndWholeTextContainsFinishedAndUnfinishedHexSequences()
        {
            var s = new Strings();
            Assert.False(s.Match(Quoted(@"\u2634 \u11")).Success());
            Assert.Equal(Quoted(@"\u2634 \u11"), s.Match(Quoted(@"\u2634 \u11")).RemainingText());
        }
    }
}
