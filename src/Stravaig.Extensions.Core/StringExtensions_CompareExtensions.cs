using System;
using System.Diagnostics.Contracts;

namespace Stravaig.Extensions.Core;

/// <summary>
/// String extensions that extend the comparison functionality.
/// </summary>
// ReSharper disable once InconsistentNaming
public static class StringExtensions_CompareExtensions
{
    /// <summary>
    /// Determines whether lhs comes after rhs given the type of comparison used.
    /// </summary>
    /// <param name="lhs">The string on the left hand side of the comparison.</param>
    /// <param name="rhs">The string on the right hand side of the comparison.</param>
    /// <param name="comparisonType">One of the enumeration values that
    /// specifies the rules to use in the comparison.</param>
    /// <returns>True if the left string comes after the right string.</returns>
    [Pure]
    public static bool IsAfter(this string lhs, string rhs, StringComparison comparisonType)
    {
        return string.Compare(lhs, rhs, comparisonType) > 0;
    }

    /// <summary>
    /// Determines whether lhs comes after or is equal to the rhs given the type
    /// of comparison used.
    /// </summary>
    /// <param name="lhs">The string on the left hand side of the comparison.</param>
    /// <param name="rhs">The string on the right hand side of the comparison.</param>
    /// <param name="comparisonType">One of the enumeration values that
    /// specifies the rules to use in the comparison.</param>
    /// <returns>True if the left string comes after the right string.</returns>
    [Pure]
    public static bool IsAfterOrEqualTo(this string lhs, string rhs, StringComparison comparisonType)
    {
        return string.Compare(lhs, rhs, comparisonType) >= 0;
    }
    
    /// <summary>
    /// Determines whether lhs comes before rhs given the type of comparison used.
    /// </summary>
    /// <param name="lhs">The string on the left hand side of the comparison.</param>
    /// <param name="rhs">The string on the right hand side of the comparison.</param>
    /// <param name="comparisonType">One of the enumeration values that
    /// specifies the rules to use in the comparison.</param>
    /// <returns>True if the left string comes before the right string.</returns>
    [Pure]
    public static bool IsBefore(this string lhs, string rhs, StringComparison comparisonType)
    {
        return string.Compare(lhs, rhs, comparisonType) < 0;
    }

    /// <summary>
    /// Determines whether lhs comes before or is equal to the rhs given the
    /// type of comparison used.
    /// </summary>
    /// <param name="lhs">The string on the left hand side of the comparison.</param>
    /// <param name="rhs">The string on the right hand side of the comparison.</param>
    /// <param name="comparisonType">One of the enumeration values that
    /// specifies the rules to use in the comparison.</param>
    /// <returns>True if the left string comes before or is equal to the right
    /// string.</returns>
    [Pure]
    public static bool IsBeforeOrEqualTo(this string lhs, string rhs, StringComparison comparisonType)
    {
        return string.Compare(lhs, rhs, comparisonType) <= 0;
    }
    
    
}