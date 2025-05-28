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
            if (row == fileManager.LinesCount - 1 && column == fileManager.GetLineLength(row))
            {
                return;
            }
            else if (column == fileManager.GetLineLength(row))
            {
                column = 0;
                row++;
            }
            else
            {
                column++;
            }
        }

        public void MoveLeft()
        {
            if (row == 0 && column == 0)
            {
                return;
            }
            else if (column == 0)
            {
                row--;
                column = fileManager.GetLineLength(row);
            }
            else
            {
                column--;
            }
        }

        public void MoveDown()
        {
            if(row == fileManager.LinesCount - 1)
            {
                return;
            }
            else if(fileManager.GetLineLength(row + 1) < column)
            {
                row++;
                column = fileManager.GetLineLength(row);
            }
            else
            {
                row++;
            }
        }

        public void MoveUp()
        {
            row--;
        }
    }
}
