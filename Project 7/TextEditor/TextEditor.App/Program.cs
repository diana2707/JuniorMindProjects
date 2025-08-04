using TextEditor.Domain.CursorLogic;
using TextEditor.Domain.Controller;
using TextEditor.Domain.Display;
using TextEditor.Domain.Editor;
using TextEditor.Domain.Input;
using TextEditor.Domain.Shared;

namespace TextEditor.App
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManager manager = null;
            ConsoleController controller = new();
            InputHandler inputHandler = new(controller);
            Displayer displayer = new(controller);

            displayer.ClearDisplay();
            displayer.DisplayLine("To read file insert path:");
            

            while (manager == null)
            {
                try
                {
                    string path = inputHandler.ReadInputLine();
                    manager = new FileManager(path);
                }
                catch (Exception exception)
                {
                    displayer.DisplayLine("The path you inserted was not valid. Please insert a correct path.");
                }
            }

            displayer.DisplayEmptyLine();

            TextManager textManager = new(manager.Text);
            Viewport viewport = new(Console.WindowWidth, Console.WindowHeight, Console.CursorTop, textManager);
            Cursor cursor = new(viewport);

            EditorController editorController = new(
                manager,
                textManager,
                inputHandler,
                displayer,
                viewport,
                cursor);

            editorController.Run();
        }
    }
    
}

