using System;
using System.Linq.Expressions;

namespace Theorem.MockNet.Http
{
    public partial class MockHttpClient
    {
        /// <summary>
        /// Specifices a setup on the HTTP PUT protocol.
        /// </summary>
        /// <param name="uri">The URI to match the setup with.</param>
        /// <param name="headers">Lambda predicate that specifics the match on headers.</param>
        public ISetup SetupPut(string uri, Expression<Func<HttpRequestHeaders, bool>> headers = null)
        {
            return Setup(HttpMethod.Put, uri, headers);
        }

        /// <summary>
        /// Specifices a setup on the HTTP PUT protocol.
        /// </summary>
        /// <param name="uri">The URI to match the setup with.</param>
        /// <param name="headers">Lambda predicate that specifics the match on headers.</param>
        /// <param name="content">Lambda predicate that specifies the match on content.</param>
        public ISetup SetupPut<TBody>(string uri, Expression<Func<HttpRequestHeaders, bool>> headers = null, Expression<Func<TBody, bool>> content = null)
        {
            return Setup(HttpMethod.Put, uri, headers, content);
        }
    }
}
