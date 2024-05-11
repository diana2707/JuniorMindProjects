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
            IMatch match = jasonValue.Match(new StringView(sample, 0));

            Console.WriteLine(match.Success() && match.StringView().IsEmpty()
                ? "Jason is valid"
                : "Jason is invalid");
        }
    }
}