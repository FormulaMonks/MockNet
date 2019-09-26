using System;
using System.Linq.Expressions;

namespace MockNet.Http
{
    public partial class MockHttpClient
    {
        public ISetup SetupPost(string uri)
        {
            return Setup(HttpMethod.Post, uri);
        }

        public ISetup SetupPost(string uri, Expression<Func<HttpRequestHeaders, bool>> headers)
        {
            return Setup(HttpMethod.Post, uri, headers);
        }

        public ISetup SetupPost<TBody>(string uri, Expression<Func<HttpRequestHeaders, bool>> headers, Expression<Func<TBody, bool>> content)
        {
            return Setup(HttpMethod.Post, uri, headers, content);
        }
    }
}