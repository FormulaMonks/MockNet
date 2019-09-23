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

        protected override Task<SystemHttpResponseMessage> SendAsync(SystemHttpRequestMessage request, CancellationToken cancellationToken)
        {
            var setups = this.setups.Find(request).ToList();

            if (!setups.Any())
            {
                throw MockHttpClientException.NoMatchingRequests();
            }

            if (setups.Count() > 1)
            {
                throw MockHttpClientException.MatchedMultipleRequests();
            }

            var setup = setups.First();

            if (setup is null)
            {
                throw MockHttpClientException.NoSetup();
            }

            var result = setup.Results.GetResultNext();

            if (result is null)
            {
                throw MockHttpClientException.NoMatchingResponses();
            }

            result.Matched = true;

            return Task.FromResult(result.HttpResponseMessage);
        }
    }
}