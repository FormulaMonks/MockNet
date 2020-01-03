using System.Text;
using SystemHttpHeaders = System.Net.Http.Headers.HttpHeaders;

namespace MockNet.Http
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
