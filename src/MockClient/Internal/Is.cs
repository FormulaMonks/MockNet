using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MockClient
{
    internal static class Is
    {
        public static bool Any<T>(T value)
        {
            return value == null || typeof(T).IsAssignableFrom(value.GetType());
        }

        public static bool NotNull<T>(T value)
        {
            return value != null && typeof(T).IsAssignableFrom(value.GetType());
        }

        public static bool Match<T>(T value, Func<T, bool> matcher)
        {
            return matcher(value);
        }

        public static bool Equal<T>(T value, T item)
        {
            return value.Equals(item);
        }

        public static bool SequenceEqual<T>(IEnumerable<T> value, IEnumerable<T> items)
        {
            return !value.Except(items).Any() && !items.Except(value).Any();
        }

        public static bool SameAs<T>(T value, T item)
        {
            return ReferenceEquals(value, item);
        }

        public static bool Empty(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsEmpty<T>(IEnumerable<T> value)
        {
            return !value.Any();
        }

        public static bool InRange<T>(T value, T from, T to, RangeType rangeType) where T : IComparable
        {
            if (value == null)
            {
                return false;
            }

            if (rangeType == RangeType.Exclusive)
            {
                return value.CompareTo(from) > 0 && value.CompareTo(to) < 0;
            }


            return value.CompareTo(from) >= 0 && value.CompareTo(to) <= 0;
        }

        public static bool In<T>(T value, IEnumerable<T> items)
        {
            return items.Contains(value);
        }

        public static bool In<T>(T value, params T[] items)
        {
            return items.Contains(value);
        }

        public static bool In<T>(IEnumerable<T> value, IEnumerable<T> items)
        {
            return !value.Except(items).Any();
        }

        public static bool In<T>(IEnumerable<T> value, params T[] items)
        {
            return !value.Except(items).Any();
        }

        public static bool NotIn<T>(T value, IEnumerable<T> items)
        {
            return !items.Contains(value);
        }

        public static bool NotIn<T>(T value, params T[] items)
        {
            return !items.Contains(value);
        }

        public static bool Contains<T>(IEnumerable<T> value, T item)
        {
            return value.Contains(item);
        }

        public static bool Regex(string value, string regex)
        {
            if (string.IsNullOrWhiteSpace(regex))
            {
                throw new ArgumentNullException(nameof(regex));
            }

            var re = new Regex(regex);

            return value != null && re.IsMatch(value);
        }

        public static bool Regex(string value, string regex, RegexOptions options)
        {
            if (string.IsNullOrWhiteSpace(regex))
            {
                throw new ArgumentNullException(nameof(regex));
            }

            var re = new Regex(regex, options);

            return value != null && re.IsMatch(value);
        }
    }
}