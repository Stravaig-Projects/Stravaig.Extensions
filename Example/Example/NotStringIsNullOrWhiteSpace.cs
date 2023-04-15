namespace Example
{
    internal static class NotStringIsNullOrWhiteSpace
    {

        internal static void GetInput()
        {
            // This method should be picked up by the Stravaig.Extensions.Core.Analyzer
            // It should detect that
            // !string.IsNullOrWhiteSpace(input)
            // can be replaced by
            // input.HasContent()
            // It should also suggest the inclusion of the Stravaig.Extensions.Core namespace
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