using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SystemHttpRequestMessage = System.Net.Http.HttpRequestMessage;

namespace Theorem.MockNet.Http
{
    public class MockHttpClientException : Exception
    {
        internal static async Task<MockHttpClientException> NoSetupAsync(SystemHttpRequestMessage request)
        {
            var message = $"\n\nMissing setup for:\n{await Utils.HttpRequestMessage.ToStringAsync(request)}\n";

            return new MockHttpClientException(ExceptionReasonTypes.NoSetup, message.ToString());
        }

        internal static async Task<MockHttpClientException> UnmatchedRequestUri(RequestMessage requestMessage, SystemHttpRequestMessage request)
        {
            var message = $"\n\nSetup:\n{requestMessage.ToString()}\n\nDid not match the Uri for request:\n{await Utils.HttpRequestMessage.ToStringAsync(request)}\n";

            return new MockHttpClientException(ExceptionReasonTypes.UnmatchedRequestUri, message.ToString());
        }

        internal static async Task<MockHttpClientException> UnmatchedHttpMethod(RequestMessage requestMessage, SystemHttpRequestMessage request)
        {
            var message = $"\n\nSetup:\n{requestMessage.ToString()}\n\nDid not match the HTTP method for request:\n{await Utils.HttpRequestMessage.ToStringAsync(request)}\n";

            return new MockHttpClientException(ExceptionReasonTypes.UnmatchedHttpMethod, message.ToString());
        }

        internal static async Task<MockHttpClientException> UnmatchedRequestHeaders(RequestMessage requestMessage, SystemHttpRequestMessage request)
        {
            var message = $"\n\nSetup:\n{requestMessage.ToString()}\n\nDid not match the headers for request:\n{await Utils.HttpRequestMessage.ToStringAsync(request)}\n";

            return new MockHttpClientException(ExceptionReasonTypes.UnmatchedHeaders, message.ToString());
        }

        internal static async Task<MockHttpClientException> UnmatchedRequestContent(RequestMessage requestMessage, SystemHttpRequestMessage request)
        {
            var message = $"\n\nSetup:\n{requestMessage.ToString()}\n\nDid not match the content for request:\n{await Utils.HttpRequestMessage.ToStringAsync(request)}\n";

            return new MockHttpClientException(ExceptionReasonTypes.UnmatchedContent, message.ToString());
        }

        internal static async Task<MockHttpClientException> NoMatchingResponses(SystemHttpRequestMessage request)
        {
            var message = $"\n\nMissing response for:\n{await Utils.HttpRequestMessage.ToStringAsync(request)}\n";

            return new MockHttpClientException(ExceptionReasonTypes.NoResponse, message.ToString());
        }

        internal static async Task<MockHttpClientException> MatchedMultipleRequests(SystemHttpRequestMessage request, int requestCount)
        {
            var message = $"\n\nExpected request on the mock once, but found {requestCount}:\n{await Utils.HttpRequestMessage.ToStringAsync(request)}\n";

            return new MockHttpClientException(ExceptionReasonTypes.MatchedMoreThanNRequests, message.ToString());
        }

        internal static MockHttpClientException UnmatchedResult(Result result)
        {
            var message = $"\n\nUnmatched setup result:\n{result.ToString()}\n";

            return new MockHttpClientException(ExceptionReasonTypes.UnmatchedResult, message.ToString());
        }

        internal static MockHttpClientException Combined(IEnumerable<MockHttpClientException> errors, string preamble = null)
        {
            var list = errors.Select(x => x.Message).ToList();

            if (!string.IsNullOrWhiteSpace(preamble))
            {
                list.Insert(0, preamble);
            }

            var message = new StringBuilder().Join(x => x.Append("-------"), list);
            var reasons = errors.Select(x => x.Reason).Aggregate((ExceptionReasonTypes)0, (a, c) => { a |= c; return a; });

            return new MockHttpClientException(reasons, message.ToString());
        }

        internal ExceptionReasonTypes Reason { get; }

        internal MockHttpClientException(ExceptionReasonTypes reason, string message) : base(message)
        {
            Reason = reason;
        }
    }
}
