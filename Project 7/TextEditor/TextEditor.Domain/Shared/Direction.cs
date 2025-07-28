using System;

namespace TextEditor.Domain.Shared
{
    [Flags]
    public enum Direction
    {
        None = 0,
        Right = 2,
        Left = 4,
        Down = 8,
        Up = 16,
        MaxRight = 32,
        MaxLeft = 64,
        ToRowEnd = 128,
    }

}
