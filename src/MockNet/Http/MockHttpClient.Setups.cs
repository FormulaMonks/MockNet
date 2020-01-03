using System;
using System.Linq.Expressions;

namespace MockNet.Http
{
    public partial class MockHttpClient
    {
        public ISetup Setup(HttpMethod method, string uri, Expression<Func<HttpRequestHeaders, bool>> headers = null)
        {
            var request = new RequestMessage(this, method, uri, headers, null, typeof(object));

            return MockHttpClient.Setup(this, request);
        }

        public ISetup Setup<TBody>(HttpMethod method, string uri, Expression<Func<HttpRequestHeaders, bool>> headers = null, Expression<Func<TBody, bool>> content = null)
        {
            var request = new RequestMessage(this, method, uri, headers, content, typeof(TBody));

            return MockHttpClient.Setup(this, request);
        }
    }
}
