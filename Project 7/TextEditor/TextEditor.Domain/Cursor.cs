using System;
using System.Runtime.Serialization;

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
            row = textFormatter.LastDisplayedRowPosition;//nu calcueaza corect
            column = textFormatter.GetDisplayedRowLength(row);//nu calculeaza corect
        }

        public int Column => column;

        public int Row => row;

        public void MoveRight()
        {
            if (column == textFormatter.GetDisplayedRowLength(row) /*- 1*/)
            {
                if (row == textFormatter.LastDisplayedRowPosition)
                {
                    return;
                }

                column = 0;
                row++;
                return;
            }

            column++;
        }

        //set limit to the left to not go on a row
        public void MoveLeft()
        {
            if (column == 0)
            {
                if (row == textFormatter.FirstDisplayedRowPosition)
                {
                    return;
                }

                row--;
                //daca textul de pe column nu exista indexul pt column ar trebui sa fie tot 0
                column = textFormatter.GetDisplayedRowLength(row)/* - 1*/;
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

            column = nextRowLength < column ? nextRowLength /*- 1*/ : column;

            row++;

        }

        public void MoveUp()
        {
            if (row == textFormatter.FirstDisplayedRowPosition)
            {
                return;
            }

            int previousRowLength = textFormatter.GetDisplayedRowLength(row - 1);

            column = previousRowLength < column ? previousRowLength/* - 1*/ : column;

            row--;
        }
    }
}
