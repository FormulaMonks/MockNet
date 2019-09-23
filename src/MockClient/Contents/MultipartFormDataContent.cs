using SystemHttpContent = System.Net.Http.HttpContent;

namespace MockClient
{
    public class MultipartFormDataContent : IHttpContent
    {
        public SystemHttpContent ToHttpContent() => null;
    }
}