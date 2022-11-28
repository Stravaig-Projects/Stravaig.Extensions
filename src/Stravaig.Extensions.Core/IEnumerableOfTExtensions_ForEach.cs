using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stravaig.Extensions.Core;

// Named after the interface + Method
// ReSharper disable once InconsistentNaming
/// <summary>
/// Extension methods that add ForEach functionality to any IEnumerable&lt;T>
/// </summary>
public static class IEnumerableOfTExtensions_ForEach
{
    /// <summary>
    /// Performs an action on each element of the sequence.
    /// </summary>
    /// <param name="sequence">The items to perform the action upon.</param>
    /// <param name="action">The action to perform on a specific element in the sequence and the index of the element in the sequence.</param>
    /// <typeparam name="T">The type of object upon which the action is taken.</typeparam>
    /// <exception cref="ArgumentNullException">If sequence or action is null then an exception is thrown.</exception>
    public static void ForEach<T>(this IEnumerable<T> sequence, Action<T, int> action)
    {
        if (sequence == null) throw new ArgumentNullException(nameof(sequence));
        if (action == null) throw new ArgumentNullException(nameof(action));
            
        var virtualIndex = 0;
        foreach (T item in sequence)
        {
            action(item, virtualIndex);
            virtualIndex++;
        }
    }

    /// <summary>
    /// Performs an action on each element of the sequence.
    /// </summary>
    /// <param name="sequence">The items to perform the action upon.</param>
    /// <param name="action">The action to perform on a specific element in the sequence.</param>
    /// <typeparam name="T">The type of object upon which the action is taken.</typeparam>
    /// <exception cref="ArgumentNullException">If sequence or action is null then an exception is thrown.</exception>
    public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
    {
        if (sequence == null) throw new ArgumentNullException(nameof(sequence));
        if (action == null) throw new ArgumentNullException(nameof(action));
            
        foreach (T item in sequence)
        {
            action(item);
        }
    }

    /// <summary>
    /// Performs an async action on each element of the sequence.
    /// </summary>
    /// <param name="sequence">The items to perform the action upon.</param>
    /// <param name="asyncAction">The action (function returning a Task) to perform on a specific element in the sequence.</param>
    /// <typeparam name="T">The type of object upon which the action is taken.</typeparam>
    /// <exception cref="ArgumentNullException">If sequence or asyncAction is null then an exception is thrown.</exception>
    public static async Task ForEachAsync<T>(this IEnumerable<T> sequence, Func<T, Task> asyncAction)
    {
        if (sequence == null) throw new ArgumentNullException(nameof(sequence));
        if (asyncAction == null) throw new ArgumentNullException(nameof(asyncAction));
            
        foreach (T item in sequence)
        {
            await asyncAction(item);
        }
    }

    /// <summary>
    /// Performs an async action on each element of the sequence.
    /// </summary>
    /// <param name="sequence">The items to perform the action upon.</param>
    /// <param name="asyncAction">The async action (function returning a Task) to perform on a specific element in the sequence and the index of the element in the sequence.</param>
    /// <typeparam name="T">The type of object upon which the action is taken.</typeparam>
    /// <exception cref="ArgumentNullException">If sequence or action is null then an exception is thrown.</exception>
    public static async Task ForEachAsync<T>(this IEnumerable<T> sequence, Func<T, int, Task> asyncAction)
    {
        if (sequence == null) throw new ArgumentNullException(nameof(sequence));
        if (asyncAction == null) throw new ArgumentNullException(nameof(asyncAction));
            
        int virtualIndex = 0;
        foreach (T item in sequence)
        {
            await asyncAction(item, virtualIndex);
            virtualIndex++;
        }
    }
}