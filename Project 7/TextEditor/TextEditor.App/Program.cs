using TextEditor.Domain;

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
            
            List<string> renderedText = viewport.RenderText();

            inputHandler.ArrowKeyPressed += cursor.OnArrowKeyPressed;
            inputHandler.EscapeKeyPressed += cursor.OnEscapeKeyPressed;

            cursor.CursorMoved += displayer.DisplayCursorAt; //fa o metoda OnCursorMoved
            cursor.CursorExitedView += viewport.OnCursorExitedView;

            viewport.ViewportMoved += displayer.OnViewportMoved;

            displayer.DisplayText(renderedText);
            cursor.SetInitialPosition();
            inputHandler.Listen();
        }
    }
    
}

