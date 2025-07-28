using System;
using TextEditor.Domain.Display;
using TextEditor.Domain.Shared;

namespace TextEditor.Domain.Cursor
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

        

        public event Action<(int column, int row)> CursorMoved;

        public event Action<Direction, int>? CursorExitedViewport;

        public int Column => column;

        public int Row => row;

        //OnCharacterInserted => metoda care face handle pt inserarea unui  caracter nou => muta cursorul o pozitie inainte
        //gandeste intai o inserare simpla, fara alt text, ulterior va trebui gestionat viewportul cand text iese in afara

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

            if (IsAtBottomRow())
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
        }

        public void MoveDown()
        {
            if (viewportInfo.IsLastTextLine(row))
            {
                return;
            }

            if (IsAtBottomRow())
            {
                CursorExitedViewport?.Invoke(Direction.Down, row);
            }
            else
            {
                row++;
            }

            if (!IsCurrentRowVisible())
            {
                CursorExitedViewport?.Invoke(Direction.ToRowEnd, row);
            }

            int currentRowLength = viewportInfo.GetVisibleRowLength(row);

            column = currentRowLength < column ? currentRowLength : column;

            CursorMoved?.Invoke((column, row));
        }

        public void MoveUp()
        {
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

            if (!IsCurrentRowVisible())
            {
                CursorExitedViewport?.Invoke(Direction.ToRowEnd, row);
            }

            int previousRowLength = viewportInfo.GetVisibleRowLength(row);
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

        private bool IsCurrentRowVisible()
        {
            return viewportInfo.GetVisibleRowLength(row) > 0;
        }
    }
}
