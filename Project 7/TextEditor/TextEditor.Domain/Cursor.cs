using System;

namespace TextEditor.Domain
{
    public class Cursor
    {
        readonly IViewportInfo viewportInfo;
        int row;
        int column;

        public Cursor(IViewportInfo viewportInfo)
        {
            this.viewportInfo = viewportInfo;
        }

        public event Action<(int column, int row)>  CursorMoved;

        public event Action<Direction, int>? CursorExitedViewport;

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

        public void OnEscapeKeyPressed()
        {
            MoveAfterViewport();
            CursorMoved?.Invoke((column, row));
        }

        public void MoveRight()
        {
            if (!IsAtRightEdgeColumn())
            {
                column++;
                CursorMoved?.Invoke((column, row));
                return;
            }

            if (!viewportInfo.IsScrolledToTextLineEnd(row))
            {
                CursorExitedViewport?.Invoke(Direction.Right, row);
                CursorMoved?.Invoke((column, row));
                return;
            }

            if (viewportInfo.IsLastTextLine(row))
            {
                return;
            }

            if(IsAtBottomRow())
            {
                CursorExitedViewport?.Invoke(Direction.Down, row);
            }
            else
            {
                row++;
            }

            if (!viewportInfo.IsScrolledToTextLineStart())
            {
                CursorExitedViewport?.Invoke(Direction.MaxLeft, row);
            }

            column = 0;

            CursorMoved?.Invoke((column, row));
            return;
        }

        public void MoveLeft()
        {
            if (!IsAtLeftEdgeColumn())
            {
                column--;
                CursorMoved?.Invoke((column, row));
                return;
            }

            if (!viewportInfo.IsScrolledToTextLineStart())
            {
                CursorExitedViewport?.Invoke(Direction.Left, row);
                CursorMoved?.Invoke((column, row));
                return;
            }

            if (viewportInfo.IsFirstTextLine(row))
            {
                return;
            }
                    
            if (IsAtTopRow())
            {
                CursorExitedViewport?.Invoke(Direction.Up, row);
            }
            else
            {
                row--;
            }

            if (!viewportInfo.IsScrolledToTextLineEnd(row))
            {
                CursorExitedViewport?.Invoke(Direction.MaxRight, row);
            }

            column = viewportInfo.GetVisibleRowLength(row);

            CursorMoved?.Invoke((column, row));
            return;
        }

        public void MoveDown()
        {
            if (row == viewportInfo.BottomRowIndex)
            {
                //+conditie pt downmost limit: am conditia in viewport!!!
                if (viewportInfo.IsLastTextLine(row)) //IsDownmostLine(row)
                {
                    return;
                }

                //conditia pt cand randul este mai scurt
                CursorExitedViewport?.Invoke(Direction.Down, row);

                int lastRowLength = viewportInfo.GetVisibleRowLength(row);

                //conditia pt cand randul este mai scurt si in afara viewportului
                if (lastRowLength < 0)
                {
                   CursorExitedViewport?.Invoke(Direction.ToRowEnd, row);
                }

                lastRowLength = viewportInfo.GetVisibleRowLength(row);
                column = lastRowLength < column ? lastRowLength : column;

                //conditia pt cand randul are o lungime similara iar cursorul isi poate pastra randul si coloana
                CursorMoved?.Invoke((column, row));
                return;
            }

            row++;

            int nextRowLength = viewportInfo.GetVisibleRowLength(row);

            //test ca sa verific if-ul asta
            // conditie pt randul urmator din afara viewportului
            if (nextRowLength < 0)
            {
                CursorExitedViewport?.Invoke(Direction.ToRowEnd, row);
            }

            //gestionza cand cursorul e in pozitia 0?

            nextRowLength = viewportInfo.GetVisibleRowLength(row);

            column = nextRowLength < column ? nextRowLength : column;

            CursorMoved?.Invoke((column, row));
        }

        public void MoveUp()
        {
            if (row == viewportInfo.TopRowIndex)
            {
                if (viewportInfo.IsFirstTextLine(row))
                {
                    return;
                }

                //conditia pt cand randul este mai scurt
                CursorExitedViewport?.Invoke(Direction.Up, row);

                int firstRowLength = viewportInfo.GetVisibleRowLength(row);

                //conditia pt cand randul este mai scurt si in afara viewportului
                //trebuie facuta diferenta intre situatia cand am lungime 0 pt ca nu e numic pe rand si cea in care 
                if (firstRowLength < 0)
                {
                    CursorExitedViewport?.Invoke(Direction.ToRowEnd, row);
                }

                firstRowLength = viewportInfo.GetVisibleRowLength(row);

                column = firstRowLength < column ? firstRowLength : column;
                //conditia pt cand randul are o lungime similara iar cursorul isi poate pastra randul si coloana
                CursorMoved?.Invoke((column, row));
                return;
            }

            // nu e bine, pt randul urmator din afara viewportului nu functioneaza
            row--;

            int previousRowLength = viewportInfo.GetVisibleRowLength(row);

            if (previousRowLength <= 0)
            {
                CursorExitedViewport?.Invoke(Direction.ToRowEnd, row);
            }

            previousRowLength = viewportInfo.GetVisibleRowLength(row);
            column = previousRowLength < column ? previousRowLength : column;

            CursorMoved?.Invoke((column, row));
        }

        public void SetDefaultPosition()
        {
            row = viewportInfo.TopRowIndex;
            column = 0;
            CursorMoved?.Invoke((column, row));
        }

        public void MoveAfterViewport()
        {
            column = 0;
            row = viewportInfo.BottomRowIndex + 1;
        }

        private bool IsAtRightEdgeColumn()
        {
            return column == viewportInfo.GetVisibleRowLength(row);
        }

        private bool IsAtLeftEdgeColumn()
        {
            return column == 0;
        }

        private bool IsAtTopRow()
        {
            return row == viewportInfo.TopRowIndex;
        }

        private bool IsAtBottomRow()
        {
            return row == viewportInfo.BottomRowIndex;
        }
    }
}
