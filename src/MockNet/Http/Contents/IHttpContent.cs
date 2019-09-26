using SystemHttpContent = System.Net.Http.HttpContent;

namespace MockNet.Http
{
    public interface IHttpContent
    {
        SystemHttpContent ToHttpContent();
    }
}