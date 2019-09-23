using SystemHttpContent = System.Net.Http.HttpContent;

namespace MockClient
{
    public interface IHttpContent
    {
        SystemHttpContent ToHttpContent();
    }
}