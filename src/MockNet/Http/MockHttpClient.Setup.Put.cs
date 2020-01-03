using System;
using System.Linq.Expressions;

namespace MockNet.Http
{
    public partial class MockHttpClient
    {
        public ISetup SetupPut(string uri, Expression<Func<HttpRequestHeaders, bool>> headers = null)
        {
            return Setup(HttpMethod.Put, uri, headers);
        }

        public ISetup SetupPut<TBody>(string uri, Expression<Func<HttpRequestHeaders, bool>> headers = null, Expression<Func<TBody, bool>> content = null)
        {
            return Setup(HttpMethod.Put, uri, headers, content);
        }
    }
}
