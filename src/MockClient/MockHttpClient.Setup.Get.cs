using System;
using System.Linq.Expressions;

namespace MockClient
{
    public partial class MockHttpClient
    {
        public ISetup SetupGet(string uri)
        {
            return Setup(HttpMethod.Get, uri);
        }

        public ISetup SetupGet(string uri, Expression<Func<HttpRequestHeaders, bool>> headers)
        {
            return Setup(HttpMethod.Get, uri, headers);
        }
    }
}