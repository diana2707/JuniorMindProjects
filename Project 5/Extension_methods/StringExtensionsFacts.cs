using System;

namespace Extension_methods
{
    public class StringExtensionsFacts
    {
        [Fact]
        public void CountVowelsReturnsNumberOfVowelsInString()
        {
            string source = "Alexandra";
            Assert.Equal(4, source.CountVowels());
        }

        [Fact]
        public void CountConsonantsReturnsNumberOfVowelsInString()
        {
            string source = "Alexandra";
            Assert.Equal(5, source.CountConsonants());
        }

        [Fact]
        public void FirstUniqueReturnsFirstUniqueCharInString()
        {
            string source = "ababacd";
            Assert.Equal('c', source.FirstUnique());
        }

        [Fact]
        public void ConvertToIntReturnsIntFromStringForUnits()
        {
            string source = "5";
            Assert.Equal(5, source.ConvertToInt());
        }
    }
}
