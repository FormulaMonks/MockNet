using System;
using System.Linq.Expressions;

namespace Theorem.MockNet.Http
{
    public partial class MockHttpClient
    {
        /// <summary>
        /// Specifies a setup on the HTTP POST protocol.
        /// </summary>
        /// <param name="uri">The URI to match the setup with.</param>
        /// <param name="headers">Lambda predicate that specifics the match on headers.</param>
        public ISetup SetupPost(string uri, Expression<Func<HttpRequestHeaders, bool>> headers = null)
        {
            return Setup(HttpMethod.Post, uri, headers);
        }

        /// <summary>
        /// Specifies a setup on the HTTP POST protocol.
        /// </summary>
        /// <param name="uri">The URI to match the setup with.</param>
        /// <param name="headers">Lambda predicate that specifics the match on headers.</param>
        /// <param name="content">Lambda predicate that specifies the match on content.</param>
        public ISetup SetupPost<TBody>(string uri, Expression<Func<HttpRequestHeaders, bool>> headers = null, Expression<Func<TBody, bool>> content = null)
        {
            return Setup(HttpMethod.Post, uri, headers, content);
        }
    }
}
