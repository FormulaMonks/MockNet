namespace Theorem.MockNet.Http
{
    public partial class MockHttpClient
    {
        internal static ISetup Setup(MockHttpClient mock, RequestMessage request)
        {
            var setup = new Setup(request);

            mock.Setups.Add(setup);

            return setup;
        }
    }
}