using System.Linq;
using System.Threading.Tasks;
using SystemDelegatingHandler = System.Net.Http.DelegatingHandler;
using SystemHttpResponseMessage = System.Net.Http.HttpResponseMessage;
using SystemHttpRequestMessage = System.Net.Http.HttpRequestMessage;
using System.Threading;

namespace Theorem.MockNet.Http
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

            // there are no setups that were found successful.
            if (setups.All(x => x.Exception is MockHttpClientException))
            {
                throw MockHttpClientException.Combined(setups.Select(x => x.Exception));
            }

            // there are more than 1 setup that was found.
            if (setups.Count(x => x.Exception is null) > 1)
            {
                throw await MockHttpClientException.MatchedMultipleRequests(request, setups.Count());
            }

            var (setup, exception) = setups.FirstOrDefault(x => x.Exception is null);

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