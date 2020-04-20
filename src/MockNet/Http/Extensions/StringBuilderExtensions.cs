using System;
using System.Collections.Generic;
using System.Text;
using SystemHttpHeaders = System.Net.Http.Headers.HttpHeaders;

namespace Theorem.MockNet.Http
{
    internal static class StringBuilderExtensions
    {
        public static StringBuilder AppendHeaders(this StringBuilder sb, SystemHttpHeaders headers)
        {
            if (headers is SystemHttpHeaders)
            {
                sb.Append(headers.ToString());
            }

            return sb;
        }

        public static StringBuilder AppendContent(this StringBuilder sb, string content)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                sb.AppendLine(content);
            }

            return sb;
        }

        public static StringBuilder Join(this StringBuilder sb, Func<StringBuilder, StringBuilder> separator, IEnumerable<string> values)
        {
            if (values is null || sb is null)
            {
                return sb;
            }

            separator = separator ?? new Func<StringBuilder, StringBuilder>(x => x);

            using (var en = values.GetEnumerator())
            {
                if (!en.MoveNext())
                {
                    return sb;
                }

                var result = new StringBuilder();
                if (en.Current is object)
                {
                    result.Append(en.Current);
                }

                while (en.MoveNext())
                {
                    separator(result);

                    if (en.Current is object)
                    {
                        result.Append(en.Current);
                    }
                }

                return result;
            }
        }

        public static StringBuilder TrimEnd(this StringBuilder sb)
        {
            while (char.IsWhiteSpace(sb[sb.Length - 1]))
            {
                --sb.Length;
            }

            return sb;
        }
    }
}
