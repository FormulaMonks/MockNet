using SystemHttpResponseMessage = System.Net.Http.HttpResponseMessage;

namespace Theorem.MockNet.Http
{
    internal sealed class Result
    {
        public bool Matched { get; set; }

        public SystemHttpResponseMessage HttpResponseMessage { get; set; }

        public MockHttpClientException VerifyAll()
        {
            return Matched ? null : MockHttpClientException.UnmatchedResult(this);
        }

        public override string ToString()
        {
            return HttpResponseMessage?.ToString() ?? "";
        }
    }
}