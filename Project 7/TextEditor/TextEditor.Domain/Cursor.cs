using System;

namespace TextEditor.Domain
{
    public class Cursor
    {
        FileManager fileManager;
        (int column, int row) initialPosition;
        int row;
        int column;

        public Cursor(FileManager fileManager, (int column, int row) initialPosition)
        {
            this.fileManager = fileManager;
            this.initialPosition = initialPosition;
            row = fileManager.LinesCount - 1;
            column = fileManager.GetLineLength(row);
        }

        public int Column => column;

        public int Row => row + initialPosition.row;

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
            if (row == 0)
            {
                return;
            }

            int previousRowLength = fileManager.GetLineLength(row - 1);

            column = previousRowLength < column ? previousRowLength : column;
            
            row--;
        }
    }
}
