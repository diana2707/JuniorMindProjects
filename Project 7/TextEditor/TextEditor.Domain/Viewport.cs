using System;

namespace TextEditor.Domain
{
    //De gestionat: la up/down, cursorul sare la finalul randului chiar daca randul este iesit din viewport si viewportul trebuie sa se rerandeze

    public class Viewport : IViewportInfo
    {
        private readonly int windowWidth;
        private readonly int windowHeigth;
        private readonly ITextInfo textInfo;

        //private int lastRowAbsoluteIndex;
        //private int lastColumnAbsoluteIndex;

        public Viewport(int windowWidth, int windowHeigth, int firstRowIndex, ITextInfo textInfo)
        {
            this.windowWidth = windowWidth;
            this.windowHeigth = windowHeigth;
            this.textInfo = textInfo;

            FirstRowIndex = firstRowIndex;

            //FirstRowIndex = startTextPosition;
            //LastRowIndex = InitializeLastRowIndex();
            //FirstColumnIndex = 0;
            //LastColumnIndex = 118;
        }

        public event Action<List<string>, (int column, int row)> ViewportMoved;

        public int FirstRowIndex { get; }

        public int LastRowIndex
        {
            get
            {
                int textLength = textInfo.GetLinesCount();
                return FirstRowIndex + textLength >= windowHeigth
                    ? windowHeigth - 2
                    : FirstRowIndex + textLength - 1;
            }
        }

        public int FirstRowAbsoluteIndex { get; private set; } = 0;

        public int LastRowAbsoluteIndex => FirstRowAbsoluteIndex + (LastRowIndex - FirstRowIndex); //am nevoie de -1?
        //{
        //    get
        //    {
        //        int viewportHeigth = LastRowIndex - FirstRowIndex + 1;
        //        int textHeigth = textInfo.GetLinesCount();
        //        if (textHeigth == 0)
        //        {
        //            return 0;
        //        }

        //        return viewportHeigth > textHeigth
        //            ? textHeigth - 1
        //            : FirstRowAbsoluteIndex + viewportHeigth - 1;
        //    }

        //}

        public int FirstColumnAbsoluteIndex { get; private set; } = 0;

        //pune conditie sa nu depaseasca lungimea randului
        public int LastColumnAbsoluteIndex(int rowIndex)
        {
            int absoluteLength = textInfo.GetLineLength(rowIndex);

            if (absoluteLength == 0)
            {
                return -1;
            }

            if (FirstColumnAbsoluteIndex + windowWidth - 2 < absoluteLength) //conditia asta nu e buna
            {
                return FirstColumnAbsoluteIndex + windowWidth - 2;
            }

            return absoluteLength - 1;

        }

        //la row corectez in LastRowIndex, aici corectez direct cu -2?

        public bool IsRightmostLimitVisible(int viewportRowIndex)
        {
            int absoluteRowIndex = viewportRowIndex - FirstRowIndex + FirstRowAbsoluteIndex;
            return LastColumnAbsoluteIndex(absoluteRowIndex) == textInfo.GetLineLength(absoluteRowIndex) - 1;
        }

        public bool IsDownmostLimitVisible(int viewportRowIndex)
        {
            int absoluteRowIndex = viewportRowIndex - FirstRowIndex + FirstRowAbsoluteIndex; //calculeaza gresit pt cand randul este scrollat...
            return absoluteRowIndex == textInfo.GetLinesCount() - 1;
        }

        public bool IsLeftmostLimitVisible()
        {
            return FirstColumnAbsoluteIndex == 0;
        }

        public bool IsUpmostLimitVisible(int viewportRowIndex)
        {
            int absoluteRowIndex = viewportRowIndex - FirstRowIndex + FirstRowAbsoluteIndex;
            return absoluteRowIndex == 0;
        }

        public int GetViewportRowLength(int viewportRowIndex)
        {
            int absoluteRowIndex = viewportRowIndex - FirstRowIndex + FirstRowAbsoluteIndex;
            return LastColumnAbsoluteIndex(absoluteRowIndex) - FirstColumnAbsoluteIndex + 1;

            //if (textInfo.GetLineLength(index - FirstRowIndex)  /*text[rowPosition - startTextPosition].Length*/ > windowWidth - 1)
            //{
            //    return windowWidth - 1;
            //}

            //return textInfo.GetLineLength(index - FirstRowIndex)/*text[rowPosition - startTextPosition].Length*/;
        }

