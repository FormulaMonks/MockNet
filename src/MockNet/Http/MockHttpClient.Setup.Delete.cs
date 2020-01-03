using System;
using System.Linq.Expressions;

namespace MockNet.Http
{
    public partial class MockHttpClient
    {
        public ISetup SetupDelete(string uri, Expression<Func<HttpRequestHeaders, bool>> headers = null)
        {
            return Setup(HttpMethod.Delete, uri, headers);
        }
    }
}
