using System;
using System.Linq.Expressions;

namespace MockClient
{
    public partial class MockHttpClient
    {
        public ISetup SetupDelete(string uri)
        {
            return Setup(HttpMethod.Delete, uri);
        }

        public ISetup SetupDelete(string uri, Expression<Func<HttpRequestHeaders, bool>> headers)
        {
            return Setup(HttpMethod.Delete, uri, headers);
        }
    }
}