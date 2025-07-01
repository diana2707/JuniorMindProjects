using System;

namespace TextEditor.Domain
{
    public class InputHandler
    {
        private readonly IConsoleController controller;

        public InputHandler(IConsoleController controller)
        {
            this.controller = controller;
        }

        public event Action<Direction> ArrowKeyPressed;

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
                //displayer.DisplayCursorAt(cursor.Column, cursor.Row);
                ConsoleKeyInfo currentKey = ReadKeyWithoutDisplay();

                switch (currentKey.Key)
                {
                    case ConsoleKey.RightArrow:
                        //cursor.MoveRight();
                        ArrowKeyPressed.Invoke(Direction.Right);
                        continue;

                    case ConsoleKey.LeftArrow:
                        //cursor.MoveLeft();
                        ArrowKeyPressed.Invoke(Direction.Left);
                        continue;

                    case ConsoleKey.UpArrow:
                        //cursor.MoveUp();
                        ArrowKeyPressed.Invoke(Direction.Up);
                        continue;

                    case ConsoleKey.DownArrow:
                        //cursor.MoveDown();
                        ArrowKeyPressed.Invoke(Direction.Down);
                        continue;

                    case ConsoleKey.Escape:
                        isListening = false;
                        EscapeKeyPressed?.Invoke();
                        //cursor.SetToStartOfLastLine();
                        //displayer.DisplayCursorAt(cursor.Column, cursor.Row);
                        continue;

                    default:
                        continue;
                }
            }
        }
    }
}
