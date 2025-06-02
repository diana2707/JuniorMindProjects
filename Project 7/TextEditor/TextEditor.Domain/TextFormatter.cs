using System;

namespace TextEditor.Domain
{
    public class TextFormatter
    {
        readonly FileManager fileManager;
        readonly (int width, int heigth) windowDimentions;

        public TextFormatter(FileManager fileManager)
        {
            this.fileManager = fileManager;
        }

        public void DisplayWithinWindow()
        {

        }
    }
}
