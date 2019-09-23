using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SystemHttpRequestMessage = System.Net.Http.HttpRequestMessage;

namespace MockNet.Http
{
    internal class RequestMessage
    {
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

        private readonly LambdaExpression expression;
        private readonly MockHttpClient mock;

        public HttpMethod HttpMethod { get; set; }
        public string RequestUri { get; set; }
        public Delegate HeadersValidator;
        public Delegate ContentValidator;
        public Type ContentType { get; set; }

        public Func<SystemHttpRequestMessage, Task<object>> Deserializer;

        public RequestMessage(MockHttpClient mock, HttpMethod method, string uri, LambdaExpression headers, LambdaExpression content, Type contentType)
        {
            this.mock = mock;
            HttpMethod = method;
            RequestUri = uri;
            HeadersValidator = headers?.Compile();
            ContentValidator = content?.Compile();
            ContentType = contentType;

            if (deserializers.ContainsKey(contentType))
            {
                Deserializer = deserializers[contentType];
            }
            else
            {
                Deserializer = new Func<SystemHttpRequestMessage, Task<object>>(x => x.Content.ToObjectAsync(contentType));
            }
        }
    }
}