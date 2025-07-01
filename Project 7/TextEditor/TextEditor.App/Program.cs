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

            TextManager textManager = new(manager.Text, Console.WindowWidth, Console.WindowHeight, Console.CursorTop);
            Viewport viewport = new(Console.WindowWidth, Console.WindowHeight, Console.CursorTop, textManager);
            Cursor cursor = new(viewport);
            List<string> renderedText = viewport.RenderText();

            inputHandler.ArrowKeyPressed += cursor.OnArrowKeyPressed;
            cursor.CursorMoved += displayer.DisplayCursorAt;

            displayer.DisplayEmptyLine();
            displayer.DisplayText(renderedText);
            inputHandler.Listen();



            //THIS LOGIC IS FOR LATTER

            //inputHandler.ArrowKeyPressed += cursor.OnArrowKeyPressed; 



            //ORIGINAL LOGIC
            //bool isListening = true;

            //while (isListening)
            //{
            //    displayer.DisplayCursorAt(cursor.Column, cursor.Row);
            //    ConsoleKeyInfo currentKey = inputHandler.ReadKeyWithoutDisplay();

            //    switch (currentKey.Key)
            //    {
            //        case ConsoleKey.RightArrow:
            //            cursor.MoveRight();

            //            continue;

            //        case ConsoleKey.LeftArrow:
            //            cursor.MoveLeft();
            //            continue;

            //        case ConsoleKey.UpArrow:
            //            cursor.MoveUp();
            //            continue;

            //        case ConsoleKey.DownArrow:
            //            cursor.MoveDown();
            //            continue;

            //        case ConsoleKey.Escape:
            //            isListening = false;
            //            cursor.SetToStartOfLastLine();
            //            displayer.DisplayCursorAt(cursor.Column, cursor.Row);
            //            continue;

            //        default:
            //            continue;
            //    }
            //}
        }
    }
    
}

