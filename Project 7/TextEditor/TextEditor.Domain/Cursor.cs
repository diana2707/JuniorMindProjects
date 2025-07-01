using System;

namespace TextEditor.Domain
{
    public class Cursor
    {
        //cursorul ar trebui sa pastreze pozitia relativa fata de viewport => acces la limitele viewportului
        //dar sa fie aware de limitele textului => acces la limitele textului
        //dar fara sa contina o instanta din textManager/viewport pt ca implica un coupling prea mare
        //CUM FAC ASTA?

        //creaza o intefata cu metodele de care ai nevoie
        //TextManager/viewport o implementeaza si ai access doar la info de care ai nevoie (numerul de randuri si o metoda care imi aduce lungimea randului)


        readonly IViewportInfo viewportInfo;
        int row;
        int column;

        //aici inlocuiest TextMnager cu viewport
        public Cursor(IViewportInfo viewportInfo)
        {
            this.viewportInfo = viewportInfo;
            row = viewportInfo.LastRowPosition;
            column = viewportInfo.GetRowLength(row);
        }

        //event pentru cand se misca cursorul (urmeaza sa il invoc dupa miscarile cursorului)
        public event Action<(int row, int column)> CursorMoved;

        public event Action<Direction>? CursorExitedView;

        public int Column => column;

        public int Row => row;

        public void OnArrowKeyPressed(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    MoveLeft();
                    return;

                case Direction.Right:
                    MoveRight();
                    return;

                case Direction.Up:
                    MoveUp();
                    return;

                case Direction.Down:
                    MoveDown();
                    return;
            }
        }

        public void MoveRight()
        {
            if (column == viewportInfo.GetRowLength(row))
            {
                if (row == viewportInfo.LastRowPosition)
                {
                    CursorExitedView?.Invoke(Direction.Right);
                    return;
                }

                column = 0;
                row++;

                CursorMoved?.Invoke((row, column));
                return;
            }

            CursorMoved?.Invoke((row, column));
            column++;
        }

        public void MoveLeft()
        {
            if (column == 0)
            {
                if (row == viewportInfo.FirstRowPosition)
                {
                    CursorExitedView?.Invoke(Direction.Left);
                    return;
                }

                row--;
                column = viewportInfo.GetRowLength(row);

                CursorMoved?.Invoke((row, column));
                return;
            }

            column--;
            CursorMoved?.Invoke((row, column));
        }

        public void MoveDown()
        {
            if (row == viewportInfo.LastRowPosition)
            {
                CursorExitedView?.Invoke(Direction.Down);
                return;
            }

            int nextRowLength = viewportInfo.GetRowLength(row + 1);

            column = nextRowLength < column ? nextRowLength : column;

            row++;

            CursorMoved?.Invoke((row, column));
        }

        public void MoveUp()
        {
            if (row == viewportInfo.FirstRowPosition)
            {
                CursorExitedView?.Invoke(Direction.Up);
                return;
            }

            int previousRowLength = viewportInfo.GetRowLength(row - 1);

            column = previousRowLength < column ? previousRowLength : column;

            row--;

            CursorMoved?.Invoke((row, column));
        }

        public void SetToStartOfLastLine()
        {
            column = 0;
            row = viewportInfo.LastRowPosition + 1;
        }
    }
}
