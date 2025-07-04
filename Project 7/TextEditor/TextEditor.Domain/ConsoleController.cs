using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextEditor.Domain
{
    public class ConsoleController : IConsoleController
    {
        public int Width => Console.WindowWidth;
        public int Height => Console.WindowHeight;

        public void WriteLine() => Console.WriteLine();

        public void WriteLine(string message) => Console.WriteLine(message);

        public void Clear() => Console.Clear();

        public void SetCursorPosition(int left, int top) => Console.SetCursorPosition(left, top);

        public string? ReadLine() => Console.ReadLine();

        public ConsoleKeyInfo ReadKeyWithoutDisplay() => Console.ReadKey(intercept: true);
    }
}
