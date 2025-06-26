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

                        continue;

                    case ConsoleKey.LeftArrow:
                        //cursor.MoveLeft();
                        continue;

                    case ConsoleKey.UpArrow:
                        //cursor.MoveUp();
                        continue;

                    case ConsoleKey.DownArrow:
                        //cursor.MoveDown();
                        continue;

                    case ConsoleKey.Escape:
                        isListening = false;
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
