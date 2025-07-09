using Microsoft.VisualStudio.TestPlatform.Common.ExtensionFramework;
using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Extensions;
using System;
using TextEditor.Domain;

namespace TextEditor.Test
{
    public class FakeConsoleController: IConsoleController
    {
        public bool IsWriteEmptyLineCalled { get; set; }
        public List<string> WrittenLines { get; set; } = [];
        public bool IsClearCalled { get; set; }
        public (int left, int top) CursorPosition { get; set; }
        public string? LineToRead { get; set; }

        public List<ConsoleKeyInfo> KeysToRead { get; set; } = [];
        int keysIndex = 0;

        public int Width => 120;
        public int Height => 30;
        public void WriteLine() => IsWriteEmptyLineCalled = true;
        public void WriteLine(string message)
        {
            if (WrittenLines.Count < CursorPosition.top) //egal aici?
            {
                for (int i = WrittenLines.Count; i < CursorPosition.top - WrittenLines.Count + 1; i++)
                {
                    WrittenLines.Add(new string(' ', Width) + "\r\n");
                }
            }

            if(WrittenLines.Count != 0 && CursorPosition.top < WrittenLines.Count) //count -1? ca sa fie index
            {
                WrittenLines[CursorPosition.top] = message + "\r\n";
                //(int left, int top) newPosition = CursorPosition;
                //newPosition.top++;
                //CursorPosition = newPosition;
                IncrementCursorRow();

                return;
            }

            WrittenLines.Add(message + "\r\n");
            IncrementCursorRow();
        }
        public void Clear() => IsClearCalled = true;
        public void SetCursorPosition(int left, int top) => CursorPosition = (left, top);
        public string? ReadLine() => LineToRead;
        public ConsoleKeyInfo ReadKeyWithoutDisplay() => KeysToRead[keysIndex++];

        private void IncrementCursorRow()
        {
            (int left, int top) newPosition = CursorPosition;
            newPosition.top++;
            CursorPosition = newPosition;
        }
    }
}
