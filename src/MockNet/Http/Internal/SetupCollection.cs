using System.Collections.Generic;
using System.Linq;
using SystemHttpRequestMessage = System.Net.Http.HttpRequestMessage;

namespace MockNet.Http
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

        public IEnumerable<Setup> Find(SystemHttpRequestMessage message)
        {
            // TODO IAsyncEnumerable

            if (setups.Count == 0)
            {
                yield return null;
            }

            lock (setups)
            {
                var matches = new List<Setup>();

                foreach (var setup in setups)
                {
                    var matched = setup.Matches(message).GetAwaiter().GetResult();
                    if (matched)
                    {
                        yield return setup;
                    }
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
