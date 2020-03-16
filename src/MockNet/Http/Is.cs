using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MockNet.Http
{
    public static class Is
    {
        /// <summary>
        /// Matches any value of the given <typeparamref name="T" /> type.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        public static T Any<T>() => default;

        /// <summary>
        /// Matches any value of the given <typeparamref name="T" /> type, except null.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        public static T NotNull<T>() => default;

        /// <summary>
        /// Matches the given value equality.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="item">The item to match the given value's equality to.</param>
        public static T Equal<T>(T item) => default;

        /// <summary>
        /// Matches the given value has the same reference.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="item">The item to match the given value's reference to.</param>
        public static T SameAs<T>(T item) => default;

        /// <summary>
        /// Matches the given value to an empty string.
        /// </summary>
        public static string Empty() => default;

        /// <summary>
        /// Matches the given list is empty.
        /// </summary>
        public static IEnumerable<T> Empty<T>() => default;

        /// <summary>
        /// Matches the given list contains the value.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="item">The item to check if the list contains.</param>
        public static IEnumerable<T> In<T>(T item) => default;

        /// <summary>
        /// Matches the given list contains all of the values.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="item">The list of items to check if the list contains.</param>
        public static IEnumerable<T> In<T>(params T[] item) => default;

        /// <summary>
        /// Matches the given list does not contain the value.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="item">The item that is not contained in the list.</param>
        public static IEnumerable<T> NotIn<T>(T item) => default;

        /// <summary>
        /// Matches the given list does not contain all of the values.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="item">The list of items that are not contained in the list.</param>
        public static IEnumerable<T> NotIn<T>(params T[] item) => default;

        /// <summary>
        /// Matches the given list contains (in order) all of the values.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="item">The exact sequence of items in the list.</param>
        public static IEnumerable<T> Sequence<T>(params T[] item) => default;

        /// <summary>
        /// Matches a string if it matches the given regular expression pattern.
        /// </summary>
        /// <param name="regex">The pattern to use when matching the string value.</param>
        public static string Match(string regex) => default;

        /// <summary>
        /// Matches a string if it matches the given regular expression pattern
        /// </summary>
        /// <param name="regex">The pattern to use when matching the string value.</param>
        /// <param name="options">The options used to interpret the pattern.</param>
        public static string Match(string regex, RegexOptions options) => default;

        /// <summary>
        /// Matches any value that is in the <see cref="RangeType.Inclusive" /> range specified.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="from">The lower bound of the range.</param>
        /// <param name="to">The upper bound of the range.</param>
        public static T InRange<T>(T from, T to) where T : IComparable => default;

        /// <summary>
        /// Matches any value that is in the range specified.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="from">The lower bound of the range.</param>
        /// <param name="to">The upper bound of the range.</param>
        /// <param name="range">The type of range to check.</param>
        public static T InRange<T>(T from, T to, RangeType range) where T : IComparable => default;
    }
}
