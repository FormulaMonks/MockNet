using SystemHttpMethod = System.Net.Http.HttpMethod;

namespace MockNet.Http
{
    /// <summary>
    /// A local helper class that mimics the <see cref="SystemHttpMethod" />. This
    /// keeps the `System.Net.Http` namespace out of the unit tests.
    /// </summary>
    public sealed class HttpMethod
    {
        private readonly SystemHttpMethod httpMethod;
        private readonly string method;

        /// <summary>
        /// Represents the Http DELETE protocol method.
        /// </summary>
        public static HttpMethod Delete => SystemHttpMethod.Delete;

        /// <summary>
        /// Represents the Http GET protocol method.
        /// </summary>
        public static HttpMethod Get => SystemHttpMethod.Get;

        /// <summary>
        /// Represents the Http HEAD protocol method.
        /// </summary>
        public static HttpMethod Head => SystemHttpMethod.Head;

        /// <summary>
        /// Represents the Http OPTIONS protocol method.
        /// </summary>
        public static HttpMethod Options => SystemHttpMethod.Options;

        /// <summary>
        /// Represents the Http POST protocol method.
        /// </summary>
        public static HttpMethod Post => SystemHttpMethod.Post;

        /// <summary>
        /// Represents the Http PUT protocol method.
        /// </summary>
        public static HttpMethod Put => SystemHttpMethod.Put;

        /// <summary>
        /// Represent the Http TRACE protocol method.
        /// </summary>
        public static HttpMethod Trace => SystemHttpMethod.Trace;

        internal string Method => method;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMethod" /> class with a specific Http method.
        /// </summary>
        public HttpMethod(string method)
        {
            this.method = method;
            this.httpMethod = new SystemHttpMethod(method);
        }

        internal int DefaultStatusCode()
        {
            switch (method)
            {
                case nameof(Post): return 201;
                case nameof(Put): return 204;
                case nameof(Delete): return 202;
                default:
                    return 200;
            }
        }

        public static implicit operator string(HttpMethod method) => method.Method;
        public static implicit operator HttpMethod(string method) => new HttpMethod(method);
        public static implicit operator SystemHttpMethod(HttpMethod method) => new SystemHttpMethod(method.Method);
        public static implicit operator HttpMethod(SystemHttpMethod method) => new HttpMethod(method.Method);
    }
}
