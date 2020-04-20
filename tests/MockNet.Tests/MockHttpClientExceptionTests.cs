using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;
using SystemHttpRequestMessage = System.Net.Http.HttpRequestMessage;
using SystemAuthenticationHeaderValue = System.Net.Http.Headers.AuthenticationHeaderValue;

namespace Theorem.MockNet.Http.Tests
{
    public class MockHttpClientExceptionTests
    {
        [Fact]
        public async Task MockHttpClientExceptionIsThrownIfNoSetup()
        {
            var expected = "\n\nMissing setup for:\nGET http://localhost/\n";
            var mock = new MockHttpClient();

            var exception = await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.GetAsync("/"));

            Assert.Equal(ExceptionReasonTypes.NoStup, exception.Reason);
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public async Task MockHttpClientExceptionIsThrowIfUnmatchedRequestUri()
        {
            var expected = "\n\nExpected Uri 'api' but sent Uri '/api'\n";

            var mock = new MockHttpClient();
            mock.SetupGet("api").ReturnsAsync(200);

            var exception = await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.GetAsync("api"));

            Assert.Equal(ExceptionReasonTypes.UnmatchedRequestUri, exception.Reason);
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public async Task MockHttpClientExceptionIsThrowIfUnmatchedHeaders()
        {
            var expected = "\n\nExpected header validation expression:\nx => (Convert(x.Accept, String) == \"wrong accept header\")\n\nActual headers:\nAccept: application/json\r\n\n";

            var mock = new MockHttpClient();
            mock.SetupGet("/api", headers: x => x.Accept == "wrong accept header").ReturnsAsync(200);

            var request = new SystemHttpRequestMessage(HttpMethod.Get, "/api");
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var exception = await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.SendAsync(request));

            Assert.Equal(ExceptionReasonTypes.UnmatchedHeaders, exception.Reason);
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public async Task MockHttpClientExceptionIsThrowIfUnmatchedContent()
        {
            var expected = "\n\nExpected content validation expression:\nx => (x == \"wrong content body\")\n\nActual content:\nactual body\n";

            var mock = new MockHttpClient();
            mock.SetupPost<string>("/api", content: x => x == "wrong content body").ReturnsAsync(200);

            var request = new SystemHttpRequestMessage(HttpMethod.Get, "/api");
            request.Content = new System.Net.Http.StringContent("actual body");

            var exception = await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.SendAsync(request));

            Assert.Equal(ExceptionReasonTypes.UnmatchedContent, exception.Reason);
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public async Task MockHttpClientExceptionIsThrowIfUnmatchedResult()
        {
            var expected = "\n\nUnmatched setup result:\nStatusCode: 404, ReasonPhrase: 'Not Found', Version: 1.1, Content: <null>, Headers:\r\n{\r\n}\n";

            var mock = new MockHttpClient();
            mock.SetupGet("/api").ReturnsAsync(200).ReturnsAsync(404);

            var result = await mock.Object.GetAsync("/api");

            var exception = Assert.Throws<MockHttpClientException>(() => mock.VerifyAll());

            Assert.Equal(ExceptionReasonTypes.UnmatchedResult, exception.Reason);
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public async Task MockHttpClientExceptionIsThrowIfMatchedMoreThanNRequests()
        {
            var expected = "\n\nExpected request on the mock once, but found 2:\nGET http://localhost/api\n";

            var mock = new MockHttpClient();
            mock.SetupGet("/api").ReturnsAsync(200);
            mock.SetupGet("/api").ReturnsAsync(404);

            var exception = await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.GetAsync("/api"));

            Assert.Equal(ExceptionReasonTypes.MatchedMoreThanNRequests, exception.Reason);
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public async Task MockHttpClientExceptionIsThrowIfNoResponse()
        {
            var expected = "\n\nMissing response for:\nGET http://localhost/api\n";

            var mock = new MockHttpClient();
            mock.SetupGet("/api");

            var exception = await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.GetAsync("/api"));

            Assert.Equal(ExceptionReasonTypes.NoResponse, exception.Reason);
            Assert.Equal(expected, exception.Message);
        }
    }
}