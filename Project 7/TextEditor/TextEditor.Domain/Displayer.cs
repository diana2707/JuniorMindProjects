using System;

namespace TextEditor.Domain
{
    public class Displayer
    {
        readonly TextWriter output;

        public Displayer(TextWriter output)
        {
            this.output = output;
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
