using System;
using SystemHttpContent = System.Net.Http.HttpContent;

namespace Theorem.MockNet.Http
{
    public class MultipartContent : HttpContent
    {
        protected override SystemHttpContent ToSystemHttpContent() => throw new NotImplementedException();
    }
}
