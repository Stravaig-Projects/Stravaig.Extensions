using Stravaig.Extensions.Core;

namespace Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetInputCheckingWithHasContent();
            GetInputCheckingWithStringIsNullOrWhiteSpace();
        }

        private static void GetInputCheckingWithHasContent()
        {
            Console.WriteLine("HasContent");
            Console.WriteLine("Type something and press Enter!");
            Console.Write("> ");
            string? input = Console.ReadLine();
            if (input.HasContent())
            {
                Console.WriteLine($"You inputted: {input}");
            }
            else
            {
                Console.WriteLine("You did not input anything.");
            }
        }


        private static void GetInputCheckingWithStringIsNullOrWhiteSpace()
        {
            // This method should be picked up by the Stravaig.Extensions.Core.Analyzer
            // It should detect that
            // !string.IsNullOrWhiteSpace(input)
            // can be replaced by
            // input.HasContent()
            Console.WriteLine("!string.IsNUllOrWhiteSpace");
            Console.WriteLine("Type something and press Enter!");
            Console.Write("> ");
            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine($"You inputted: {input}");
            }
            else
            {
                Console.WriteLine("You did not input anything.");
            }
        }
    }
}