using System;

namespace TextEditor.Domain
{
    //Viewport ar trebui sa gestioneze partea vizibila (dimensiunile ferestrei si rerendering cand se depasesc limitele)
    //Viewport trebuie sa declanseze un event pt displayer de fiecare data cand apare o modificare
    //am nevoie de limitele textului pt a calcula corect ce sa afiseaza

    public class Viewport : IViewportInfo
    {
        private readonly int width;
        private readonly int heigth;
        private readonly int startTextPosition;
        private readonly ITextInfo textInfo;

        public Viewport(int width, int heigth, int startTextPosition, ITextInfo textInfo)
        {
            this.width = width;
            this.heigth = heigth;
            this.startTextPosition = startTextPosition;
            this.textInfo = textInfo;
        }

        public int FirstRowPosition => startTextPosition;

        public int LastRowPosition
        {
            get
            {
                if (startTextPosition + textInfo.GetLinesCount() >= heigth)
                {
                    return heigth - 2;
                }

                return startTextPosition + textInfo.GetLinesCount() - 1;
            }
        }

        public int GetRowLength(int index)
        {
            if (textInfo.GetLineLength(index - startTextPosition)  /*text[rowPosition - startTextPosition].Length*/ > width - 1)
            {
                return width - 1;
            }

            return textInfo.GetLineLength(index - startTextPosition)/*text[rowPosition - startTextPosition].Length*/;
        }

        //adu logica din textManager pt inceput
        public List<string> RenderText()
        {
            List<string> result = [];

            int textHeigth = textInfo.GetLinesCount() /*text.Length*/ < heigth /*windowHeigth*/ - startTextPosition /*startingRow*/ ? textInfo.GetLinesCount() /*text.Length*/ : heigth /*windowHeigth*/ - startTextPosition /*startingRow*/ - 1;

            for (int i = 0; i < textHeigth; i++)
            {
                result.Add((textInfo.GetLineLength(i) /*text[i].Length*/ > width /*windowWidth*/ - 1 ? textInfo.GetLine(i)[..(width - 1)] /*text[i][..(windowWidth - 1)]*/ : textInfo.GetLine(i)/*text[i]*/));
            }

            return result;
        }
    }
}
