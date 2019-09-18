using SystemRangeHeaderValue = System.Net.Http.Headers.RangeHeaderValue;

namespace MockClient
{
    public class RangeHeaderValue : IHeaderValue<SystemRangeHeaderValue>
    {
        public static RangeHeaderValue Parse(string input) => SystemRangeHeaderValue.Parse(input);

        private readonly SystemRangeHeaderValue value;

        internal RangeHeaderValue(SystemRangeHeaderValue value)
        {
            this.value = value;
        }

        public SystemRangeHeaderValue GetValue() => value;

        public override string ToString() => value.ToString();

        public static implicit operator string(RangeHeaderValue header) => header.ToString();
        public static implicit operator RangeHeaderValue(string input) => new RangeHeaderValue(Parse(input));
        public static implicit operator SystemRangeHeaderValue(RangeHeaderValue header) => header.GetValue();
        public static implicit operator RangeHeaderValue(SystemRangeHeaderValue header) => new RangeHeaderValue(header);
    }

}