using System.Threading.Tasks;
using Xunit;

namespace MockClient.Tests
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
            headers[""] = "";
            headers.Location = new System.Uri("");

            mock.SetupPost<StringContent>("/", x => x.Accept == "application/json", x => x == "test").ReturnsAsync(headers);

            var result = await mock.Object.GetAsync("/");

            // Assert.Equal(headers.Location, result.Headers.Location.ToString());
        }
    }
}