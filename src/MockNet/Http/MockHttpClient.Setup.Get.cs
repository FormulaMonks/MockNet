using System;
using System.Linq.Expressions;

namespace Theorem.MockNet.Http
{
    public partial class MockHttpClient
    {
        /// <summary>
        /// Specifies a setup on the HTTP GET protocol.
        /// </summary>
        /// <param name="uri">The URI to match the setup with.</param>
        /// <param name="headers">Lambda predicate that specifics the match on headers.</param>
        public ISetup SetupGet(string uri, Expression<Func<HttpRequestHeaders, bool>> headers = null)
        {
            return Setup(HttpMethod.Get, uri, headers);
        }
    }
}
