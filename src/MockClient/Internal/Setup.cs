using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemHttpRequestMessage = System.Net.Http.HttpRequestMessage;

namespace MockClient
{
    internal class Setup : ISetup
    {
        private readonly RequestMessage request;
        private readonly int defaultHttpStatusCode = 200;

        internal ResultCollection Results { get; }

        public Setup(RequestMessage request)
        {
            this.request = request;
            this.defaultHttpStatusCode = request.HttpMethod.DefaultStatusCode();

            Results = new ResultCollection();
        }

        public IReturns ReturnsAsync(int code)
        {
            return ReturnsAsync(code, null, null);
        }

        public IReturns ReturnsAsync(IContent content)
        {
            return ReturnsAsync(defaultHttpStatusCode, null, content);
        }

        public IReturns ReturnsAsync(HttpResponseHeaders headers)
        {
            return ReturnsAsync(defaultHttpStatusCode, headers, null);
        }

        public IReturns ReturnsAsync(int code, IContent content)
        {
            return ReturnsAsync(code, null, content);
        }

        public IReturns ReturnsAsync(int code, HttpResponseHeaders headers)
        {
            return ReturnsAsync(code, headers, null);
        }

        public IReturns ReturnsAsync(HttpResponseHeaders headers, IContent content)
        {
            return ReturnsAsync(defaultHttpStatusCode, headers, content);
        }

        public IReturns ReturnsAsync(int code, HttpResponseHeaders headers, IContent content)
        {
            var message = new HttpResponseMessageBuilder()
                .WithStatusCode(code)
                .WithContent(content)
                .WithHeaders(headers)
                .Build();

            Results.Add(new Result()
            {
                HttpResponseMessage = message
            });

            return this;
        }

        public async Task<bool> Matches(SystemHttpRequestMessage message)
        {
            // TODO: better match request uri
            if (request.RequestUri != message.RequestUri.PathAndQuery)
            {
                return false;
            }

            if (request.HeadersValidator is Delegate)
            {
                var headers = new HttpRequestHeaders(message.Headers);
                var b = request.HeadersValidator.DynamicInvoke(headers);
                if (b is false)
                {
                    return false;
                }
            }

            if (request.ContentValidator is Delegate)
            {
                var content = await request.Deserializer(message);
                var b = request.ContentValidator.DynamicInvoke(content);
                if (b is false)
                {
                    return false;
                }
            }

            return true;
        }

        public MockHttpClientException VerifyAll()
        {
            return TryVerify(result => result.VerifyAll());
        }

        private MockHttpClientException TryVerify(Func<Result, MockHttpClientException> verify)
        {
            List<MockHttpClientException> errors = new List<MockHttpClientException>();

            foreach (var result in Results)
            {
                var error = verify(result);
                if (error is MockHttpClientException)
                {
                    errors.Add(error);
                }
            }

            if (errors.Any())
            {
                return MockHttpClientException.Combined(errors);
            }

            return null;
        }
    }
}