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

        [Fact]
        public void ReadInputKeyMethodReadsSingleKey()
        {
            FakeConsoleController controller = new();
            InputHandler inputHandler = new(controller);

            ConsoleKeyInfo inputKey = new ('\a', ConsoleKey.A, false, false, false);

            controller.KeyToRead = inputKey;

            Assert.Equal(inputKey, inputHandler.ReadInputKey());
        }
    }
}
