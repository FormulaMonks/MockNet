using System;
using SystemHttpContent = System.Net.Http.HttpContent;

namespace Theorem.MockNet.Http
{
    public class MultipartFormDataContent : HttpContent
    {
        protected override SystemHttpContent ToSystemHttpContent() => throw new NotImplementedException();
    }
}
