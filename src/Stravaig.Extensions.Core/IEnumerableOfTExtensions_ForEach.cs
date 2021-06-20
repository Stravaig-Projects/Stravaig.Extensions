using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stravaig.Extensions.Core
{
    // Named after the interface + Method
    // ReSharper disable once InconsistentNaming
    public static class IEnumerableOfTExtensions_ForEach
    {
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

        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            foreach (T item in sequence)
            {
                action(item);
            }
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> sequence, Func<T, Task> asyncAction)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (asyncAction == null) throw new ArgumentNullException(nameof(asyncAction));
            
            foreach (T item in sequence)
            {
                await asyncAction(item);
            }
        }

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
}