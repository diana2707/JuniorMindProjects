using System;

namespace TextEditor.Domain.Editor
{
    public class TextManager : ITextInfo
    {
        readonly List<string> text;
        
        public TextManager(string[] text)
        {
            this.text = [.. text];
        }

        //sa updatez pozitia absoluta din viewport
        //sa o folosesc pt a insera un caracter nou
        //dupa ce inserez un caracter nou, se actualizeaza viewportul?
        public (int row, int column) AbsoluteCursorPosition { get; private set; } = (0, 0);

        public int GetLinesCount() => text.Count;

        public string GetLine(int index) => text[index];

        public int GetLineLength(int index) => text[index].Length;

        //aici salvezi in memomy textul nou inserat

        public void OnCharacterKeyPressed(char ch, int row, int col)
        {
            InserCharacterAt(ch, row, col);
        }

        private void InserCharacterAt(char ch, int row, int col)
        {
            if (text.Count == 0)
            {
                text.Add(ch.ToString());
                return;
            }

            text[row] = text[row].Insert(col, ch.ToString());
        }
    }
}
