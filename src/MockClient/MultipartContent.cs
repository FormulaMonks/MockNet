using SystemHttpContent = System.Net.Http.HttpContent;

namespace MockClient
{
    public class MultipartContent : IContent
    {
        public SystemHttpContent ToHttpContent() => null;
    }
}