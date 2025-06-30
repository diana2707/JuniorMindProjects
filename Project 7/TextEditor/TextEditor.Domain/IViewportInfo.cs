using System;

namespace TextEditor.Domain
{
    public interface IViewportInfo
    {
        public int LastRowPosition { get; set; }

        public int GetRowLength(int index);
    }
}
