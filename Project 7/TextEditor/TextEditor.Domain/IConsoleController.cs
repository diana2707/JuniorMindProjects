using System;

namespace TextEditor.Domain
{
    public interface IConsoleController
    {
        public int Width { get; }
        public int Height { get; }
        public void WriteLine();
        public void WriteLine(string message);
        public void Clear();
        public void SetCursorPosition(int left, int top);
        public string? ReadLine();
        public ConsoleKeyInfo ReadKeyWithoutDisplay();
    }
}
