using System;

namespace Stravaig.Extensions.Core
{
    public static class StringExtensions
    {
        public static bool HasContent(this string target)
        {
            return !string.IsNullOrWhiteSpace(target);
        }
    }
}