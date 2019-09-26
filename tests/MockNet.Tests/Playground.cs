using System;
using System.Threading.Tasks;
using Xunit;
using SystemHttpRequestMessage = System.Net.Http.HttpRequestMessage;

namespace MockNet.Http.Tests
{
    public class Test
    {
        [Fact]
        public async Task MethodAsync()
        {
            var mock = new MockHttpClient();
            mock.Setup(HttpMethod.Get, "/").ReturnsAsync(201);
            var result = await mock.Object.GetAsync("/");
        }

        [Fact]
        public async Task StringAsync()
        {
            var mock = new MockHttpClient();
            mock.Setup<string>(HttpMethod.Post, "/", x => true, x => x == "test").ReturnsAsync(201);
            var result = await mock.Object.PostAsync("/", new StringContent("test"));
        }

        [Fact]
        public async Task StringContentAsync()
        {
            var mock = new MockHttpClient();
            var expected = 201;

            mock.Setup<StringContent>(HttpMethod.Post, "/", x => true, x => x == "test").ReturnsAsync(expected);
            var result = await mock.Object.PostAsync("/", new StringContent("test"));

            Assert.Equal(expected, (int)result.StatusCode);
        }

        [Fact]
        public async Task ReturnsStringContentAsync()
        {
            var mock = new MockHttpClient();
            var expected = new StringContent("result");

            mock.Setup<StringContent>(HttpMethod.Post, "/", x => true, x => x == "test").ReturnsAsync(expected);
            var result = await mock.Object.PostAsync("/", new StringContent("test"));

            var actual = await result.Content.ReadAsStringAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task MultipleReturnsSetupAsync()
        {
            var mock = new MockHttpClient();
            var first = new StringContent("result");
            var second = 201;

            mock.Setup<StringContent>(HttpMethod.Post, "/", x => true, x => x == "test")
                .ReturnsAsync(first)
                .ReturnsAsync(second);

            {
                var result = await mock.Object.PostAsync("/", new StringContent("test"));

                var actual = await result.Content.ReadAsStringAsync();

                Assert.Equal(first, actual);
            }

            {
                var result = await mock.Object.PostAsync("/", new StringContent("test"));

                Assert.Equal(second, (int)result.StatusCode);
            }
        }

        [Fact]
        public async Task MockExceptionIsThrownIfNoSetup()
        {
            var mock = new MockHttpClient();

            await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.GetAsync("/"));
        }

        [Fact]
        public async Task MockExceptionIsThrowIfNoMatchingRequests()
        {
            var mock = new MockHttpClient();
            mock.Setup(HttpMethod.Get, "/").ReturnsAsync(201);

            await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.GetAsync("/invalid"));
        }

        [Fact]
        public async Task MockExceptionIsThrowIfMatchedMultipleRequests()
        {
            var mock = new MockHttpClient();
            mock.Setup(HttpMethod.Get, "/").ReturnsAsync(201);
            mock.Setup(HttpMethod.Get, "/").ReturnsAsync(200);

            await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.GetAsync("/"));
        }

        [Fact]
        public async Task MockExceptionIsThrowIfNoMatchingResponses()
        {
            var mock = new MockHttpClient();
            mock.Setup(HttpMethod.Get, "/");

            await Assert.ThrowsAsync<MockHttpClientException>(() => mock.Object.GetAsync("/"));
        }

        [Fact]
        public async Task MockExceptionIsThrowIfUnmatchedResult()
        {
            var mock = new MockHttpClient();
            mock.Setup(HttpMethod.Get, "/").ReturnsAsync(201);
            mock.Setup(HttpMethod.Get, "/path").ReturnsAsync(200);

            await mock.Object.GetAsync("/");

            Assert.Throws<MockHttpClientException>(() => mock.VerifyAll());
        }

        [Fact]
        public async Task ReturnsHeaderInformation()
        {
            var mock = new MockHttpClient();

            var headers = new HttpResponseHeaders();
            headers.AcceptRanges.Add("none");

            mock.SetupGet("/", x => true).ReturnsAsync(headers);

            var result = await mock.Object.GetAsync("/");

            Assert.Equal(headers.AcceptRanges, result.Headers.AcceptRanges);
        }

        [Fact]
        public async Task ReturnsCustomHeaderInformation()
        {
            var mock = new MockHttpClient();

            var headers = new HttpResponseHeaders();
            headers["x-session-id"] = Guid.NewGuid().ToString();

            mock.SetupGet("/", x => true).ReturnsAsync(headers);

            var result = await mock.Object.GetAsync("/");

            Assert.Equal(headers.GetValues("x-session-id"), result.Headers.GetValues("x-session-id"));
        }

        [Fact]
        public async Task ReturnsMultipleHeaderInformation()
        {
            var mock = new MockHttpClient();

            var headers = new HttpResponseHeaders();
            headers["x-session-id"] = Guid.NewGuid().ToString();
            headers.AcceptRanges.Add("none");
            headers.AcceptRanges.Add("bytes");
            headers.Location = new Uri("https://localhost/v1/customers/1");

            mock.SetupGet("/", x => true).ReturnsAsync(headers);

            var result = await mock.Object.GetAsync("/");

            Assert.Equal(headers.GetValues("x-session-id"), result.Headers.GetValues("x-session-id"));
            Assert.Equal(headers.AcceptRanges, result.Headers.AcceptRanges);
            Assert.Equal(headers.Location, result.Headers.Location);
        }

        [Fact]
        public async Task TestRequestMessage()
        {
            var mock = new MockHttpClient();

            var request = new SystemHttpRequestMessage(HttpMethod.Post, "/");
            request.Content = new System.Net.Http.StringContent("test");
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            mock.SetupPost<StringContent>("/", x => x.Accept == "application/json", x => x == "test").ReturnsAsync(201);

            var result = await mock.Object.SendAsync(request);

            Assert.Equal(201, (int)result.StatusCode);
        }

        [Fact]
        public async Task TestContentHeaders()
        {
            var mock = new MockHttpClient();

            var request = new SystemHttpRequestMessage(HttpMethod.Post, "/");
            request.Content = new System.Net.Http.StringContent("test", System.Text.Encoding.ASCII, "application/json");
            request.Content.Headers.Allow.Add("GET");
            request.Content.Headers.Allow.Add("POST");
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            mock.SetupPost<StringContent>("/", x => x.Accept == "application/json" && x.ContentType == "application/json; charset=us-ascii", x => x == "test").ReturnsAsync(201);

            var result = await mock.Object.SendAsync(request);

            Assert.Equal(201, (int)result.StatusCode);
        }

        [Fact]
        public async Task TestContentHeadersWithStringType()
        {
            var mock = new MockHttpClient();

            var request = new SystemHttpRequestMessage(HttpMethod.Post, "/");
            request.Content = new System.Net.Http.StringContent("test", System.Text.Encoding.ASCII, "application/json");
            request.Content.Headers.Allow.Add("GET");
            request.Content.Headers.Allow.Add("POST");
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            mock.SetupPost<string>("/", x => x.Accept == "application/json" && x.ContentType == "application/json; charset=us-ascii", x => x == "test").ReturnsAsync(201);

            var result = await mock.Object.SendAsync(request);

            Assert.Equal(201, (int)result.StatusCode);
        }
    }
}