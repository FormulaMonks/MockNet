using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using SystemHttpRequestMessage = System.Net.Http.HttpRequestMessage;

namespace Theorem.MockNet.Http
{
    public class MockHttpClientException : Exception
    {
        internal static async Task<MockHttpClientException> NoSetupAsync(SystemHttpRequestMessage request)
        {
            return new MockHttpClientException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    @"{0} request failed.\n{1}",
                    await Utils.HttpRequestMessage.ToStringAsync(request),
                    "All requests on the mock must have a corresponding setup."));
        }

        internal static async Task<MockHttpClientException> NoMatchingRequestsAsync(SystemHttpRequestMessage request)
        {
            return new MockHttpClientException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    @"{0}\n{1}",
                    await Utils.HttpRequestMessage.ToStringAsync(request),
                    "No requests found."));
        }

        internal static async Task<MockHttpClientException> NoMatchingResponses(SystemHttpRequestMessage request)
        {
            return new MockHttpClientException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    @"{0} request failed.\n{1}",
                    await Utils.HttpRequestMessage.ToStringAsync(request),
                    "All requests on the mock must have a corresponding setup."));
        }

        internal static async Task<MockHttpClientException> MatchedMultipleRequests(SystemHttpRequestMessage request, int requestCount)
        {
            return new MockHttpClientException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "Expected request on the mock once, but found {0} : {1}",
                    requestCount,
                    await Utils.HttpRequestMessage.ToStringAsync(request)));
        }

        internal static MockHttpClientException UnmatchedResult(Result result)
        {
            return new MockHttpClientException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    @"{0}:\nThis setup was not matched.",
                    result));
        }

        internal static MockHttpClientException Combined(IEnumerable<MockHttpClientException> errors, string preamble = null)
        {
            var message = new StringBuilder();

            if (preamble is string)
            {
                message.Append(preamble)
                    .AppendLine()
                    .AppendLine();
            }

            foreach (var error in errors)
            {
                message.Append(error.Message)
                    .AppendLine()
                    .AppendLine();
            }

            return new MockHttpClientException(message.ToString());
        }


        internal string Reason { get; }

        internal MockHttpClientException(string message) : base(message)
        {

        }
    }
}
