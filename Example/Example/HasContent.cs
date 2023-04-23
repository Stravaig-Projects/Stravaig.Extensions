using Stravaig.Extensions.Core;

namespace Example
{
    internal static class HasContent
    {

        internal static void GetInput()
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
    }
}