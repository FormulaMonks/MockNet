using System;
using System.Linq.Expressions;

namespace MockNet.Http
{
    public partial class MockHttpClient
    {
        /// <summary>
        /// Specifices a setup on the HTTP DELETE protocol.
        /// </summary>
        /// <param name="uri">The uri to match the setup with.</param>
        public ISetup SetupDelete(string uri)
        {
            return Setup(HttpMethod.Delete, uri);
        }

        /// <summary>
        /// Specifies a setup on the HTTP DELETE protocol.
        /// </summary>
        /// <param name="uri">The uri to match the setup with.</param>
        /// <param name="headers">Lambda predicate that specifics the match on headers.</param>
        public ISetup SetupDelete(string uri, Expression<Func<HttpRequestHeaders, bool>> headers)
        {
            return Setup(HttpMethod.Delete, uri, headers);
        }
    }
}
