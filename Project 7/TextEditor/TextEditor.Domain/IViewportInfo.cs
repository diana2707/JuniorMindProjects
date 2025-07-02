using System;

namespace TextEditor.Domain
{
    public interface IViewportInfo
    {
        public int LastRowIndex { get; }

        public int FirstRowIndex { get; }

        public int GetRowLength(int index);
    }
}
