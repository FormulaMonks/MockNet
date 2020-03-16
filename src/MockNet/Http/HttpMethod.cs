using SystemHttpMethod = System.Net.Http.HttpMethod;

namespace MockNet.Http
{
    /// <summary>
    /// A local helper class that mimics the <see cref="SystemHttpMethod" />. This
    /// keeps the `System.Net.Http` namespace out of the unit tests.
    /// </summary>
    /// <remark>
    /// One of the goals of this project: is to allow developer to write clean unit tests
    /// and build tests that allow you to use `==` and `!=` on objects that the BCL do not
    /// allow. For example:
    /// .Setup("/", headers: headers => headers.Accept == "application/json")
    /// We could not test the Accept header because of its backend object type in the BCL.
    /// </remark>
    public sealed class HttpMethod
    {
        private readonly SystemHttpMethod httpMethod;
        private readonly string method;

        /// <summary>
        /// Represents the HTTP DELETE protocol method.
        /// </summary>
        public static HttpMethod Delete => SystemHttpMethod.Delete;

        /// <summary>
        /// Represents the HTTP GET protocol method.
        /// </summary>
        public static HttpMethod Get => SystemHttpMethod.Get;

        /// <summary>
        /// Represents the HTTP HEAD protocol method.
        /// </summary>
        public static HttpMethod Head => SystemHttpMethod.Head;

        /// <summary>
        /// Represents the HTTP OPTIONS protocol method.
        /// </summary>
        public static HttpMethod Options => SystemHttpMethod.Options;

        /// <summary>
        /// Represents the HTTP POST protocol method.
        /// </summary>
        public static HttpMethod Post => SystemHttpMethod.Post;

        /// <summary>
        /// Represents the HTTP PUT protocol method.
        /// </summary>
        public static HttpMethod Put => SystemHttpMethod.Put;

        /// <summary>
        /// Represent the HTTP TRACE protocol method.
        /// </summary>
        public static HttpMethod Trace => SystemHttpMethod.Trace;

        internal string Method => method;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMethod" /> class with a specific HTTP method.
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
