using System;
using SystemHttpContent = System.Net.Http.HttpContent;

namespace MockNet.Http
{
    public class MultipartContent : IHttpContent
    {
        public SystemHttpContent ToHttpContent() => throw new NotImplementedException();
    }
}
