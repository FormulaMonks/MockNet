using SystemRangeConditionHeaderValue = System.Net.Http.Headers.RangeConditionHeaderValue;

namespace MockClient
{
    public class RangeConditionHeaderValue : IHeaderValue<SystemRangeConditionHeaderValue>
    {
        public static RangeConditionHeaderValue Parse(string input) => SystemRangeConditionHeaderValue.Parse(input);

        private readonly SystemRangeConditionHeaderValue value;

        internal RangeConditionHeaderValue(SystemRangeConditionHeaderValue value)
        {
            this.value = value;
        }

        public SystemRangeConditionHeaderValue GetValue() => value;

        public static implicit operator string(RangeConditionHeaderValue header) => header.GetValue().ToString();
        public static implicit operator RangeConditionHeaderValue(string input) => new RangeConditionHeaderValue(Parse(input));
        public static implicit operator SystemRangeConditionHeaderValue(RangeConditionHeaderValue header) => header.GetValue();
        public static implicit operator RangeConditionHeaderValue(SystemRangeConditionHeaderValue header) => new RangeConditionHeaderValue(header);
    }

}