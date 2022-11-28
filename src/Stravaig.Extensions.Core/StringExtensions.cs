using System.Diagnostics.Contracts;

namespace Stravaig.Extensions.Core;

/// <summary>
/// Extends the string class
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Indicates whether the string has content or not.
    /// </summary>
    /// <param name="target">The string to test</param>
    /// <returns>True if the string has content, false if the string is null, empty or consists only of whitespace.</returns>
    [Pure]
    public static bool HasContent(this string target)
    {
        return !string.IsNullOrWhiteSpace(target);
    }
}