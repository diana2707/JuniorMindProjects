using TextEditor.Domain;

namespace TextEditor.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("To read file insert path:");

            FileManager manager = null;

            while (manager == null)
            {
                try
                {
                    string path = Console.ReadLine();
                    manager = new FileManager(path);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("The path you inserted was not valid. Please insert a correct path.");
                }
            }

            Console.WriteLine();

            TextFormatter formatter = new(manager.Text, Console.WindowWidth, Console.WindowHeight, Console.CursorTop);
            Cursor cursor = new(formatter);

            List<string> formattedText = formatter.FormatWithinWindow();
            foreach(var line in formattedText)
            {
                Console.WriteLine(line);
            }

            //cursor.OnMove += formatter.ScrollWhatever(row, column)
            
            bool isListening = true;

            while (isListening)
            {
                Console.SetCursorPosition(cursor.Column, cursor.Row);
                ConsoleKeyInfo currentKey = Console.ReadKey(intercept: true);

                switch (currentKey.Key)
                {
                    case ConsoleKey.RightArrow:
                        cursor.MoveRight();

                        continue;

                    case ConsoleKey.LeftArrow:
                        cursor.MoveLeft();
                        continue;

                    case ConsoleKey.UpArrow:
                        cursor.MoveUp();
                        continue;

                    case ConsoleKey.DownArrow:
                        cursor.MoveDown();
                        continue;

                    case ConsoleKey.Escape:
                        isListening = false;
                        cursor.SetToStartOfLastLine();
                        Console.SetCursorPosition(cursor.Column, cursor.Row);
                        continue;

                    default:
                        continue;
                }
            }
        }
    }
    
}

