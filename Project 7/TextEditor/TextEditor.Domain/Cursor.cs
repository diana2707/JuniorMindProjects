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

        public int Row => row;

        public void MoveRight()
        {
            if (column == fileManager.GetLineLength(row))
            {
                if (row == fileManager.LinesCount - 1)
                {
                    return;
                }

                column = 0;
                row++;
                return;
            }
      
            column++;
        }

        public void MoveLeft()
        {
            if (column == 0)
            {
                if (row == 0)
                {
                    return;
                }

                row--;
                column = fileManager.GetLineLength(row);
                return;
            }

            column--;
        }

        public void MoveDown()
        {
            if (row == fileManager.LinesCount - 1)
            {
                return;
            }

            int nextRowLength = fileManager.GetLineLength(row + 1);

            column = nextRowLength < column ? nextRowLength : column;
            
            row++;
            
        }

        public void MoveUp()
        {
            if(row == 0)
            {
                return;
            }
            else if (fileManager.GetLineLength(row - 1) < column)
            {
                row--;
                column = fileManager.GetLineLength(row);
            }
            else
            {
                row--;
            }
            
        }
    }
}
