using System;

namespace TextEditor.Domain
{
    public interface IConsoleController
    {
        public void WriteLine();
        public void WriteLine(string message);
        public void Clear();
        public void SetCursorPosition(int left, int top);
        public string? ReadLine();
        public ConsoleKeyInfo ReadKeyWithoutDisplay();
    }
}
