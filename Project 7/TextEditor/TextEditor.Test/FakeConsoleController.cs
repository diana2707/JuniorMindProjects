using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class FakeConsoleController: IConsoleController
    {
        public bool IsWriteEmptyLineCalled { get; set; }
        public List<string> WrittenLines { get; set; } = [];
        public List<ConsoleKeyInfo> KeysToRead { get; set; } = [];
        public bool IsClearCalled { get; set; }
        public (int, int) CursorPosition { get; set; }
        public string? LineToRead { get; set; }

        public void WriteLine() => IsWriteEmptyLineCalled = true;
        public void WriteLine(string message) => WrittenLines.Add(message + "\r\n");
        public void Clear() => IsClearCalled = true;
        public void SetCursorPosition(int left, int top) => CursorPosition = (left, top);
        public string? ReadLine() => LineToRead;
        public ConsoleKeyInfo ReadKeyWithoutDisplay() => KeysToRead[0];
    }
}
