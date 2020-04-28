using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SystemHttpRequestMessage = System.Net.Http.HttpRequestMessage;

namespace Theorem.MockNet.Http
{
    internal class RequestMessage
    {
        private static ExpressionVisitor visitor = new IsExpressionVisitor();

        private static Dictionary<Type, Func<SystemHttpRequestMessage, Task<object>>> deserializers => new Dictionary<Type, Func<SystemHttpRequestMessage, Task<object>>>()
        {
            [typeof(string)] = x => x.Content.ToStringAsync(),
            [typeof(StringContent)] = x => x.Content.ToStringContentAsync(),

            [typeof(Stream)] = x => x.Content.ToStreamAsync(),
            [typeof(StreamContent)] = x => x.Content.ToStreamContentAsync(),

            [typeof(byte[])] = x => x.Content.ToByteArrayAsync(),
            [typeof(ByteArrayContent)] = x => x.Content.ToByteArrayContentAsync(),

            [typeof(Dictionary<string, string>)] = x => x.Content.ToDictionaryAsync(),
            [typeof(FormUrlEncodedContent)] = x => x.Content.ToFormUrlEncodedContentAsync(),
        };

        private readonly MockHttpClient mock;

        public HttpMethod HttpMethod { get;}
        public string RequestUri { get; }
        public Expression Headers { get; }
        public Delegate HeadersValidator { get; }
        public Expression Content { get; }
        public Delegate ContentValidator { get; }
        public Type ContentType { get; }

        public Func<SystemHttpRequestMessage, Task<object>> Deserializer;

        public RequestMessage(MockHttpClient mock, HttpMethod method, string uri, LambdaExpression headers, LambdaExpression content, Type contentType)
        {
            this.mock = mock;
            HttpMethod = method;
            RequestUri = uri;
            Headers = headers;
            Content = content;

            HeadersValidator = visit(visitor, headers)?.Compile();
            ContentValidator = visit(visitor, content)?.Compile();
            ContentType = contentType;

            if (deserializers.ContainsKey(contentType))
            {
                Deserializer = deserializers[contentType];
            }
            else
            {
                Deserializer = new Func<SystemHttpRequestMessage, Task<object>>(x => x.Content.ToObjectAsync(contentType));
            }

            LambdaExpression visit(ExpressionVisitor v, LambdaExpression e) => v.Visit(e) as LambdaExpression;
        }

        public override string ToString()
        {
            var parameterList = new List<string>
            {
                $"\"{RequestUri}\"",
            };

            if (Headers is Expression)
            {
                parameterList.Add($"headers: {Headers.ToString()}");
            }

            if (Content is Expression)
            {
                parameterList.Add($"content: {Content.ToString()}");
            }

            var parameters = string.Join(", ", parameterList.Where(x => x is string));
            var method = Utils.String.Capitalize(HttpMethod.Method);

            if (ContentType == typeof(object))
            {
                return $"Setup{method}({parameters})";
            }

            return $"Setup{method}<{ContentType}>({parameters})";
        }
    }
}