using System.Collections.Generic;

namespace MockClient
{
    public sealed class HttpResponseHeaders
    {
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            yield return new KeyValuePair<string, string>(null, null);
        }
    }
}