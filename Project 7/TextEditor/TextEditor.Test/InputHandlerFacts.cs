using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class InputHandlerFacts
    {
        [Fact]
        public void InputHandlerClassExists()
        {
            FakeConsoleController controller = new();
            InputHandler inputHandler = new(controller);
            Assert.IsType<InputHandler>(inputHandler);
        }

        [Fact]
        public void ReadLineInputMethodReadsSingleLine()
        {
            FakeConsoleController controller = new();
            InputHandler inputHandler = new(controller);

            controller.LineToRead = "test";

            Assert.Equal("test", inputHandler.ReadInputLine());
        }
    }
}
