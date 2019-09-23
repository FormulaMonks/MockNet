namespace MockClient
{
    public interface IReturns
    {
        IReturns ReturnsAsync(int code);
        IReturns ReturnsAsync(IHttpContent content);
        IReturns ReturnsAsync(HttpResponseHeaders headers);
        IReturns ReturnsAsync(int code, IHttpContent content);
        IReturns ReturnsAsync(int code, HttpResponseHeaders headers);
        IReturns ReturnsAsync(int code, HttpResponseHeaders headers, IHttpContent content);
        IReturns ReturnsAsync(HttpResponseHeaders headers, IHttpContent content);
    }
}