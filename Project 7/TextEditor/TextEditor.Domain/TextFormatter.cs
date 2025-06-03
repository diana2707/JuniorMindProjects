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

        public int LastDisplayedRowPosition
        {
            get
            {
                if (startingRow + text.Length > windowHeigth)
                {
                    return windowHeigth - startingRow - 1;
                }

                return startingRow + text.Length - 1;
            }
        }

        public int GetDisplayedRowLength(int rowPosition)
        {
            if (text[rowPosition - startingRow].Length > windowWidth)
            {
                return windowWidth /*- 1*/;
            }

            return text[rowPosition - startingRow].Length;
        }

        public string FormatWithinWindow()
        {
            string result = string.Empty;

            int textHeigth = text.Length < windowHeigth - startingRow ? text.Length : windowHeigth - startingRow;

            for (int i = 0; i < textHeigth; i++)
            {
                result += (text[i].Length > windowWidth ? text[i][..windowWidth] : text[i]) + "\r\n";
            }

            return result;
        }
    }
}
