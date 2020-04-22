using System;
using System.Collections.Generic;
using System.Text;
using SystemHttpHeaders = System.Net.Http.Headers.HttpHeaders;

namespace Theorem.MockNet.Http
{
    internal static class StringBuilderExtensions
    {
        public static StringBuilder AppendHeaders(this StringBuilder builder, SystemHttpHeaders headers)
        {
            if (headers is SystemHttpHeaders)
            {
                builder.Append(headers.ToString());
            }

            return builder;
        }

        public static StringBuilder AppendContent(this StringBuilder builder, string content)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                builder.AppendLine(content);
            }

            return builder;
        }

        public static StringBuilder Join(this StringBuilder builder, Func<StringBuilder, StringBuilder> separator, IEnumerable<string> values)
        {
            if (values is null || builder is null)
            {
                return builder;
            }

            separator = separator ?? new Func<StringBuilder, StringBuilder>(x => x);

            using (var en = values.GetEnumerator())
            {
                if (!en.MoveNext())
                {
                    return builder;
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

        public static StringBuilder TrimEnd(this StringBuilder builder)
        {
            while (char.IsWhiteSpace(builder[builder.Length - 1]))
            {
                --builder.Length;
            }

            return builder;
        }
    }
}
