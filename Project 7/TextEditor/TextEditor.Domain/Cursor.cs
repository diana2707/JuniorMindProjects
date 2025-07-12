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

        public event Action<Direction, int>? CursorExitedView;

        public int Column => column;

        public int Row => row;

        //redenumeste pt claritate - seteaza cursorul la finalul ultimului rand
        public void SetInitialPosition()
        {
            row = viewportInfo.LastRowIndex;
            column = viewportInfo.GetViewportRowLength(row);
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

        //public void MoveRight()
        //{
        //    if (column == viewportInfo.GetRowLength(row))
        //    {

        //        if (row == viewportInfo.LastRowIndex)
        //        {
        //            CursorExitedView?.Invoke(Direction.Right);
        //            SetInitialPosition();
        //            return;
        //        }

        //        column = 0;
        //        row++;

        //        CursorMoved?.Invoke((column, row));
        //        return;
        //    }

        //    column++;
        //    CursorMoved?.Invoke((column, row));
        //}

        public void MoveRight()
        {
            if (column == viewportInfo.GetViewportRowLength(row)) //IsLastVisibleColumn(row)
            {
                //CURSORUL SARE PE RANDUL URMATOR DOAR DACA CURSORUL SE AFLA PE ULTIMUL RAND

                if (viewportInfo.IsRightmostLimitVisible(row)) //IsRightmostCharacter(row)
                {
                    //daca e final de text nu ar mai trebui mutat (rezolva conditia)
                    if (viewportInfo.IsDownmostLimitVisible(row)) //IsDownmostLine(row)
                    {
                        return;
                    }

                    column = 0;

                    if(row == viewportInfo.LastRowIndex) //IsLastVisibleRow(row)
                    {
                        CursorExitedView?.Invoke(Direction.MaxLeftAndDown, row);
                        CursorMoved?.Invoke((column, row));
                        return;
                    }

                    //aici cursorul trece pe randul urmator
                    row++;

                    //daca incepututl randului urmator este in afara viewportului am nevoie sa rerandez
                    if (!viewportInfo.IsLeftmostLimitVisible()) //CanScrollLeft()
                    {
                        CursorExitedView?.Invoke(Direction.MaxLeft, row);
                    }

                    CursorMoved?.Invoke((column, row));
                    return;
                }

                CursorExitedView?.Invoke(Direction.Right, row);
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
                    return;
                }

                row--;
                column = viewportInfo.GetViewportRowLength(row);

                if (!viewportInfo.IsRightmostLimitVisible(row))
                {
                    CursorExitedView?.Invoke(Direction.MaxRight, row);
                }

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
                CursorExitedView?.Invoke(Direction.Down, row);
                CursorMoved?.Invoke((column, row));
                return;
            }

            int nextRowLength = viewportInfo.GetViewportRowLength(row + 1);

            column = nextRowLength < column ? nextRowLength : column;

            row++;

            CursorMoved?.Invoke((column, row));
        }

        public void MoveUp()
        {
            if (row == viewportInfo.FirstRowIndex)
            {
                CursorExitedView?.Invoke(Direction.Up, row);
                CursorMoved?.Invoke((column, row));
                return;
            }

            int previousRowLength = viewportInfo.GetViewportRowLength(row - 1);

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
