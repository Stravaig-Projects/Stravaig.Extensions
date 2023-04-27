// ReSharper disable All
namespace Example;

internal static class CompareStrings
{
    public static void IsABeforeB()
    {
        Console.WriteLine("string.Compare(lhs, rhs, Comparison)");
        Console.WriteLine("Give me two values to compare:");

        Console.Write("LHS = ");
        string? lhs = Console.ReadLine();

        Console.Write("RHS = ");
        string? rhs = Console.ReadLine();

        if (string.Compare(lhs, rhs, StringComparison.OrdinalIgnoreCase) < 0)
            Console.WriteLine($"\"{lhs}\" precedes (is before) \"{rhs}\".");
        else
            Console.WriteLine($"\"{lhs}\" does not precede (is not before) \"{rhs}\".");
    }

    public static void IsABeforeOrEqualToB()
    {
        Console.WriteLine("string.Compare(lhs, rhs, Comparison)");
        Console.WriteLine("Give me two values to compare:");

        Console.Write("LHS = ");
        string? lhs = Console.ReadLine();

        Console.Write("RHS = ");
        string? rhs = Console.ReadLine();

        if (string.Compare(lhs, rhs, StringComparison.OrdinalIgnoreCase) <= 0)
            Console.WriteLine($"\"{lhs}\" precedes (is before) or is equal to \"{rhs}\".");
        else
            Console.WriteLine($"\"{lhs}\" does not precede (is not before) and is not equal to \"{rhs}\".");
    }

    public static void IsAAfterOrEqualToB()
    {
        Console.WriteLine("string.Compare(lhs, rhs, Comparison)");
        Console.WriteLine("Give me two values to compare:");

        Console.Write("LHS = ");
        string? lhs = Console.ReadLine();

        Console.Write("RHS = ");
        string? rhs = Console.ReadLine();

        if (string.Compare(lhs, rhs, StringComparison.OrdinalIgnoreCase) >= 0)
            Console.WriteLine($"\"{lhs}\" follows (is after) or is equal to \"{rhs}\".");
        else
            Console.WriteLine($"\"{lhs}\" does not follow (is not after) and is not equal to \"{rhs}\".");
    }

    public static void IsAAfterB()
    {
        Console.WriteLine("string.Compare(lhs, rhs, Comparison)");
        Console.WriteLine("Give me two values to compare:");

        Console.Write("LHS = ");
        string? lhs = Console.ReadLine();

        Console.Write("RHS = ");
        string? rhs = Console.ReadLine();

        if (string.Compare(lhs, rhs, StringComparison.OrdinalIgnoreCase) > 0)
            Console.WriteLine($"\"{lhs}\" follows (is after) \"{rhs}\".");
        else
            Console.WriteLine($"\"{lhs}\" does not follow (is not after) \"{rhs}\".");
    }
}