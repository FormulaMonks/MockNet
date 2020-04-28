using System.Collections.Generic;
using System.Linq;
using SystemHttpRequestMessage = System.Net.Http.HttpRequestMessage;

namespace Theorem.MockNet.Http
{
    internal sealed class SetupCollection
    {
        private IList<Setup> setups;

        public SetupCollection()
        {
            this.setups = new List<Setup>();
        }

        public void Add(Setup setup)
        {
            lock (setups)
            {
                setups.Add(setup);
            }
        }

        public IEnumerable<(Setup Setup, MockHttpClientException Exception)> Find(SystemHttpRequestMessage message)
        {
            // TODO IAsyncEnumerable

            if (!setups.Any())
            {
                yield return (null, null);
            }

            lock (setups)
            {
                var matches = new List<Setup>();

                foreach (var setup in setups)
                {
                    var exception = setup.Matches(message).GetAwaiter().GetResult();

                    yield return (setup, exception);
                }
            }
        }

        public IEnumerator<Setup> GetEnumerator()
        {
            // Take local copies of collection and count so they are isolated from changes by other threads.
            Setup[] collection;
            int count;

            lock (setups)
            {
                collection = setups.ToArray();
                count = collection.Count();
            }

            for (var i = 0; i < count; i++)
            {
                yield return collection[i];
            }
        }
    }
}
