using SystemHttpMethod = System.Net.Http.HttpMethod;

namespace MockNet.Http
{
    public sealed class HttpMethod
    {
        private readonly SystemHttpMethod httpMethod;
        private readonly string method;

        public static HttpMethod Delete => SystemHttpMethod.Delete;
        public static HttpMethod Get => SystemHttpMethod.Get;
        public static HttpMethod Head => SystemHttpMethod.Head;
        public static HttpMethod Options => SystemHttpMethod.Options;
        public static HttpMethod Post => SystemHttpMethod.Post;
        public static HttpMethod Put => SystemHttpMethod.Put;
        public static HttpMethod Trace => SystemHttpMethod.Trace;

        public string Method => method;

        public HttpMethod(string method)
        {
            this.method = method;
            this.httpMethod = new SystemHttpMethod(method);
        }

        public int DefaultStatusCode()
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