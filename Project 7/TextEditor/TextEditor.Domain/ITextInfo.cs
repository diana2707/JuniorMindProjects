using System;

namespace TextEditor.Domain
{
    public interface ITextInfo
    {
        public int GetRowsCount();
        public int GetRow(int rowIndex);
    }
}
