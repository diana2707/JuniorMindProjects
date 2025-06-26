using System;

namespace TextEditor.Domain
{
    public class Viewport
    {
        private readonly int width;
        private readonly int heigth;
        private readonly int startTextPosition;

        public Viewport(int width, int heigth, int startTextPosition)
        {
            this.width = width;
            this.heigth = heigth;
            this.startTextPosition = startTextPosition;
        }
    }
}
