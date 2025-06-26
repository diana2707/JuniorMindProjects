using System;

namespace TextEditor.Domain
{
    public interface ITextInfo
    {
        public int GetLinesCount();
        public string GetLine(int index);
        public int GetLineLength(int index);
    }
}
