using System;

namespace TextEditor.Domain
{
    //TextManager ar trebui sa gestioneze intregul text + pozitia absoluta a cursorului in text
    

    public class TextManager : ITextInfo
    {
        readonly string[] text;
        
        public TextManager(string[] text)
        {
            this.text = text;
        }

        public (int row, int column) AbsoluteCursorPosition { get; private set; } = (0, 0);

        public int GetLinesCount() => text.Length;

        public string GetLine(int index) => text[index];

        public int GetLineLength(int index) => text[index].Length;


        //ORIGINAL IMPLEMENTATION

        //public int FirstDisplayedRowPosition => startingRow;

        //public int LastDisplayedRowPosition
        //{
        //    get
        //    {
        //        if (startingRow + text.Length >= windowHeigth)
        //        {
        //            return windowHeigth - 2;
        //        }

        //        return startingRow + text.Length - 1;
        //    }
        //}

        //public int GetDisplayedRowLength(int rowPosition)
        //{
        //    if (text[rowPosition - startingRow].Length > windowWidth - 1)
        //    {
        //        return windowWidth - 1;
        //    }

        //    return text[rowPosition - startingRow].Length;
        //}

        ////Format within window 
        //public List<string> FormatWithinWindow()
        //{
        //    List<string> result = [];

        //    int textHeigth = text.Length < windowHeigth - startingRow ? text.Length : windowHeigth - startingRow - 1;

        //    for (int i = 0; i < textHeigth; i++)
        //    {
        //        result.Add((text[i].Length > windowWidth - 1 ? text[i][..(windowWidth - 1)] : text[i]));
        //    }

        //    return result;
        //}

    }
}
