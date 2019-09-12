using System;
using System.Collections.Generic;
using System.Text;

namespace MockClient
{
    public class MockHttpClientException : Exception
    {
        internal static MockHttpClientException NoSetup()
        {
            return new MockHttpClientException("TBD: no setup.");
        }

        internal static MockHttpClientException NoMatchingRequests()
        {
            return new MockHttpClientException("TBD: no matching requests.");
        }

        internal static MockHttpClientException NoMatchingResponses()
        {
            return new MockHttpClientException("TBD: no result setup.");
        }

        internal static MockHttpClientException MatchedMultipleRequests()
        {
            return new MockHttpClientException("TBD: match multiple requests.");
        }

        internal static MockHttpClientException UnmatchedResult(Result result)
        {
            return new MockHttpClientException("TBD: unmatched result.");
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