        //cum pot sa fac renderul flezibil?
        public List<string> RenderText()
        {
            List<string> result = [];

            //int textHeigth = textInfo.GetLinesCount() < windowHeigth - FirstRowIndex
            //    ? textInfo.GetLinesCount()
            //    : windowHeigth - FirstRowIndex - 1;

            for (int i = FirstRowAbsoluteIndex; i <= LastRowAbsoluteIndex; i++)
            {
                if (LastColumnAbsoluteIndex(i) == -1 || FirstColumnAbsoluteIndex > LastColumnAbsoluteIndex(i))
                {
                    result.Add(string.Empty);
                    continue;
                }

                result.Add(textInfo.GetLine(i)[FirstColumnAbsoluteIndex..(LastColumnAbsoluteIndex(i) + 1)]);
            }

            return result;
        }


        //inainte de rerender thebuie sa setez cursorul la inceputul textlui
        public void OnCursorExitedView(Direction direction, int viewportRowIndex)
        {
            switch (direction)
            {
                case Direction.Right:
                    MoveRight(viewportRowIndex);
                    break;

                case Direction.Left:
                    MoveLeft();
                    break;

                case Direction.Down:
                    MoveDown();
                    break;

                case Direction.Up:
                    MoveUp();
                    break;

                case Direction.MaxLeft:
                    MoveMaxLeft();
                    break;

                case Direction.MaxRight:
                    MoveMaxRight(viewportRowIndex);
                    break;

                case Direction.MaxLeft | Direction.Down:
                    //MoveMaxLeftAndDown();
                    MoveMaxLeft();
                    MoveDown();
                    break;

                case Direction.MaxRight | Direction.Up:
                    //MoveMaxRightAndUp(viewportRowIndex);
                    MoveUp();
                    MoveMaxRight(viewportRowIndex);
                    break;

                case Direction.ToRowEnd:
                    MoveToRowEnd(viewportRowIndex);
                    break;

                case Direction.Down | Direction.ToRowEnd:
                    MoveDown();
                    MoveToRowEnd(viewportRowIndex);
                    break;
            }

            List<string> renderedText = RenderText();
            ViewportMoved?.Invoke(renderedText, (column: 0, row: FirstRowIndex));
        }

        public void MoveRight(int viewportRowIndex)
        {
            //int absoluteRowIndex = rowIndex - FirstRowIndex;
            if (IsRightmostLimitVisible(viewportRowIndex))
            {
                return;
            }

            FirstColumnAbsoluteIndex++;
        }

        public void MoveLeft()
        {
            FirstColumnAbsoluteIndex--;
        }

        public void MoveDown()
        {
            if (LastRowAbsoluteIndex == textInfo.GetLinesCount() - 1)
            {
                return;
            }

            FirstRowAbsoluteIndex++;
        }

        public void MoveUp()
        {
            if (FirstRowAbsoluteIndex == 0)
            {
                return;
            }

            FirstRowAbsoluteIndex--;
        }

        public void MoveMaxLeft()
        {
            FirstColumnAbsoluteIndex = 0;
        }

        public void MoveMaxRight(int viewportRowIndex)
        {
            int absoluteRowIndex = viewportRowIndex - FirstRowIndex + FirstRowAbsoluteIndex;
            int rowLength = textInfo.GetLineLength(absoluteRowIndex);

            //am nevoie de asta si in MoveMaxLeft? de ce?
            if (rowLength < windowWidth - 2)
            {
                FirstColumnAbsoluteIndex = 0;
                return;
            }

            FirstColumnAbsoluteIndex = rowLength - 1 - windowWidth + 2;
        }

        //public void MoveMaxLeftAndDown()
        //{
        //    MoveMaxLeft();
        //    MoveDown();
        //}

        //public void MoveMaxRightAndUp(int viewportRowIndex)
        //{
        //    MoveUp();
        //    MoveMaxRight(viewportRowIndex);
        //}

        public void MoveToRowEnd(int viewportRowIndex)
        {
            int absoluteRowIndex = viewportRowIndex - FirstRowIndex + FirstRowAbsoluteIndex;
            int rowLength = textInfo.GetLineLength(absoluteRowIndex);

            if (rowLength == 0)
            {
                FirstColumnAbsoluteIndex = 0;
                return;
            }

            FirstColumnAbsoluteIndex = rowLength - 1;
        }
    }
}
