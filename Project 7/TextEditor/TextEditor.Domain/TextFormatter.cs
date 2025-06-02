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
