using System;

namespace TextEditor.Domain
{
    public class Displayer
    {
        readonly IConsoleController controller;

        public Displayer(IConsoleController controller)
        {
            this.controller = controller;
        }

        public void DisplayLine(string line)
        {
            controller.WriteLine(line);
        }

        public void DisplayText(List<string> text)
        {
            foreach (var line in text)
            {
                controller.WriteLine(line);
            }
        }

        public void DisplayEmptyLine()
        {
            controller.WriteLine();
        }

        public void ClearDisplay()
        {
            controller.Clear();
        }

        public void ClearFrom(int startRow)
        {
            List<string> clearedRows = [];

            for (int i = 0; i < (controller.Height - startRow - 1); i++)
            {
                clearedRows.Add(new string(' ', controller.Width));
            }

            DisplayCursorAt(0, startRow);
            DisplayText(clearedRows);
            DisplayCursorAt(0, startRow);
        }

        public void DisplayCursorAt(int column, int row)
        {
            controller.SetCursorPosition(column, row);
        }

        public void OnViewportMoved(List<string> renderedText, (int column, int row) startPosition)
        {
            ClearFrom(startPosition.row);
            DisplayText(renderedText);
        }

        public void OnCursorMoved((int column, int row) cursorPosition)
        {
            DisplayCursorAt(cursorPosition.column, cursorPosition.row);
        }
    }
}