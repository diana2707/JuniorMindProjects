using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class DisplayerFacts
    {
        [Fact]
        public void DisplayerObjectExists()
        {
            StringWriter writer = new();
            var displayer = new Displayer(writer);
            Assert.IsType<Displayer>(displayer);
        }

        [Fact]
        public void DisplayLineMethodWritesLineInTextWriter()
        {
            StringWriter writer = new();
            var displayer = new Displayer(writer);

            displayer.DisplayLine("test");
            Assert.Equal("test\r\n", writer.ToString());
        }

        [Fact]
        public void DisplayTextMethodWritesMultipleLinesInTextWriter()
        {
            StringWriter writer = new();
            var displayer = new Displayer(writer);
            List<string> input = ["a", "b", "c"];

            displayer.DisplayText(input);
            Assert.Equal("a\r\nb\r\nc\r\n", writer.ToString());
        }

        [Fact]
        public void DisplayEmptyLineMethodWritesMultipleLinesInTextWriter()
        {
            StringWriter writer = new();
            var displayer = new Displayer(writer);

            displayer.DisplayEmptyLine();
            Assert.Equal("\r\n", writer.ToString());
        }
    }
}
