using System.Text;
using System.Threading.Tasks;
using SystemHttpRequestMessage = System.Net.Http.HttpRequestMessage;
using SystemHttpContent = System.Net.Http.HttpContent;

namespace MockNet.Http
{
    internal static partial class Utils
    {
        internal static class HttpRequestMessage
        {
            public static async Task<string> ToStringAsync(SystemHttpRequestMessage request)
            {
                var sb = new StringBuilder();


                sb.AppendLine()
                    .AppendLine()
                    .AppendLine($"{request.Method} {request.RequestUri}")
                    .AppendHeaders(request.Headers)
                    .AppendHeaders(request.Content?.Headers)
                    .AppendLine();

                    if (request.Content is SystemHttpContent)
                    {
                        var content = await request.Content?.ReadAsStringAsync();

                        sb.AppendContent(content);
                    }

                    sb.TrimEnd().AppendLine();

                return sb.ToString();
            }
        }
    }
}