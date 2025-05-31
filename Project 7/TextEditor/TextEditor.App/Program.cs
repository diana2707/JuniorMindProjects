using TextEditor.Domain;

namespace TextEditor.App
{
    class Program
    {
        static void Main(string[] args)
        {
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

            Cursor cursor = new(manager, Console.GetCursorPosition().Top);
            Console.WriteLine(manager.ReadFile());

            bool isListening = true;

            while (isListening)
            {
                ConsoleKeyInfo currentKey = Console.ReadKey();
                switch (currentKey.Key)
                {
                    case ConsoleKey.RightArrow:
                        cursor.MoveRight();
                        Console.SetCursorPosition(cursor.Column, cursor.Row);
                        continue;

                    case ConsoleKey.LeftArrow:
                        cursor.MoveLeft();
                        Console.SetCursorPosition(cursor.Column, cursor.Row);
                        continue;

                    case ConsoleKey.UpArrow:
                        cursor.MoveUp();
                        Console.SetCursorPosition(cursor.Column, cursor.Row);
                        continue;

                    case ConsoleKey.DownArrow:
                        cursor.MoveDown();
                        Console.SetCursorPosition(cursor.Column, cursor.Row);
                        continue;

                    case ConsoleKey.Escape:
                        isListening = false;
                        return;

                    default:
                        continue;
                }
            }
        }
    }
    
}

