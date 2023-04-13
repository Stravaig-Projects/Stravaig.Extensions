using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Stravaig.Extensions.Core;

// Named after the interface + Method
// ReSharper disable once InconsistentNaming
/// <summary>
/// Extensions to IEnumerable&lt;KeyValuePair&lt;TKey, TValue>> so that the
/// common value factories for the Key and Value don't have to be manually
/// specified.
/// </summary>
public static class IEnumerableOfKeyValuePairExtensions_ToDictionary
{
    /// <summary>
    /// Creates a Dictionary&lt;TKey,TValue> from an IEnumerable&lt;KeyValuePair&lt;TKey, TValue>>.
    /// </summary>
    /// <param name="keyValuePairs">The IEnumerable&lt;KeyValuePair&lt;TKey, TValue>> to use to form the dictionary.</param>
    /// <typeparam name="TKey">The type used for the dictionary key.</typeparam>
    /// <typeparam name="TValue">The type used for the dictionary value.</typeparam>
    /// <returns>A Dictionary&lt;TKey,TValue> that contains keys and values selected from the input sequence.</returns>
    [Pure]
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
    {
        if (keyValuePairs == null) throw new ArgumentNullException(nameof(keyValuePairs));

        return keyValuePairs.ToDictionary(
            static kvp => kvp.Key,
            static kvp => kvp.Value);
    }

    /// <summary>
    /// Creates a Dictionary&lt;TKey,TValue> from an IEnumerable&lt;KeyValuePair&lt;TKey, TValue>>.
    /// </summary>
    /// <param name="enumerable">The IEnumerable&lt;KeyValuePair&lt;TKey, TValue>> to use to form the dictionary.</param>
    /// <param name="comparer">The comparer used to evaluate the keys.</param>
    /// <typeparam name="TKey">The type used for the dictionary key.</typeparam>
    /// <typeparam name="TValue">The type used for the dictionary value.</typeparam>
    /// <returns>A Dictionary&lt;TKey,TValue> that contains keys and values selected from the input sequence.</returns>
    [Pure]
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> enumerable, IEqualityComparer<TKey> comparer)
    {
        if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
        if (comparer == null) throw new ArgumentNullException(nameof(comparer));
        
        return enumerable.ToDictionary(
            static kvp => kvp.Key,
            static kvp => kvp.Value,
            comparer);
    }
}