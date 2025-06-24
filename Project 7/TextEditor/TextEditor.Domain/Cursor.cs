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
            row = textFormatter.LastDisplayedRowPosition;
            column = textFormatter.GetDisplayedRowLength(row);
        }

        //public OnMove(row, column);
        public event Action<int, int> CursorExitedView;

        public int Column => column;

        public int Row => row;

        public void MoveRight()
        {
            if (column == textFormatter.GetDisplayedRowLength(row))
            {
                if (row == textFormatter.LastDisplayedRowPosition)
                {
                    //aici poate fi Invoked OnMove.Invoke(row, column)
                    CursorExitedView?.Invoke(row, column);
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
                if (row == textFormatter.FirstDisplayedRowPosition)
                {
                    CursorExitedView?.Invoke(row, column);
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
            if (row == textFormatter.LastDisplayedRowPosition)
            {
                return;
            }

            int nextRowLength = textFormatter.GetDisplayedRowLength(row + 1);

            column = nextRowLength < column ? nextRowLength : column;

            row++;

        }

        public void MoveUp()
        {
            if (row == textFormatter.FirstDisplayedRowPosition)
            {
                return;
            }

            int previousRowLength = textFormatter.GetDisplayedRowLength(row - 1);

            column = previousRowLength < column ? previousRowLength : column;

            row--;
        }

        public void SetToStartOfLastLine()
        {
            column = 0;
            row = textFormatter.LastDisplayedRowPosition + 1;
        }
    }
}
