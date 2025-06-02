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

        //public string FormatWithinWindow()
        //{
        //    string result = string.Empty;

        //    if (fileManager.RowsCount > windowDimensions.heigth - startingRow)
        //    {

        //    }
        //}
    }
}
