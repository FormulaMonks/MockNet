using SystemHttpContent = System.Net.Http.HttpContent;

namespace Theorem.MockNet.Http
{
    public interface IHttpContent
    {
        SystemHttpContent ToHttpContent();
    }
}