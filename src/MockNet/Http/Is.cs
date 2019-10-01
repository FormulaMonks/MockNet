using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MockNet.Http
{
    public static class Is
    {
        public static T Any<T>() => default;

        public static T NotNull<T>() => default;

        public static T Equal<T>(T item) => default;

        public static T SameAs<T>(T item) => default;

        public static string Empty() => default;

        public static IEnumerable<T> Empty<T>() => default;

        public static IEnumerable<T> In<T>(T item) => default;

        public static IEnumerable<T> In<T>(params T[] item) => default;

        // public static IEnumerable<T> NotIn<T>(T item) => default;

        // public static IEnumerable<T> NotIn<T>(params T[] item) => default;

        //public static IEnumerable<T> Sequence<T>(params T[] item) => default;

        // public static string Match(string regex) => default;

        // public static string Match(string regex, RegexOptions options) => default;
    }
}