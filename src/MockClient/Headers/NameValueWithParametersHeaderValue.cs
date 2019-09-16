using SystemNameValueWithParametersHeaderValue = System.Net.Http.Headers.NameValueWithParametersHeaderValue;

namespace MockClient
{
    public class NameValueWithParametersHeaderValue : IHeaderValue<SystemNameValueWithParametersHeaderValue>
    {
        public static NameValueWithParametersHeaderValue Parse(string input) => SystemNameValueWithParametersHeaderValue.Parse(input);

        private readonly SystemNameValueWithParametersHeaderValue value;

        internal NameValueWithParametersHeaderValue(SystemNameValueWithParametersHeaderValue value)
        {
            this.value = value;
        }

        public SystemNameValueWithParametersHeaderValue GetValue() => value;

        public static implicit operator string(NameValueWithParametersHeaderValue header) => header.GetValue().ToString();
        public static implicit operator NameValueWithParametersHeaderValue(string input) => new NameValueWithParametersHeaderValue(Parse(input));
        public static implicit operator SystemNameValueWithParametersHeaderValue(NameValueWithParametersHeaderValue header) => header.GetValue();
        public static implicit operator NameValueWithParametersHeaderValue(SystemNameValueWithParametersHeaderValue header) => new NameValueWithParametersHeaderValue(header);
    }

}