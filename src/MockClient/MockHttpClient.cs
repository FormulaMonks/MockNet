using System;
using System.Collections.Generic;
using System.Linq;
using SystemHttpClient = System.Net.Http.HttpClient;

namespace MockClient
{
    public partial class MockHttpClient
    {
        private SystemHttpClient httpClient;
        private readonly MockHandler handler;
        private string baseAddress;

        public SystemHttpClient Object => httpClient;

        internal SetupCollection Setups { get; }

        public MockHttpClient() : this("http://localhost")
        {
        }

        public MockHttpClient(string baseAddress)
        {
            this.baseAddress = baseAddress;
            Setups = new SetupCollection();
            handler = new MockHandler(Setups);
            httpClient = new SystemHttpClient(handler)
            {
                BaseAddress = new Uri(baseAddress),
            };
        }

        public void VerifyAll()
        {
            var error = TryVerify(setup => setup.VerifyAll());
            if (error is MockHttpClientException)
            {
                throw error;
            }
        }

        private MockHttpClientException TryVerify(Func<Setup, MockHttpClientException> verify)
        {
            List<MockHttpClientException> errors = new List<MockHttpClientException>();

            foreach (var setup in Setups)
            {
                var error = verify(setup);
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