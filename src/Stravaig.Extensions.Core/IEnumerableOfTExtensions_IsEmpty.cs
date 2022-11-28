using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Stravaig.Extensions.Core;

/// <summary>
/// Extensions to detect emptiness.
/// </summary>
// ReSharper disable once InconsistentNaming
public static class IEnumerableOfTExtensions_IsEmpty
{
    /// <summary>
    /// Detects whether the source is empty or not.
    /// </summary>
    /// <param name="source">A source sequence</param>
    /// <typeparam name="T">The type of object in the source</typeparam>
    /// <returns>A Boolean, true if the source is empty, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown if source is null.</exception>
    [Pure]
    public static bool IsEmpty<T>(this IEnumerable<T> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return !source.Any();
    }

    /// <summary>
    /// Detects whether the source is empty or null.
    /// </summary>
    /// <param name="source">A source sequence</param>
    /// <typeparam name="T">The type of object in the source</typeparam>
    /// <returns>A Boolean, true if the source is empty or null, false otherwise.</returns>
    [Pure]
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
    {
        return source == null || source.IsEmpty();
    }
}