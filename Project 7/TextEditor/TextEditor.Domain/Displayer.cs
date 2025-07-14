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

        //should the parameter be array?
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

        //modifica tuplelul
        public void DisplayCursorAt(int column, int row)
        {
            controller.SetCursorPosition(column, row);
        }

        public void OnViewportMoved(List<string> renderedText, (int column, int row) startPosition)
        {
            DisplayCursorAt(startPosition.column, startPosition.row); //cred ca poate fi sters pt ca seteaza si in clear from cursorul la inceput
            ClearFrom(startPosition.row);
            DisplayText(renderedText);
            //display cursor at the end of row Cursor.SetInitialPosition...
        }

        public void OnCursorMoved((int column, int row) cursorPosition)
        {
            DisplayCursorAt(cursorPosition.column, cursorPosition.row);
        }
    }
}

//class ConsoleRenderer
//{
//    private readonly TextWriter _output;

//    public ConsoleRenderer(TextWriter output)
//    {
//        _output = output;
//    }

//    public void Render(string message)
//    {
//        _output.WriteLine(message);
//    }
//}

//var renderer = new ConsoleRenderer(Console.Out);

//var output = new StringWriter();
//var renderer = new ConsoleRenderer(output);

//renderer.Render("Hello");
//Assert.Equal("Hello" + Environment.NewLine, output.ToString());
