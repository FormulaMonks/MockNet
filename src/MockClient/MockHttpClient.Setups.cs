using System;
using System.Linq.Expressions;

namespace MockClient
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

        public ISetup Setup<T>(HttpMethod method, string uri, Expression<Func<HttpRequestHeaders, bool>> headers, Expression<Func<T, bool>> content)
        {
            var request = new RequestMessage(this, method, uri, headers, content, typeof(T))
            {
                HttpMethod = method,
                RequestUri = uri,
                ContentType = typeof(T),
            };

            return MockHttpClient.Setup(this, request);
        }
    }
}