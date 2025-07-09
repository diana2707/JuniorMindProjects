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

        [Fact]
        public void ClearMethodClearsTheDisplay()
        {
            FakeConsoleController controller = new();
            var displayer = new Displayer(controller);

            displayer.ClearDisplay();
            Assert.True(controller.IsClearCalled);
        }

        [Fact]
        public void DisplayCursorAtMovesTheCursorAtGivenCoordinates()
        {
            FakeConsoleController controller = new();
            var displayer = new Displayer(controller);

            displayer.DisplayCursorAt((5, 7));
            Assert.Equal((5, 7), controller.CursorPosition);
        }

        [Fact]
        public void ClearFromMethodPrintsEmptySpaceStartingFromPosition()
        {
            FakeConsoleController controller = new();
            var displayer = new Displayer(controller);

            displayer.DisplayText(["test1", "test2"]);
            displayer.ClearFrom(1);

            List<string> result = ["test1\r\n"];

            for (int i = 0; i < controller.Height - 2; i++)
            {
                result.Add(new string(' ', controller.Width) + "\r\n");
            }

            List<string> lines = controller.WrittenLines;

            Assert.Equal(result, controller.WrittenLines);
        }

        [Fact]
        public void OnViewportMovedSetsCursorAtStartOfViewportAndDisplaysText()
        {
            FakeConsoleController controller = new();
            var displayer = new Displayer(controller);

            displayer.OnViewportMoved(["test", "test"], (0, 3));
            List<string> result = [];

            for (int i = 0; i < 3; i++)
            {
                result.Add(new string(' ', controller.Width) + "\r\n");
            }

            result.AddRange(["test\r\n", "test\r\n"]);

            for (int i = 5; i < controller.Height - 2; i++)
            {
                result.Add(new string(' ', controller.Width) + "\r\n");
            }

            List<string> lines = controller.WrittenLines;

            Assert.Equal((0, 5), controller.CursorPosition);
            Assert.Equal(result, controller.WrittenLines);
        }
    }
}
