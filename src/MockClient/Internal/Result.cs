using SystemHttpResponseMessage = System.Net.Http.HttpResponseMessage;

namespace MockClient
{
    internal sealed class Result
    {
        public bool Matched { get; set; }

        public SystemHttpResponseMessage HttpResponseMessage { get; set; }

        public MockHttpClientException VerifyAll()
        {
            return Matched ? null : MockHttpClientException.UnmatchedResult(this);
        }
    }
}