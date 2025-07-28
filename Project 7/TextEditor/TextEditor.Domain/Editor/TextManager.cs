using System;

namespace TextEditor.Domain.Editor
{
    public class TextManager : ITextInfo
    {
        readonly string[] text;
        
        public TextManager(string[] text)
        {
            this.text = text;
        }

        //sa updatez pozitia absoluta din viewport
        //sa o folosesc pt a insera un caracter nou
        //dupa ce inserez un caracter nou, se actualizeaza viewportul?
        public (int row, int column) AbsoluteCursorPosition { get; private set; } = (0, 0);

        public int GetLinesCount() => text.Length;

        public string GetLine(int index) => text[index];

        public int GetLineLength(int index) => text[index].Length;

        //aici salvezi in memomy textul nou inserat
        //
    }
}
