using SystemWarningHeaderValue = System.Net.Http.Headers.WarningHeaderValue;

namespace MockClient
{
    public class WarningHeaderValue : IHeaderValue<SystemWarningHeaderValue>
    {
        public static WarningHeaderValue Parse(string input) => SystemWarningHeaderValue.Parse(input);

        private readonly SystemWarningHeaderValue value;

        internal WarningHeaderValue(SystemWarningHeaderValue value)
        {
            this.value = value;
        }

        public SystemWarningHeaderValue GetValue() => value;

        public override string ToString() => value.ToString();

        public static implicit operator string(WarningHeaderValue header) => header.ToString();
        public static implicit operator WarningHeaderValue(string input) => new WarningHeaderValue(Parse(input));
        public static implicit operator SystemWarningHeaderValue(WarningHeaderValue header) => header.GetValue();
        public static implicit operator WarningHeaderValue(SystemWarningHeaderValue header) => new WarningHeaderValue(header);
    }
}