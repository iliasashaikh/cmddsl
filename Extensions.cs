using System.Collections.Generic;
using System.Linq;

namespace cmddsl
{
    public static class Extensions
    {
        public static bool IsEmpty(this string s) => string.IsNullOrEmpty(s);
        public static bool IsEmpty<T>(this IEnumerable<T> e) => e == null || e.Any();
    }
}