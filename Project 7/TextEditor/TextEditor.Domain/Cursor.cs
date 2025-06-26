using System;

namespace TextEditor.Domain
{
    public class Cursor
    {
        readonly TextManager textFormatter;
        int row;
        int column;

        public Cursor(TextManager textFormatter)
        {
            this.textFormatter = textFormatter;
            row = textFormatter.LastDisplayedRowPosition;
            column = textFormatter.GetDisplayedRowLength(row);
        }

        //public OnMove(row, column);
        public event Action<Direction>? CursorExitedView;

        public int Column => column;

        public int Row => row;

        public void MoveRight()
        {
            if (column == textFormatter.GetDisplayedRowLength(row))
            {
                if (row == textFormatter.LastDisplayedRowPosition)
                {
                    //aici poate fi Invoked OnMove.Invoke(row, column)
                    CursorExitedView?.Invoke(Direction.Right);
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
                    CursorExitedView?.Invoke(Direction.Left);
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
                CursorExitedView?.Invoke(Direction.Down);
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
                CursorExitedView?.Invoke(Direction.Up);
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
