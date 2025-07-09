using System;

namespace TextEditor.Domain
{
    //Viewport ar trebui sa gestioneze partea vizibila (dimensiunile ferestrei si rerendering cand se depasesc limitele)
    //Viewport trebuie sa declanseze un event pt displayer de fiecare data cand apare o modificare
    //am nevoie de limitele textului pt a calcula corect ce sa afiseaza

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

        public int LastColumnAbsoluteIndex => FirstColumnAbsoluteIndex + windowWidth - 2;//la row corectez in LastRowIndex, aici corectez direct cu -2?

        public bool RowIsAtRightLimit(int index) => LastColumnAbsoluteIndex == textInfo.GetLineLength(index) - 1;

        public int GetRowLength(int index)
        {
            if (textInfo.GetLineLength(index - FirstRowIndex)  /*text[rowPosition - startTextPosition].Length*/ > windowWidth - 1)
            {
                return windowWidth - 1;
            }

            return textInfo.GetLineLength(index - FirstRowIndex)/*text[rowPosition - startTextPosition].Length*/;
        }

        //cum pot sa fac renderul flezibil?
        public List<string> RenderText()
        {
            List<string> result = [];

            //int textHeigth = textInfo.GetLinesCount() < windowHeigth - FirstRowIndex
            //    ? textInfo.GetLinesCount()
            //    : windowHeigth - FirstRowIndex - 1;

            for (int i = FirstRowAbsoluteIndex /*0*/; i <= LastRowAbsoluteIndex /*textHeigth*/; i++)// in loc de heigth sa folosesc last index?
            {
                result.Add((textInfo.GetLineLength(i) > windowWidth - 1
                    ? textInfo.GetLine(i)[FirstColumnAbsoluteIndex..(LastColumnAbsoluteIndex + 1)/*(windowWidth - 1)*/]
                    : textInfo.GetLine(i)));
            }

            return result;
        }


        //inainte de rerender thebuie sa setez cursorul la inceputul textlui
        public void OnCursorExitedView(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    MoveRight();
                    break;

                case Direction.Left:
                    MoveRight();
                    break;

                case Direction.Down:
                    MoveDown();
                    break;

                case Direction.Up:
                    MoveUp();
                    break;
            }

            List<string> renderedText = RenderText();
            ViewportMoved?.Invoke(renderedText, (column: 0, row: FirstRowIndex));
        }

        public void MoveRight()
        {
            FirstColumnAbsoluteIndex++;
        }

        public void MoveLeft()
        {
            FirstColumnAbsoluteIndex--;
        }

        public void MoveDown()
        {
            if(LastRowAbsoluteIndex == textInfo.GetLinesCount() - 1)
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

    }
}
