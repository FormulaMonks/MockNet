using SystemNameValueHeaderValue = System.Net.Http.Headers.NameValueHeaderValue;

namespace MockClient
{
    public class NameValueHeaderValue : IHeaderValue<SystemNameValueHeaderValue>
    {
        public static NameValueHeaderValue Parse(string input) => SystemNameValueHeaderValue.Parse(input);

        private readonly SystemNameValueHeaderValue value;

        internal NameValueHeaderValue(SystemNameValueHeaderValue value)
        {
            this.value = value;
        }

        public SystemNameValueHeaderValue GetValue() => value;

        public static implicit operator string(NameValueHeaderValue header) => header.GetValue().ToString();
        public static implicit operator NameValueHeaderValue(string input) => new NameValueHeaderValue(Parse(input));
        public static implicit operator SystemNameValueHeaderValue(NameValueHeaderValue header) => header.GetValue();
        public static implicit operator NameValueHeaderValue(SystemNameValueHeaderValue header) => new NameValueHeaderValue(header);
    }
}