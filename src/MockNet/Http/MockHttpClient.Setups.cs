using System;
using System.Linq.Expressions;

namespace Theorem.MockNet.Http
{
    public partial class MockHttpClient
    {
        /// <summary>
        /// Specifices a setup for the given <see cref="HttpMethod" /> protocol.
        /// </summary>
        /// <param name="method">The protocol for the setup.</param>
        /// <param name="uri">The URI to match the setup with.</param>
        /// <param name="headers">Lambda predicate that specifics the match on headers.</param>
        public ISetup Setup(HttpMethod method, string uri, Expression<Func<HttpRequestHeaders, bool>> headers = null)
        {
            var request = new RequestMessage(this, method, uri, headers, null, typeof(object));

            return MockHttpClient.Setup(this, request);
        }

        /// <summary>
        /// Specifices a setup for the given <see cref="HttpMethod" /> protocol.
        /// </summary>
        /// <param name="method">The protocol for the setup.</param>
        /// <param name="uri">The URI to match the setup with.</param>
        /// <param name="headers">Lambda predicate that specifics the match on headers.</param>
        /// <param name="content">Lambda predicate that specifies the match on content.</param>
        public ISetup Setup<TBody>(HttpMethod method, string uri, Expression<Func<HttpRequestHeaders, bool>> headers = null, Expression<Func<TBody, bool>> content = null)
        {
            var request = new RequestMessage(this, method, uri, headers, content, typeof(TBody));

            return MockHttpClient.Setup(this, request);
        }
    }
}
