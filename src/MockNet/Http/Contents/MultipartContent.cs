using System;
using SystemHttpContent = System.Net.Http.HttpContent;

namespace Theorem.MockNet.Http
{
    public class MultipartContent : IHttpContent
    {
        public SystemHttpContent ToHttpContent() => throw new NotImplementedException();
    }
}
