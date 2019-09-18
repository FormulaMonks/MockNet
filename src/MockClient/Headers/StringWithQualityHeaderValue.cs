using SystemStringWithQualityHeaderValue = System.Net.Http.Headers.StringWithQualityHeaderValue;

namespace MockClient
{
    public class StringWithQualityHeaderValue : IHeaderValue<SystemStringWithQualityHeaderValue>
    {
        public static StringWithQualityHeaderValue Parse(string input) => SystemStringWithQualityHeaderValue.Parse(input);

        private readonly SystemStringWithQualityHeaderValue value;

        internal StringWithQualityHeaderValue(SystemStringWithQualityHeaderValue value)
        {
            this.value = value;
        }

        public SystemStringWithQualityHeaderValue GetValue() => value;

        public override string ToString() => value.ToString();

        public static implicit operator string(StringWithQualityHeaderValue header) => header.ToString();
        public static implicit operator StringWithQualityHeaderValue(string input) => new StringWithQualityHeaderValue(Parse(input));
        public static implicit operator SystemStringWithQualityHeaderValue(StringWithQualityHeaderValue header) => header.GetValue();
        public static implicit operator StringWithQualityHeaderValue(SystemStringWithQualityHeaderValue header) => new StringWithQualityHeaderValue(header);
    }
}