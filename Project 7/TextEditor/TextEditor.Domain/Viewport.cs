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

        public int GetRightEdgeColumnAbsoluteIndex(int rowIndex)
        {
            int absoluteLength = textInfo.GetLineLength(rowIndex);
            int maxRightEdgeColumnAbsoluteIndex = LeftEdgeColumnAbsoluteIndex + viewportWidth - 1;

            return maxRightEdgeColumnAbsoluteIndex < absoluteLength
                ? maxRightEdgeColumnAbsoluteIndex
                : absoluteLength - 1;
        }

        public bool IsScrolledToTextLineStart()
        {
            return LeftEdgeColumnAbsoluteIndex == 0;
        }

        public bool IsScrolledToTextLineEnd(int viewportRowIndex)
        {
            int absoluteRowIndex = GetAbsoluteRowIndex(viewportRowIndex);
            return GetRightEdgeColumnAbsoluteIndex(absoluteRowIndex) == textInfo.GetLineLength(absoluteRowIndex) - 1;
        }

        public bool IsLastTextLine(int viewportRowIndex)
        {
            int absoluteRowIndex = GetAbsoluteRowIndex(viewportRowIndex);
            return absoluteRowIndex == textInfo.GetLinesCount() - 1;
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
        }

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

                case Direction.ToRowEnd:
                    MoveToRowEnd(viewportRowIndex);
                    break;
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
            if (IsScrolledToTextLineStart())
            {
                return;
            }

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

            LeftEdgeColumnAbsoluteIndex = rowLength < viewportWidth - 1 ? 0 : rowLength - viewportWidth;
        }

        public void MoveToRowEnd(int viewportRowIndex)
        {
            int absoluteRowIndex = GetAbsoluteRowIndex(viewportRowIndex);
            int rowLength = textInfo.GetLineLength(absoluteRowIndex);

            LeftEdgeColumnAbsoluteIndex = rowLength == 0 ? 0 : rowLength - 1;
        }

        public List<string> RenderText()
        {
            List<string> result = [];

            for (int i = TopRowAbsoluteIndex; i <= BottomRowAbsoluteIndex; i++)
            {
                if (LeftEdgeColumnAbsoluteIndex > GetRightEdgeColumnAbsoluteIndex(i))
                {
                    result.Add(string.Empty);
                    continue;
                }

                result.Add(textInfo.GetLine(i)[LeftEdgeColumnAbsoluteIndex..(GetRightEdgeColumnAbsoluteIndex(i) + 1)]);
            }

            return result;
        }


        private int GetAbsoluteRowIndex(int viewportRowIndex)
        {
            return viewportRowIndex - TopRowIndex + TopRowAbsoluteIndex;
        }
    }
}
