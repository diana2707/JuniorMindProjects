using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class DisplayerFacts
    {
        [Fact]
        public void DisplayerObjectExists()
        {
            FakeConsoleController controller = new();
            var displayer = new Displayer(controller);
            Assert.IsType<Displayer>(displayer);
        }

        [Fact]
        public void DisplayLineMethodWritesLineInTextWriter()
        {
            FakeConsoleController controller = new();
            var displayer = new Displayer(controller);

            displayer.DisplayLine("test");
            Assert.Equal(["test\r\n"], controller.WrittenLines);
        }

        [Fact]
        public void DisplayTextMethodWritesMultipleLinesInTextWriter()
        {
            FakeConsoleController controller = new();
            var displayer = new Displayer(controller);
            List<string> input = ["a", "b", "c"];

            displayer.DisplayText(input);
            Assert.Equal(["a\r\n", "b\r\n", "c\r\n"], controller.WrittenLines);
        }

        [Fact]
        public void DisplayEmptyLineMethodWritesMultipleLinesInTextWriter()
        {
            FakeConsoleController controller = new();
            var displayer = new Displayer(controller);

            displayer.DisplayEmptyLine();
            Assert.True(controller.IsWriteEmptyLineCalled);
        }
    }
}
