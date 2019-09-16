using SystemHttpContent = System.Net.Http.HttpContent;

namespace MockClient
{
    public interface IContent
    {
        SystemHttpContent ToHttpContent();
    }
}