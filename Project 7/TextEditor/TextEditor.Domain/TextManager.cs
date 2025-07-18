using System;

namespace TextEditor.Domain
{
    public class TextManager : ITextInfo
    {
        readonly string[] text;
        
        public TextManager(string[] text)
        {
            this.text = text;
        }

        public (int row, int column) AbsoluteCursorPosition { get; private set; } = (0, 0);

        public int GetLinesCount() => text.Length;

        public string GetLine(int index) => text[index];

        public int GetLineLength(int index) => text[index].Length;
    }
}
