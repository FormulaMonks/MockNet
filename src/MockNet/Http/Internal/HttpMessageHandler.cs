using System.Linq;
using System.Threading.Tasks;
using SystemDelegatingHandler = System.Net.Http.DelegatingHandler;
using SystemHttpResponseMessage = System.Net.Http.HttpResponseMessage;
using SystemHttpRequestMessage = System.Net.Http.HttpRequestMessage;
using System.Threading;

namespace MockNet.Http
{
    internal sealed class HttpMessageHandler : SystemDelegatingHandler
    {
        private readonly SetupCollection setups;

        public HttpMessageHandler(SetupCollection setups)
        {
            this.setups = setups;
        }

        protected override async Task<SystemHttpResponseMessage> SendAsync(SystemHttpRequestMessage request, CancellationToken cancellationToken)
        {
            var setups = this.setups.Find(request).ToList();

            if (!setups.Any())
            {
                throw await MockHttpClientException.NoMatchingRequestsAsync(request);
            }

            if (setups.Count() > 1)
            {
                throw await MockHttpClientException.MatchedMultipleRequests(request, setups.Count());
            }

            var setup = setups.First();

            if (setup is null)
            {
                throw await MockHttpClientException.NoSetupAsync(request);
            }

            var result = setup.Results.GetResultNext();

            if (result is null)
            {
                throw await MockHttpClientException.NoMatchingResponses(request);
            }

            result.Matched = true;

            return result.HttpResponseMessage;
        }
    }
}