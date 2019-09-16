using SystemCacheControlHeaderValue = System.Net.Http.Headers.CacheControlHeaderValue;

namespace MockClient
{
    public class CacheControlHeaderValue : IHeaderValue<SystemCacheControlHeaderValue>
    {
        public static CacheControlHeaderValue Parse(string input) => SystemCacheControlHeaderValue.Parse(input);

        private readonly SystemCacheControlHeaderValue value;

        internal CacheControlHeaderValue(SystemCacheControlHeaderValue value)
        {
            this.value = value;
        }

        public SystemCacheControlHeaderValue GetValue() => value;

        public static implicit operator string(CacheControlHeaderValue header) => header.GetValue().ToString();
        public static implicit operator CacheControlHeaderValue(string input) => new CacheControlHeaderValue(Parse(input));
        public static implicit operator SystemCacheControlHeaderValue(CacheControlHeaderValue header) => header.GetValue();
        public static implicit operator CacheControlHeaderValue(SystemCacheControlHeaderValue header) => new CacheControlHeaderValue(header);
    }
}