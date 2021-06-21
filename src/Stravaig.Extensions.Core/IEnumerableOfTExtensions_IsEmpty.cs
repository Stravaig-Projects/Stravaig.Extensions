using System.Collections.Generic;
using System.Linq;

namespace Stravaig.Extensions.Core
{
    public static class IEnumerableOfTExtensions_IsEmpty
    {
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || source.IsEmpty();
        }
    }
}