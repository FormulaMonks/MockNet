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
        IReturns ReturnsAsync(IHttpContent content);

        /// <summary>
        /// Specifies any <see cref="HttpResponseHeaders" /> to return.
        /// </summary>
        IReturns ReturnsAsync(HttpResponseHeaders headers);

        /// <summary>
        /// Specifies the HTTP status code and the <see cref="IHttpContent" /> to return.
        /// </summary>
        IReturns ReturnsAsync(int code, IHttpContent content);

        /// <summary>
        /// Specifies the HTTP status code and any <see cref="HttpResponseHeaders" /> to return.
        /// </summary>
        IReturns ReturnsAsync(int code, HttpResponseHeaders headers);

        /// <summary>
        /// Specifies the HTTP status code, any <see cref="HttpResponseHeaders" /> and the <see cref="IHttpContent" /> to return.
        /// </summary>
        IReturns ReturnsAsync(int code, HttpResponseHeaders headers, IHttpContent content);

        /// <summary>
        /// Specifies any <see cref="HttpResponseHeader" /> and the <see cref="IHttpContent" /> to return.
        /// </summary>
        IReturns ReturnsAsync(HttpResponseHeaders headers, IHttpContent content);
    }
}
