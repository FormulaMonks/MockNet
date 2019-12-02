using System.Collections.Generic;
using System.Linq;

namespace MockNet.Http
{
    internal sealed class ResultCollection
    {
        private IList<Result> results;

        public ResultCollection()
        {
            this.results = new List<Result>();
        }

        public void Add(Result result)
        {
            lock (results)
            {
                results.Add(result);
            }
        }

        public Result GetResultNext()
        {
            if (results.Count == 0)
            {
                return null;
            }

            lock (results)
            {
                return results.FirstOrDefault(x => !x.Matched);
            }
        }

        public IEnumerator<Result> GetEnumerator()
        {
            // Take local copies of collection and count so they are isolated from changes by other threads.
            Result[] collection;
            int count;

            lock (results)
            {
                collection = results.ToArray();
                count = collection.Count();
            }

            for (var i = 0; i < count; i++)
            {
                yield return collection[i];
            }
        }
    }
}
