using System;

namespace TextEditor.Domain
{
    public class Cursor
    {
        readonly TextFormatter textFormatter;
        int row;
        int column;

        public Cursor(TextFormatter textFormatter)
        {
            this.textFormatter = textFormatter;
            row = textFormatter.GetLastDisplayedRowPosition();
            column = textFormatter.GetDisplayedRowLength(row);
        }

        public int Column => column;

        public int Row => row;

        public void MoveRight()
        {
            if (column == textFormatter.GetDisplayedRowLength(row))
            {
                if (row == textFormatter.GetLastDisplayedRowPosition())
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
                column = textFormatter.GetDisplayedRowLength(row);
                return;
            }

            column--;
        }

        public void MoveDown()
        {
            if (row == textFormatter.GetLastDisplayedRowPosition())
            {
                return;
            }

            int nextRowLength = textFormatter.GetDisplayedRowLength(row + 1);

            column = nextRowLength < column ? nextRowLength : column;

            row++;

        }

        public void MoveUp()
        {
            if (row == 0)
            {
                return;
            }

            int previousRowLength = textFormatter.GetDisplayedRowLength(row - 1);

            column = previousRowLength < column ? previousRowLength : column;

            row--;
        }
    }
}
