using System;

namespace TextEditor.Domain
{
    public interface IViewportInfo
    {
        public int LastRowIndex { get; }

        public int FirstRowIndex { get; }

        public bool IsRightmostLimit(int index);

        public bool IsDownmostLimit(int index);

        public int GetViewportRowLength(int index);
    }
}
