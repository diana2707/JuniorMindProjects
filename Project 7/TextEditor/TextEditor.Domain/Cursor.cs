using System;

namespace TextEditor.Domain
{
    public class Cursor
    {
        readonly FileManager fileManager;
        readonly int initialRowPosition;
        int row;
        int column;

        public Cursor(FileManager fileManager, int initialRowPosition)
        {
            this.fileManager = fileManager;
            this.initialRowPosition = initialRowPosition;
            row = fileManager.RowsCount - 1;
            column = fileManager.GetRowLength(row);
        }

        public int Column => column;

        public int Row => row + initialRowPosition;

        public void MoveRight()
        {
            if (column == fileManager.GetRowLength(row))
            {
                if (row == fileManager.RowsCount - 1)
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
                column = fileManager.GetRowLength(row);
                return;
            }

            column--;
        }

        public void MoveDown()
        {
            if (row == fileManager.RowsCount - 1)
            {
                return;
            }

            int nextRowLength = fileManager.GetRowLength(row + 1);

            column = nextRowLength < column ? nextRowLength : column;
            
            row++;
            
        }

        public void MoveUp()
        {
            if (row == 0)
            {
                return;
            }

            int previousRowLength = fileManager.GetRowLength(row - 1);

            column = previousRowLength < column ? previousRowLength : column;
            
            row--;
        }
    }
}
