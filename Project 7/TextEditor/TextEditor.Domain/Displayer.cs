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

        //public void ClearFrom(int startRow, int height)
        //{
        //    List<string> clearedRows = [];

        //    for (int i = 0; i < height; i++)
        //    {

        //    }
        //}

        //modifica tuplelul
        public void DisplayCursorAt((int left, int top) coordinate)
        {
            controller.SetCursorPosition(coordinate.left, coordinate.top);
        }

        public void OnViewportMoved(List<string> renderedText, (int column, int row) startPosition)
        {
            DisplayCursorAt((startPosition.column, startPosition.row));
            DisplayText(renderedText);
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
