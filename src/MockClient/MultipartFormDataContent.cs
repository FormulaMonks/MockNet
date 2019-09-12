using SystemHttpContent = System.Net.Http.HttpContent;

namespace MockClient
{
    public class MultipartFormDataContent : IContent
    {
        public SystemHttpContent ToHttpContent() => null;
    }
}