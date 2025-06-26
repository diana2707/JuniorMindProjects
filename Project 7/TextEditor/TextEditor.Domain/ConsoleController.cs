using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor.Domain
{
    public class ConsoleController : IConsoleController
    {
        public void WriteLine() => Console.WriteLine();

        public void WriteLine(string message) => Console.WriteLine(message);

        public void Clear() => Console.Clear();

        public void SetCursorPosition(int left, int top) => Console.SetCursorPosition(left, top);

        public string? ReadLine() => Console.ReadLine();
    }
}
