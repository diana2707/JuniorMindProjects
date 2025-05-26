using System;

namespace TextEditor.Domain
{
    public class Cursor
    {
        FileManager fileManager;
        int row;
        int column;

        public Cursor(FileManager fileManager)
        {
            this.fileManager = fileManager;
        }

        public int Column => column;

        public void MoveRight()
        {
            column++;
        }
    }
}
