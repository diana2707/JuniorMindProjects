using System;

namespace TextEditor.Domain
{
    public interface IViewportInfo
    {
        public int LastRowPosition { get; }

        public int FirstRowPosition { get; }

        public int GetRowLength(int index);
    }
}
