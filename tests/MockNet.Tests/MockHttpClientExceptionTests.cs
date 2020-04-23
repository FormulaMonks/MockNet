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
            var expected = "\n\nMissing setup for:\nGET /\n";
            var mock = new MockHttpClient();

            var exception = await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.GetAsync("/"));

            Assert.Equal(ExceptionReasonTypes.NoSetup, exception.Reason);
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public async Task MockHttpClientExceptionIsThrownIfUnmatchedRequestUri()
        {
            var expected = "\n\nSetup:\nSetupGet(\"api\")\n\nDid not match the Uri for request:\nGET /api\n";

            var mock = new MockHttpClient();
            mock.SetupGet("api").ReturnsAsync(200);

            var exception = await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.GetAsync("api"));

            Assert.Equal(ExceptionReasonTypes.UnmatchedRequestUri, exception.Reason);
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public async Task MockHttpClientExceptionIsThrownIfUnmatchedHeaders()
        {
            var expected = "\n\nSetup:\nSetupGet(\"/api\", headers: x => (Convert(x.Accept, String) == \"wrong accept header\"))\n\nDid not match the headers for request:\nGET /api\nAccept: application/json\n";

            var mock = new MockHttpClient();
            mock.SetupGet("/api", headers: x => x.Accept == "wrong accept header").ReturnsAsync(200);

            var request = new SystemHttpRequestMessage(HttpMethod.Get, "/api");
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var exception = await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.SendAsync(request));

            Assert.Equal(ExceptionReasonTypes.UnmatchedHeaders, exception.Reason);
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public async Task MockHttpClientExceptionIsThrownIfUnmatchedContent()
        {
            var expected = "\n\nSetup:\nSetupPost<System.String>(\"/api\", content: x => (x == \"wrong content body\"))\n\nDid not match the content for request:\nPOST /api\nContent-Type: text/plain; charset=utf-8\r\n\nactual body\n";

            var mock = new MockHttpClient();
            mock.SetupPost<string>("/api", content: x => x == "wrong content body").ReturnsAsync(200);

            var request = new SystemHttpRequestMessage(HttpMethod.Post, "/api");
            request.Content = new System.Net.Http.StringContent("actual body");

            var exception = await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.SendAsync(request));

            Assert.Equal(ExceptionReasonTypes.UnmatchedContent, exception.Reason);
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public async Task MockHttpClientExceptionIsThrownIfMultipleUnmatchesOccur()
        {
            var expected =
                "\n\nSetup:\nSetupGet(\"/api\")\n\nDid not match the HTTP method for request:\nPOST /api\nAccept: application/json\r\nContent-Type: text/plain; charset=utf-8\r\n\nactual body\n"
                + "-------\n\nSetup:\nSetupPost(\"api\")\n\nDid not match the Uri for request:\nPOST /api\nAccept: application/json\r\nContent-Type: text/plain; charset=utf-8\r\n\nactual body\n"
                + "-------\n\nSetup:\nSetupPost(\"/api\", headers: x => (Convert(x.Accept, String) == \"wrong accept header\"))\n\nDid not match the headers for request:\nPOST /api\nAccept: application/json\r\nContent-Type: text/plain; charset=utf-8\r\n\nactual body\n"
                + "-------\n\nSetup:\nSetupPost<System.String>(\"/api\", content: x => (x == \"wrong content body\"))\n\nDid not match the content for request:\nPOST /api\nAccept: application/json\r\nContent-Type: text/plain; charset=utf-8\r\n\nactual body\n";

            var mock = new MockHttpClient();
            mock.SetupGet("/api").ReturnsAsync(200);
            mock.SetupPost("api").ReturnsAsync(201);
            mock.SetupPost("/api", headers: x => x.Accept == "wrong accept header").ReturnsAsync(201);
            mock.SetupPost<string>("/api", content: x => x == "wrong content body").ReturnsAsync(201);

            var request = new SystemHttpRequestMessage(HttpMethod.Post, "/api");
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new System.Net.Http.StringContent("actual body");

            var exception = await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.SendAsync(request));

            Assert.True(exception.Reason.HasFlag(ExceptionReasonTypes.UnmatchedHttpMethod));
            Assert.True(exception.Reason.HasFlag(ExceptionReasonTypes.UnmatchedRequestUri));
            Assert.True(exception.Reason.HasFlag(ExceptionReasonTypes.UnmatchedHeaders));
            Assert.True(exception.Reason.HasFlag(ExceptionReasonTypes.UnmatchedContent));
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public async Task MockHttpClientExceptionIsThrownIfUnmatchedResult()
        {
            var expected = "\n\nUnmatched setup result:\nStatusCode: 404, ReasonPhrase: 'Not Found', Version: 1.1, Content: System.Net.Http.StringContent, Headers:\r\n{\r\n  Content-Type: text/plain; charset=utf-8\r\n}\n";

            var mock = new MockHttpClient();
            var content = new StringContent("content");
            mock.SetupPost("/api").ReturnsAsync(200).ReturnsAsync(404, content);

            var request = new SystemHttpRequestMessage(HttpMethod.Post, "/api");
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new System.Net.Http.StringContent("actual body");

            var result = await mock.Object.SendAsync(request);

            var exception = Assert.Throws<MockHttpClientException>(() => mock.VerifyAll());

            Assert.Equal(ExceptionReasonTypes.UnmatchedResult, exception.Reason);
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public async Task MockHttpClientExceptionIsThrownIfMatchedMoreThanNRequests()
        {
            var expected = "\n\nExpected request on the mock once, but found 2:\nGET /api\n";

            var mock = new MockHttpClient();
            mock.SetupGet("/api").ReturnsAsync(200);
            mock.SetupGet("/api").ReturnsAsync(404);

            var exception = await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.GetAsync("/api"));

            Assert.Equal(ExceptionReasonTypes.MatchedMoreThanNRequests, exception.Reason);
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public async Task MockHttpClientExceptionIsThrownIfNoResponse()
        {
            var expected = "\n\nMissing response for:\nGET /api\n";

            var mock = new MockHttpClient();
            mock.SetupGet("/api");

            var exception = await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.GetAsync("/api"));

            Assert.Equal(ExceptionReasonTypes.NoResponse, exception.Reason);
            Assert.Equal(expected, exception.Message);
        }
    }
}