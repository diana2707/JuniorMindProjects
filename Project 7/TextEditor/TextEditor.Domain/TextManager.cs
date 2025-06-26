using System;

namespace TextEditor.Domain
{
    //TextManager ar trebui sa gestioneze intregul text + pozitia absoluta a cursorului in text
    //Viewport ar trebui sa gestioneze partea vizibila (dimensiunile ferestrei si rerendering cand se depasesc limitele)

    public class TextManager
    {
        readonly string[] text;
        readonly int windowWidth;
        readonly int windowHeigth;
        readonly int startingRow;

        public TextManager(string[] text, int windowWidth, int windowHeigth, int startingRow)
        {
            this.text = text;
            this.windowWidth = windowWidth;
            this.windowHeigth = windowHeigth;
            this.startingRow = startingRow;
        }

        public int FirstDisplayedRowPosition => startingRow;

        public int LastDisplayedRowPosition
        {
            get
            {
                if (startingRow + text.Length >= windowHeigth)
                {
                    return windowHeigth - 2;
                }

                return startingRow + text.Length - 1;
            }
        }

        public int GetDisplayedRowLength(int rowPosition)
        {
            if (text[rowPosition - startingRow].Length > windowWidth - 1)
            {
                return windowWidth - 1;
            }

            return text[rowPosition - startingRow].Length;
        }

        //Format within window 
        public List<string> FormatWithinWindow()
        {
            List<string> result = [];

            int textHeigth = text.Length < windowHeigth - startingRow ? text.Length : windowHeigth - startingRow - 1;

            for (int i = 0; i < textHeigth; i++)
            {
                result.Add((text[i].Length > windowWidth - 1 ? text[i][..(windowWidth - 1)] : text[i]));
            }

            return result;
        }

        //UpdateTextView(Cursor.Direction direction)
    }
}
