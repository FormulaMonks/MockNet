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
            var message = new StringBuilder()
                .AppendLine()
                .AppendLine()
                .AppendLine("Missing setup for:")
                .Append(await Utils.HttpRequestMessage.ToStringAsync(request))
                .AppendLine();

            return new MockHttpClientException(ExceptionReasonTypes.NoSetup, message.ToString());
        }

        internal static MockHttpClientException UnmatchedRequestUri(string expectedUri, string actualUri)
        {
            var message = new StringBuilder()
                .AppendLine()
                .AppendLine()
                .AppendLine($"Expected Uri '{expectedUri}' but sent Uri '{actualUri}'");

            return new MockHttpClientException(ExceptionReasonTypes.UnmatchedRequestUri, message.ToString());
        }

        internal static MockHttpClientException UnmatchedRequestHeaders(Expression headers,  HttpRequestHeaders actual)
        {
            var message = new StringBuilder()
                .AppendLine()
                .AppendLine()
                .AppendLine($"Expected header validation expression:")
                .AppendLine(headers.ToString()) // TODO: try to remove the "Convert" expression type
                .AppendLine()
                .AppendLine("Actual headers:")
                .AppendLine(actual.ToString());

            return new MockHttpClientException(ExceptionReasonTypes.UnmatchedHeaders, message.ToString());
        }

        internal static MockHttpClientException UnmatchedRequestContent(Expression content,  object actual)
        {
            var message = new StringBuilder()
                .AppendLine()
                .AppendLine()
                .AppendLine($"Expected content validation expression:")
                .AppendLine(content.ToString())
                .AppendLine()
                .AppendLine("Actual content:")
                .AppendLine(actual.ToString());

            return new MockHttpClientException(ExceptionReasonTypes.UnmatchedContent, message.ToString());
        }

        internal static async Task<MockHttpClientException> NoMatchingResponses(SystemHttpRequestMessage request)
        {
            var message = new StringBuilder()
                .AppendLine()
                .AppendLine()
                .AppendLine("Missing response for:")
                .Append(await Utils.HttpRequestMessage.ToStringAsync(request))
                .AppendLine();

            return new MockHttpClientException(ExceptionReasonTypes.NoResponse, message.ToString());
        }

        internal static async Task<MockHttpClientException> MatchedMultipleRequests(SystemHttpRequestMessage request, int requestCount)
        {
            var message = new StringBuilder()
                .AppendLine()
                .AppendLine()
                .AppendLine($"Expected request on the mock once, but found {requestCount}:")
                .AppendLine(await Utils.HttpRequestMessage.ToStringAsync(request));

            return new MockHttpClientException(ExceptionReasonTypes.MatchedMoreThanNRequests, message.ToString());
        }

        internal static MockHttpClientException UnmatchedResult(Result result)
        {
            var message = new StringBuilder()
                .AppendLine()
                .AppendLine()
                .AppendLine("Unmatched setup result:")
                .AppendLine(result.ToString());

            return new MockHttpClientException(ExceptionReasonTypes.UnmatchedResult, message.ToString());
        }

        internal static MockHttpClientException Combined(IEnumerable<MockHttpClientException> errors, string preamble = null)
        {
            var list = errors.Select(x => x.Message).ToList();

            if (!string.IsNullOrWhiteSpace(preamble))
            {
                list.Insert(0, preamble);
            }

            var message = new StringBuilder().Join(x => x.AppendLine(), list);
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
