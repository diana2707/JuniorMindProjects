using System;

namespace TextEditor.Domain
{
    public class InputHandler
    {
        private readonly IConsoleController controller;

        public InputHandler(IConsoleController controller)
        {
            this.controller = controller;
        }

        public string? ReadInputLine()
        {
            return controller.ReadLine();
        }
    }
}
