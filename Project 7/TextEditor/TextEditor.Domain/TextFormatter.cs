using System;

namespace TextEditor.Domain
{
    public class TextFormatter
    {
        readonly string[] text;
        readonly int windowWidth;
        readonly int windowHeigth;
        readonly int startingRow;

        public TextFormatter(string[] text, int windowWidth, int windowHeigth, int startingRow)
        {
            this.text = text;
            this.windowWidth = windowWidth;
            this.windowHeigth = windowHeigth;
            this.startingRow = startingRow;
        }

        public int FirstDisplayedRowPosition => startingRow;

        public int LastDisplayedRowPosition
        {
            get
            {
                //daca este exact egal cu window heigth avand in vedere ca ultimul rand ar trebui sa ramana liber?
                if (startingRow + text.Length > windowHeigth)
                {
                    return windowHeigth - 2;
                }

                return startingRow + text.Length - 1;
            }
        }

        public int GetDisplayedRowLength(int rowPosition)
        {
            if (text[rowPosition - startingRow].Length > windowWidth - 1)
            {
                return windowWidth - 1;
            }

            return text[rowPosition - startingRow].Length;
        }

        public List<string> FormatWithinWindow()
        {
            List<string> result = [];

            int textHeigth = text.Length < windowHeigth - startingRow ? text.Length : windowHeigth - startingRow - 1;

            for (int i = 0; i < textHeigth; i++)
            {
                result.Add((text[i].Length > windowWidth - 1 ? text[i][..(windowWidth - 1)] : text[i]));
            }

            return result;
        }
    }
}
