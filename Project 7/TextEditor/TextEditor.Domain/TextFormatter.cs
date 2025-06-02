using System;

namespace TextEditor.Domain
{
    public class TextFormatter
    {
        readonly FileManager fileManager;
        readonly (int width, int heigth) windowDimensions;
        readonly int startingRow;

        public TextFormatter(FileManager fileManager, (int, int) windowDimensions, int startingRow)
        {
            this.fileManager = fileManager;
            this.windowDimensions = windowDimensions;
            this.startingRow = startingRow;
        }

        //public string FormatWithinWindow()
        //{
        //    string result = string.Empty;

        //    if(fileManager.RowsCount > windowDimensions.heigth - startingRow)
        //    {

        //    }
        //}
    }
}
