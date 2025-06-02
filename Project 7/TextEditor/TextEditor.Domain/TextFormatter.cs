using System;

namespace TextEditor.Domain
{
    public class TextFormatter
    {
        readonly string[] text;
        readonly (int width, int heigth) windowDimensions;
        readonly int startingRow;

        public TextFormatter(string[] text, (int, int) windowDimensions, int startingRow)
        {
            this.text = text;
            this.windowDimensions = windowDimensions;
            this.startingRow = startingRow;
        }

        public string FormatWithinWindow()
        {
            string result = string.Empty;

            int textHeigth = text.Length < windowDimensions.heigth - startingRow ? text.Length : windowDimensions.heigth - startingRow;

            for (int i = 0; i < textHeigth; i++)
            {
                result += (text[i].Length > windowDimensions.width ? text[i][..windowDimensions.width] : text[i]) + "\r\n";
            }

            return result;
        }
    }
}
