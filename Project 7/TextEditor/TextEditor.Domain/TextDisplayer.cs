using System;

namespace TextEditor.Domain
{
    public class TextDisplayer
    {
        readonly FileManager fileManager;
        readonly (int width, int heigth) windowDimentions;

        public TextDisplayer(FileManager fileManager)
        {
            this.fileManager = fileManager;
        }
    }
}
