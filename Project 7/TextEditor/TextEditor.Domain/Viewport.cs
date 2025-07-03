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

        private readonly int firstRowIndex;

        public Viewport(int windowWidth, int windowHeigth, int firstRowIndex, ITextInfo textInfo)
        {
            this.windowWidth = windowWidth;
            this.windowHeigth = windowHeigth;
            this.firstRowIndex = firstRowIndex;
            this.textInfo = textInfo;

            //FirstRowIndex = startTextPosition;
            //LastRowIndex = InitializeLastRowIndex();
            //FirstColumnIndex = 0;
            //LastColumnIndex = 118;
        }

        public int FirstRowIndex => firstRowIndex;

        public int LastRowIndex
        {
            get
            {
                if (firstRowIndex + textInfo.GetLinesCount() >= windowHeigth)
                {
                    return windowHeigth - 2;
                }

                return firstRowIndex + textInfo.GetLinesCount() - 1;
            }
        }

        public int FirstRowAbsoluteIndex { get; private set; } = 0;
        public int LastRowAbsoluteIndex { get; private set; } = 0;

        public int FirstColumnIndex { get; private set;}

        public int LastColumnIndex { get; private set;}

        public int GetRowLength(int index)
        {
            if (textInfo.GetLineLength(index - firstRowIndex)  /*text[rowPosition - startTextPosition].Length*/ > windowWidth - 1)
            {
                return windowWidth - 1;
            }

            return textInfo.GetLineLength(index - firstRowIndex)/*text[rowPosition - startTextPosition].Length*/;
        }

        public List<string> RenderText()
        {
            List<string> result = [];

            int textHeigth = textInfo.GetLinesCount() /*text.Length*/ < windowHeigth /*windowHeigth*/ - firstRowIndex /*startingRow*/ ? textInfo.GetLinesCount() /*text.Length*/ : windowHeigth /*windowHeigth*/ - firstRowIndex /*startingRow*/ - 1;

            for (int i = 0; i < textHeigth; i++)
            {
                result.Add((textInfo.GetLineLength(i) /*text[i].Length*/ > windowWidth /*windowWidth*/ - 1 ? textInfo.GetLine(i)[..(windowWidth - 1)] /*text[i][..(windowWidth - 1)]*/ : textInfo.GetLine(i)/*text[i]*/));
            }

            return result;
        }

        //public void OnCursorExitedView(Direction direction)
        //{

        //}

        //public void MoveRight()
        //{
        //    FirstColumnIndex++;
        //    LastColumnIndex++;

            
        //}

        //public void MoveLeft()
        //{
        //    FirstColumnIndex--;
        //    LastColumnIndex--;
        //}

        //public void MoveDown()
        //{
        //    FirstRowIndex++;
        //    LastRowIndex++;
        //}

        //public void MoveUp()
        //{
        //    FirstRowIndex--;
        //    LastRowIndex--;
        //}


    }
}
