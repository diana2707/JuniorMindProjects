using System;

namespace TextEditor.Domain
{
    public interface IViewportInfo
    {
        public int FirstViewportRowIndex { get; }

        public int LastViewportRowIndex { get; }

        public bool IsScrolledToTextLineEnd(int rowIndex);

        public bool IsScrolledToTextLineStart();

        public bool IsLastTextLine(int rowIndex);

        public bool IsFirstTextLine(int rowIndex);

        public int GetViewportRowLength(int rowIndex);
    }
}
