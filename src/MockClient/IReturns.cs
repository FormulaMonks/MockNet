namespace MockClient
{
    public interface IReturns
    {
        IReturns ReturnsAsync(int code);
        IReturns ReturnsAsync(IContent content);
        IReturns ReturnsAsync(HttpResponseHeaders headers);
        IReturns ReturnsAsync(int code, IContent content);
        IReturns ReturnsAsync(int code, HttpResponseHeaders headers);
        IReturns ReturnsAsync(int code, HttpResponseHeaders headers, IContent content);
        IReturns ReturnsAsync(HttpResponseHeaders headers, IContent content);
    }
}