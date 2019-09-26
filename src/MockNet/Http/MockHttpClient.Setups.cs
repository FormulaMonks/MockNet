using System;
using System.Linq.Expressions;

namespace MockNet.Http
{
    public partial class MockHttpClient
    {
        public ISetup Setup(HttpMethod method, string uri)
        {
            var request = new RequestMessage(this, method, uri, null, null, typeof(object));

            return MockHttpClient.Setup(this, request);
        }

        public ISetup Setup(HttpMethod method, string uri, Expression<Func<HttpRequestHeaders, bool>> headers)
        {
            var request = new RequestMessage(this, method, uri, headers, null, typeof(object))
            {
                HttpMethod = method,
                RequestUri = uri,
            };

            return MockHttpClient.Setup(this, request);
        }

        public ISetup Setup<TBody>(HttpMethod method, string uri, Expression<Func<HttpRequestHeaders, bool>> headers, Expression<Func<TBody, bool>> content)
        {
            var request = new RequestMessage(this, method, uri, headers, content, typeof(TBody))
            {
                HttpMethod = method,
                RequestUri = uri,
                ContentType = typeof(TBody),
            };

            return MockHttpClient.Setup(this, request);
        }
    }
}