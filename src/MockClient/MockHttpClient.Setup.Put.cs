using System;
using System.Linq.Expressions;

namespace MockClient
{
    public partial class MockHttpClient
    {
        public ISetup SetupPut(string uri)
        {
            return Setup(HttpMethod.Put, uri);
        }

        public ISetup SetupPut(string uri, Expression<Func<HttpRequestHeaders, bool>> headers)
        {
            return Setup(HttpMethod.Put, uri, headers);
        }

        public ISetup SetupPut<T>(HttpMethod method, string uri, Expression<Func<HttpRequestHeaders, bool>> headers, Expression<Func<T, bool>> content)
        {
            return Setup(HttpMethod.Put, uri, headers, content);
        }
    }
}