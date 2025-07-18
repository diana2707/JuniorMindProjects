using System;

namespace TextEditor.Domain
{
    public class Viewport : IViewportInfo
    {
        private readonly int viewportWidth;
        private readonly int viewportHeigth;
        private readonly ITextInfo textInfo;

        public Viewport(int windowWidth, int windowHeigth, int startRowIndex, ITextInfo textInfo)
        {
            this.viewportWidth = windowWidth - 1;
            this.viewportHeigth = windowHeigth - 1;
            this.textInfo = textInfo;

            TopRowIndex = startRowIndex;
        }

        public event Action<List<string>, (int column, int row)> ViewportMoved;

        public int TopRowIndex { get; }

        public int BottomRowIndex
        {
            get
            {
                int textLength = textInfo.GetLinesCount();
                return TopRowIndex + textLength > viewportHeigth
                    ? viewportHeigth - 1
                    : TopRowIndex + textLength - 1;
            }
        }

        public int TopRowAbsoluteIndex { get; private set; } = 0;

        public int BottomRowAbsoluteIndex => GetAbsoluteRowIndex(BottomRowIndex);

        public int LeftEdgeColumnAbsoluteIndex { get; private set; } = 0;

        //pune conditie sa nu depaseasca lungimea randului
        public int GetRightEdgeColumnAbsoluteIndex(int rowIndex)
        {
            int absoluteLength = textInfo.GetLineLength(rowIndex);

            if (absoluteLength == 0)
            {
                return -1;
            }

            if (LeftEdgeColumnAbsoluteIndex + viewportWidth - 1 < absoluteLength)
            {
                return LeftEdgeColumnAbsoluteIndex + viewportWidth - 1;
            }

            return absoluteLength - 1;

        }

        //la row corectez in LastRowIndex, aici corectez direct cu -2?

        public bool IsScrolledToTextLineEnd(int viewportRowIndex)
        {
            int absoluteRowIndex = GetAbsoluteRowIndex(viewportRowIndex);
            return GetRightEdgeColumnAbsoluteIndex(absoluteRowIndex) == textInfo.GetLineLength(absoluteRowIndex) - 1;
        }

        public bool IsLastTextLine(int viewportRowIndex)
        {
            int absoluteRowIndex = GetAbsoluteRowIndex(viewportRowIndex);  //calculeaza gresit pt cand randul este scrollat...
            return absoluteRowIndex == textInfo.GetLinesCount() - 1;
        }

        public bool IsScrolledToTextLineStart()
        {
            return LeftEdgeColumnAbsoluteIndex == 0;
        }

        public bool IsFirstTextLine(int viewportRowIndex)
        {
            int absoluteRowIndex = GetAbsoluteRowIndex(viewportRowIndex);
            return absoluteRowIndex == 0;
        }

        public int GetVisibleRowLength(int viewportRowIndex)
        {
            int absoluteRowIndex = viewportRowIndex - TopRowIndex + TopRowAbsoluteIndex;
            return GetRightEdgeColumnAbsoluteIndex(absoluteRowIndex) - LeftEdgeColumnAbsoluteIndex + 1;

            //if (textInfo.GetLineLength(index - FirstRowIndex)  /*text[rowPosition - startTextPosition].Length*/ > windowWidth - 1)
            //{
            //    return windowWidth - 1;
            //}

            //return textInfo.GetLineLength(index - FirstRowIndex)/*text[rowPosition - startTextPosition].Length*/;
        }

        public List<string> RenderText()
        {
            List<string> result = [];

            //int textHeigth = textInfo.GetLinesCount() < windowHeigth - FirstRowIndex
            //    ? textInfo.GetLinesCount()
            //    : windowHeigth - FirstRowIndex - 1;

            for (int i = TopRowAbsoluteIndex; i <= BottomRowAbsoluteIndex; i++)
            {
                if (GetRightEdgeColumnAbsoluteIndex(i) == -1 || LeftEdgeColumnAbsoluteIndex > GetRightEdgeColumnAbsoluteIndex(i))
                {
                    result.Add(string.Empty);
                    continue;
                }

                result.Add(textInfo.GetLine(i)[LeftEdgeColumnAbsoluteIndex..(GetRightEdgeColumnAbsoluteIndex(i) + 1)]);
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

                //case Direction.Down | Direction.ToRowEnd:
                //    MoveDown();
                //    MoveToRowEnd(viewportRowIndex);
                //    break;
            }

            List<string> renderedText = RenderText();
            ViewportMoved?.Invoke(renderedText, (column: 0, row: TopRowIndex));
        }

        public void MoveRight(int viewportRowIndex)
        {
            if (IsScrolledToTextLineEnd(viewportRowIndex))
            {
                return;
            }

            LeftEdgeColumnAbsoluteIndex++;
        }

        public void MoveLeft()
        {
            LeftEdgeColumnAbsoluteIndex--;
        }

        public void MoveDown()
        {
            if (BottomRowAbsoluteIndex == textInfo.GetLinesCount() - 1)
            {
                return;
            }

            TopRowAbsoluteIndex++;
        }

        public void MoveUp()
        {
            if (TopRowAbsoluteIndex == 0)
            {
                return;
            }

            TopRowAbsoluteIndex--;
        }

        public void MoveMaxLeft()
        {
            LeftEdgeColumnAbsoluteIndex = 0;
        }

        public void MoveMaxRight(int viewportRowIndex)
        {
            int absoluteRowIndex = GetAbsoluteRowIndex(viewportRowIndex);
            int rowLength = textInfo.GetLineLength(absoluteRowIndex);

            //am nevoie de asta si in MoveMaxLeft? de ce?
            if (rowLength < viewportWidth - 1)
            {
                LeftEdgeColumnAbsoluteIndex = 0;
                return;
            }

            LeftEdgeColumnAbsoluteIndex = rowLength - 1 - viewportWidth + 1;
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
            int absoluteRowIndex = GetAbsoluteRowIndex(viewportRowIndex);
            int rowLength = textInfo.GetLineLength(absoluteRowIndex);

            if (rowLength == 0)
            {
                LeftEdgeColumnAbsoluteIndex = 0;
                return;
            }

            LeftEdgeColumnAbsoluteIndex = rowLength - 1;
        }

        private int GetAbsoluteRowIndex(int viewportRowIndex)
        {
            return viewportRowIndex - TopRowIndex + TopRowAbsoluteIndex;
        }
    }
}
