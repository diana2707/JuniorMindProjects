using System;

namespace TextEditor.Domain
{
    public class Cursor
    {
        readonly TextFormatter textFormatter;
        readonly int initialRowPosition;
        int row;
        int column;

        public Cursor(TextFormatter textFormatter)
        {
            this.textFormatter = textFormatter;
            row = textFormatter.LastRowPosition;
            //column = fileManager.GetRowLength(row);
        }

        public int Column => column;

        public int Row => textFormatter.LastRowPosition;

        //public void MoveRight()
        //{
        //    if (column == fileManager.GetRowLength(row))
        //    {
        //        if (row == fileManager.RowsCount - 1)
        //        {
        //            return;
        //        }

        //        column = 0;
        //        row++;
        //        return;
        //    }

        //    column++;
        //}

        //public void MoveLeft()
        //{
        //    if (column == 0)
        //    {
        //        if (row == 0)
        //        {
        //            return;
        //        }

        //        row--;
        //        column = fileManager.GetRowLength(row);
        //        return;
        //    }

        //    column--;
        //}

        //public void MoveDown()
        //{
        //    if (row == fileManager.RowsCount - 1)
        //    {
        //        return;
        //    }

        //    int nextRowLength = fileManager.GetRowLength(row + 1);

        //    column = nextRowLength < column ? nextRowLength : column;

        //    row++;

        //}

        //public void MoveUp()
        //{
        //    if (row == 0)
        //    {
        //        return;
        //    }

        //    int previousRowLength = fileManager.GetRowLength(row - 1);

        //    column = previousRowLength < column ? previousRowLength : column;

        //    row--;
        //}
    }
}
