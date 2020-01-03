using System;
using System.Linq.Expressions;

namespace MockNet.Http
{
    public partial class MockHttpClient
    {
        public ISetup SetupPost(string uri, Expression<Func<HttpRequestHeaders, bool>> headers = null)
        {
            return Setup(HttpMethod.Post, uri, headers);
        }

        public ISetup SetupPost<TBody>(string uri, Expression<Func<HttpRequestHeaders, bool>> headers = null, Expression<Func<TBody, bool>> content = null)
        {
            return Setup(HttpMethod.Post, uri, headers, content);
        }
    }
}
