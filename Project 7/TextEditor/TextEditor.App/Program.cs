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
            Console.WriteLine(manager.ReadFile());
        }
    }
    
}

