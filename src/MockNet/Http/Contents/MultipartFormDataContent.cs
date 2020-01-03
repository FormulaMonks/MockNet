using System;
using SystemHttpContent = System.Net.Http.HttpContent;

namespace MockNet.Http
{
    public class MultipartFormDataContent : IHttpContent
    {
        public SystemHttpContent ToHttpContent() => throw new NotImplementedException();
    }
}
