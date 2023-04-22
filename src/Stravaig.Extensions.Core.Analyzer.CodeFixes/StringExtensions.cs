using System;

namespace Stravaig.Extensions.Core.Analyzer;

internal static class StringExtensions
{
    public static bool IsAfter(this string lhs, string rhs, StringComparison comparisonType)
    {
        return string.Compare(lhs, rhs, comparisonType) > 0;
    }
}