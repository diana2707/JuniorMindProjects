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
    }
}
