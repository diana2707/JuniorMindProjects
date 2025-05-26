using System;

namespace TextEditor.Domain
{
    public class Cursor
    {
        FileManager fileManager;

        public Cursor(FileManager fileManager)
        {
            this.fileManager = fileManager;
        }
    }
}
