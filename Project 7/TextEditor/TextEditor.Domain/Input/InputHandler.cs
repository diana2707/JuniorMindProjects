using System;
using TextEditor.Domain.Shared;

namespace TextEditor.Domain.Input
{
    public class InputHandler
    {
        private readonly IConsoleController controller;

        public InputHandler(IConsoleController controller)
        {
            this.controller = controller;
        }

        public event Action<Direction> ArrowKeyPressed;

        public event Action<char> CharacterKeyPressed;

        public event Action EscapeKeyPressed;

        public string? ReadInputLine()
        {
            return controller.ReadLine();
        }

        public ConsoleKeyInfo ReadKeyWithoutDisplay()
        {
            return controller.ReadKeyWithoutDisplay();
        }

        public void Listen()
        {
            bool isListening = true;

            while (isListening)
            {
                ConsoleKeyInfo currentKey = ReadKeyWithoutDisplay();

                switch (currentKey.Key)
                {
                    case ConsoleKey.RightArrow:
                        ArrowKeyPressed.Invoke(Direction.Right);
                        continue;

                    case ConsoleKey.LeftArrow:
                        ArrowKeyPressed.Invoke(Direction.Left);
                        continue;

                    case ConsoleKey.UpArrow:
                        ArrowKeyPressed.Invoke(Direction.Up);
                        continue;

                    case ConsoleKey.DownArrow:
                        ArrowKeyPressed.Invoke(Direction.Down);
                        continue;

                    case ConsoleKey.Escape:
                        isListening = false;
                        EscapeKeyPressed?.Invoke();
                        continue;

                    //case pt salvare (CTRL + S) + event
                }

                if (!char.IsControl(currentKey.KeyChar))
                {
                    CharacterKeyPressed.Invoke(currentKey.KeyChar);
                }
            }
        }
    }
}
