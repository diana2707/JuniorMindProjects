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

        public Cursor(IViewportInfo viewportInfo)
        {
            //aici imi calculeaza incepand cu ultimul rand nu cu randul de dedesubt
            this.viewportInfo = viewportInfo;
        }

        //event pentru cand se misca cursorul (urmeaza sa il invoc dupa miscarile cursorului)
        public event Action<(int column, int row)>  CursorMoved;

        public event Action<Direction>? CursorExitedView;

        public int Column => column;

        public int Row => row;

        public void SetInitialPosition()
        {
            row = viewportInfo.LastRowIndex;
            column = viewportInfo.GetRowLength(row);
            CursorMoved?.Invoke((column, row));
        }

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

        public void OnEscapeKeyPressed()
        {
            SetToStartOfLastLine();
            CursorMoved?.Invoke((column, row));
        }

        public void MoveRight()
        {
            if (column == viewportInfo.GetRowLength(row))
            {
                if (row == viewportInfo.LastRowIndex)
                {
                    CursorExitedView?.Invoke(Direction.Right);
                    //SetInitialPosition();
                    return;
                }

                column = 0;
                row++;

                CursorMoved?.Invoke((column, row));
                return;
            }

            column++;
            CursorMoved?.Invoke((column, row));
        }

        public void MoveLeft()
        {
            if (column == 0)
            {
                if (row == viewportInfo.FirstRowIndex)
                {
                    CursorExitedView?.Invoke(Direction.Left);
                    return;
                }

                row--;
                column = viewportInfo.GetRowLength(row);

                CursorMoved?.Invoke((column, row));
                return;
            }

            column--;
            CursorMoved?.Invoke((column, row));
        }

        public void MoveDown()
        {
            if (row == viewportInfo.LastRowIndex)
            {
                CursorExitedView?.Invoke(Direction.Down);
                return;
            }

            int nextRowLength = viewportInfo.GetRowLength(row + 1);

            column = nextRowLength < column ? nextRowLength : column;

            row++;

            CursorMoved?.Invoke((column, row));
        }

        public void MoveUp()
        {
            if (row == viewportInfo.FirstRowIndex)
            {
                CursorExitedView?.Invoke(Direction.Up);
                return;
            }

            int previousRowLength = viewportInfo.GetRowLength(row - 1);

            column = previousRowLength < column ? previousRowLength : column;

            row--;

            CursorMoved?.Invoke((column, row));
        }

        public void SetToStartOfLastLine()
        {
            column = 0;
            row = viewportInfo.LastRowIndex + 1;
        }
    }
}
