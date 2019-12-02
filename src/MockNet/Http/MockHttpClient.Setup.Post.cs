using System;
using System.Linq.Expressions;

namespace MockNet.Http
{
    public partial class MockHttpClient
    {
        /// <summary>
        /// Specifices a setup on the HTTP POST protocol.
        /// </summary>
        /// <param name="uri">The uri to match the setup with.</param>
        public ISetup SetupPost(string uri)
        {
            return Setup(HttpMethod.Post, uri);
        }

        /// <summary>
        /// Specifies a setup on the HTTP POST protocol.
        /// </summary>
        /// <param name="uri">The uri to match the setup with.</param>
        /// <param name="headers">Lambda predicate that specifics the match on headers.</param>
        public ISetup SetupPost(string uri, Expression<Func<HttpRequestHeaders, bool>> headers)
        {
            return Setup(HttpMethod.Post, uri, headers);
        }

        /// <summary>
        /// Specifies a setup on the HTTP POST protocol.
        /// </summary>
        /// <param name="uri">The uri to match the setup with.</param>
        /// <param name="headers">Lambda predicate that specifics the match on headers.</param>
        /// <param name="content">Lambda predicate that specifies the match on content.</param>
        public ISetup SetupPost<TBody>(string uri, Expression<Func<HttpRequestHeaders, bool>> headers, Expression<Func<TBody, bool>> content)
        {
            return Setup(HttpMethod.Post, uri, headers, content);
        }
    }
}
