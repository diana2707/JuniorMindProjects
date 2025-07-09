using System;

namespace TextEditor.Domain
{
    public interface IViewportInfo
    {
        public int LastRowIndex { get; }

        public int FirstRowIndex { get; }

        public bool IsRightTextLimit { get; }

        public int GetRowLength(int index);
    }
}
