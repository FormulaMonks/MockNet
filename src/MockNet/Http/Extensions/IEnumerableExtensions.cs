using System;
using System.Collections.Generic;

namespace Theorem.MockNet.Http
{
    internal static class IEnumerableExtensions
    {
        internal static TSource SecondOrDefault<TSource>(this IEnumerable<TSource> source) => source.NthOrDefault(2);

        internal static TSource NthOrDefault<TSource>(this IEnumerable<TSource> source, int nth)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (source is IList<TSource> list)
            {
                if (list.Count > nth - 1)
                {
                    return list[nth - 1];
                }
            }
            else
            {
                int i = 0;
                using (IEnumerator<TSource> e = source.GetEnumerator())
                {
                    if (e.MoveNext() && i++ == nth) return e.Current;
                }
            }

            return default;
        }
    }
}
