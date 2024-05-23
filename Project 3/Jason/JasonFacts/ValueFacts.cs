using Jason.Jason;
using Xunit;

namespace Jason.JasonFacts
{
    public class ValueFacts
    {
        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsString()
        {
            var value = new Value();
            StringView currentIndex = new (" \"abc\" ", 0);
            StringView failedIndex = new (" \"abc\" ", 0);

            Assert.True(value.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsNumber()
        {
            var value = new Value();
            StringView currentIndex = new (" 12 ", 0);
            StringView failedIndex = new (" 12 ", 0);

            Assert.True(value.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsEmptyArray()
        {
            var value = new Value();
            StringView currentIndex = new (" [ ] ", 0);
            StringView failedIndex = new (" [ ] ", 0);

            Assert.True(value.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsAnArrayOfNumbers()
        {
            var value = new Value();
            StringView currentIndex = new (" [ 1 , 2 , 3 ] ", 0);
            StringView failedIndex = new (" [ 1 , 2 , 3 ] ", 0);

            Assert.True(value.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsAnArrayOfStrings()
        {
            var value = new Value();
            StringView currentIndex = new (" [ \"a\" , \"b\" , \"c\" ] ", 0);
            StringView failedIndex = new (" [ \"a\" , \"b\" , \"c\" ] ", 0);

            Assert.True(value.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsAnArrayOfBoolean()
        {
            var value = new Value();
            StringView currentIndex = new (" [ true ] ", 0);
            StringView failedIndex = new (" [ true ] ", 0);

            Assert.True(value.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsAnArrayOfArrays()
        {
            var value = new Value();
            StringView currentIndex = new (" [ [ 1 ] , [ 2 ] , [ 3 , 4 ] ] ", 0);
            StringView failedIndex = new (" [ [ 1 ] , [ 2 ] , [ 3 , 4 ] ] ", 0);

            Assert.True(value.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsEmptyObject()
        {
            var value = new Value();
            StringView currentIndex = new (" { } ", 0);
            StringView failedIndex = new (" { } ", 0);

            Assert.True(value.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsObjectWithKeyValues()
        {
            var value = new Value();

            const string obj = " { \"Name\" : \"Ana\" , \"Age\" : 25 , \"Married\" : true , " +
                "\"Children\" : [ \"Andreea\" , \"Ioana\" ] , \"Pets\" : { \"Cat\" : \"Kitty\" , \"Dog\" : \"Marley\" } }";

            StringView currentIndex = new (obj, 0);
            StringView failedIndex = new (obj, 0);

            Assert.True(value.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnFalseAndRemainingTextWhenValueIsInvalidObject()
        {
            var value = new Value();

            const string obj = " { \"Name\" : \"Ana\" , \"Age\" : 25 , \"Married\" : true , " +
                "\"Children\"  [ \"Andreea\" , \"Ioana\" ] , \"Pets\" : { \"Cat\" : \"Kitty\" , \"Dog\" : \"Marley\" } }";

            StringView currentIndex = new (obj, 0);
            StringView failedIndex = new (obj, 0);

            Assert.False(value.Match(currentIndex, failedIndex).Success());
            Assert.Equal(' ', value.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('[', value.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }

        [Fact]
        public void ShouldReturnTrueAndRemainingTextWhenValueIsObjectWithKeyValuesOver690Characters()
        {
            var value = new Value();

            const string obj = "{\r\n    \"glossary\": {\r\n        \"title\": \"example glossary\",\r\n\t\t\"GlossDiv\": {\r\n            \"title\": \"S\",\r\n\t\t\t" +
                "\"GlossList\": {\r\n                \"GlossEntry\": {\r\n                    \"ID\": \"SGML\",\r\n\t\t\t\t\t\"SortAs\": \"SGML\",\r\n\t\t\t\t\t\"" +
                "GlossTerm\": \"Standard Generalized Markup Language\",\r\n\t\t\t\t\t\"Acronym\": \"SGML\",\r\n\t\t\t\t\t\"Abbrev\": \"ISO 8879:1986\",\r\n\t\t\t\t\t\"" +
                "GlossDef\": {\r\n                        \"para\": \"A meta-markup language, used to create markup languages such as DocBook.\",\r\n\t\t\t\t\t\t\"" +
                "GlossSeeAlso\": [\"GML\", \"XML\"]\r\n                    },\r\n\t\t\t\t\t\"GlossSee\": \"markup\"\r\n                }\r\n            }\r\n        }\r\n    }\r\n}";

            StringView currentIndex = new (obj, 0);
            StringView failedIndex = new (obj, 0);

            Assert.True(value.Match(currentIndex, failedIndex).Success());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).CurrentIndex().Peek());
            Assert.Equal('\0', value.Match(currentIndex, failedIndex).FailedIndex().Peek());
        }
    }
}