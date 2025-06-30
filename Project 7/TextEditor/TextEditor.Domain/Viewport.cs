using System;
using static System.Net.Mime.MediaTypeNames;

namespace TextEditor.Domain
{
    //Viewport ar trebui sa gestioneze partea vizibila (dimensiunile ferestrei si rerendering cand se depasesc limitele)
    //am nevoie de limitele textului pt a calcula corect ce sa afiseaza

    public class Viewport /*: ITextInfo*/
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
    }
}
