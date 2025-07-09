using System;

namespace TextEditor.Domain
{
    public interface IViewportInfo
    {
        public int LastRowIndex { get; }

        public int FirstRowIndex { get; }

        public bool RowIsAtRightLimit(int index);

        public int GetRowLength(int index);
    }
}
