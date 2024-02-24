using System;

namespace Jason.Jason
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Provide a file path");
            }

            var jasonValue = new Value();
            string sample = File.ReadAllText(args[0]);
            Console.WriteLine(jasonValue.Match(sample).Success());
        }
    }
}
