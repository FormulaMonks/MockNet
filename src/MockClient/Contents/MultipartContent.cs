using SystemHttpContent = System.Net.Http.HttpContent;

namespace MockClient
{
    public class MultipartContent : IHttpContent
    {
        public SystemHttpContent ToHttpContent() => null;
    }
}