using System;

namespace TextEditor.Domain
{
    public interface IViewportInfo
    {
        public int LastRowIndex { get; }

        public int FirstRowIndex { get; }

        public bool IsRightmostLimitVisible(int index);

        public bool IsLeftmostLimitVisible();

        public bool IsDownmostLimitVisible(int index);

        public int GetViewportRowLength(int index);

        //public bool IsLeftmostOutsideViewport();
    }
}
