namespace Theorem.MockNet.Http
{
    public interface IReturns
    {
        /// <summary>
        /// Specifices the HTTP status code to return.
        /// </summary>
        IReturns ReturnsAsync(int code);

        /// <summary>
        /// Specifies the <see cref="IHttpContent" /> to return.
        /// </summary>
        IReturns ReturnsAsync<THttpContent>(THttpContent content) where THttpContent : IHttpContent;

        /// <summary>
        /// Specifies any <see cref="HttpResponseHeaders" /> to return.
        /// </summary>
        IReturns ReturnsAsync(HttpResponseHeaders headers);

        /// <summary>
        /// Specifies the HTTP status code and the <see cref="IHttpContent" /> to return.
        /// </summary>
        IReturns ReturnsAsync<THttpContent>(int code, THttpContent content) where THttpContent : IHttpContent;

        /// <summary>
        /// Specifies the HTTP status code and any <see cref="HttpResponseHeaders" /> to return.
        /// </summary>
        IReturns ReturnsAsync(int code, HttpResponseHeaders headers);

        /// <summary>
        /// Specifies any <see cref="HttpResponseHeader" /> and the <see cref="IHttpContent" /> to return.
        /// </summary>
        IReturns ReturnsAsync<THttpContent>(HttpResponseHeaders headers, THttpContent content) where THttpContent : IHttpContent;

        /// <summary>
        /// Specifies the HTTP status code, any <see cref="HttpResponseHeaders" /> and the <see cref="IHttpContent" /> to return.
        /// </summary>
        IReturns ReturnsAsync<THttpContent>(int code, HttpResponseHeaders headers, THttpContent content) where THttpContent : IHttpContent;

        /// <summary>
        /// Specifies the content to return.
        /// </summary>
        IReturns ReturnsAsync(object content);

        /// <summary>
        /// Specifies the HTTP status code and the content to return.
        /// </summary>
        IReturns ReturnsAsync(int code, object content);

        /// <summary>
        /// Specifies the HTTP status code, any <see cref="HttpResponseHeaders" /> and the content to return.
        /// </summary>
        IReturns ReturnsAsync(int code, HttpResponseHeaders headers, object content);

        /// <summary>
        /// Specifies any <see cref="HttpResponseHeader" /> and the content to return.
        /// </summary>
        IReturns ReturnsAsync(HttpResponseHeaders headers, object content);
    }
}
