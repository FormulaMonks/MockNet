using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MockNet.Http
{
    internal static class IsInternal
    {
        public static bool Any<T>(T value)
        {
            return value == null || typeof(T).IsAssignableFrom(value.GetType());
        }

        public static bool NotNull<T>(T value)
        {
            return value != null && typeof(T).IsAssignableFrom(value.GetType());
        }

        public static bool Equal<T>(T value, T item)
        {
            return value.Equals(item);
        }

        public static bool SameAs<T>(T value, T item)
        {
            return ReferenceEquals(value, item);
        }

        public static bool Empty(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool Empty<T>(IEnumerable<T> value)
        {
            return !(value?.Any() ?? false);
        }

        public static bool In<T>(IEnumerable<T> value, T item)
        {
            return value.Contains(item);
        }

        public static bool In<T>(IEnumerable<T> value, IEnumerable<T> item)
        {
            return !value.Except(item).Any();
        }

        // public static bool NotIn<T>(IEnumerable<T> value, T item)
        // {
        //     return !value.Contains(item);
        // }

        // public static bool NotIn<T>(IEnumerable<T> value, IEnumerable<T> item)
        // {
        //     return !item.Except(value).Any();
        // }

        // public static bool Sequence<T>(IEnumerable<T> value, IEnumerable<T> item)
        // {
        //     return value.SequenceEqual(item);
        // }

        // public static bool Match(string value, string regex)
        // {
        //     if (string.IsNullOrWhiteSpace(regex))
        //     {
        //         throw new ArgumentNullException(nameof(regex));
        //     }

        //     var re = new Regex(regex);

        //     return value != null && re.IsMatch(value);
        // }

        // public static bool Match(string value, string regex, RegexOptions options)
        // {
        //     if (string.IsNullOrWhiteSpace(regex))
        //     {
        //         throw new ArgumentNullException(nameof(regex));
        //     }

        //     var re = new Regex(regex, options);

        //     return value != null && re.IsMatch(value);
        // }
    }
}