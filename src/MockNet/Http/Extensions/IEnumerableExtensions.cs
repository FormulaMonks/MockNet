using System;
using System.Collections.Generic;

namespace MockNet.Http
{
    internal static class IEnumerableExtensions
    {
        internal static TSource SecondOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (source is IList<TSource> list)
            {
                if (list.Count > 1)
                {
                    return list[1];
                }
            }
            else
            {
                int i = 0;
                using (IEnumerator<TSource> e = source.GetEnumerator())
                {
                    if (e.MoveNext() && i++ == 2) return e.Current;
                }
            }

            return default;
        }

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