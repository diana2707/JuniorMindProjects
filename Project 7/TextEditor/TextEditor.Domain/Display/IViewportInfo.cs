using System;

namespace TextEditor.Domain.Display
{
    public interface IViewportInfo
    {
        public int TopRowIndex { get; }

        public int BottomRowIndex { get; }

        public bool IsScrolledToTextLineEnd(int rowIndex);

        public bool IsScrolledToTextLineStart();

        public bool IsLastTextLine(int rowIndex);

        public bool IsFirstTextLine(int rowIndex);

        public int GetVisibleRowLength(int rowIndex);
    }
}
