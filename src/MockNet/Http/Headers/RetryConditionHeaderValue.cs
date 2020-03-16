using SystemRetryConditionHeaderValue = System.Net.Http.Headers.RetryConditionHeaderValue;

namespace Theorem.MockNet.Http
{
    public class RetryConditionHeaderValue : IHeaderValue<SystemRetryConditionHeaderValue>
    {
        public static RetryConditionHeaderValue Parse(string input) => SystemRetryConditionHeaderValue.Parse(input);

        private readonly SystemRetryConditionHeaderValue value;

        internal RetryConditionHeaderValue(SystemRetryConditionHeaderValue value)
        {
            this.value = value;
        }

        public SystemRetryConditionHeaderValue GetValue() => value;

        public override string ToString() => value.ToString();

        public static implicit operator string(RetryConditionHeaderValue header) => header.ToString();
        public static implicit operator RetryConditionHeaderValue(string input) => new RetryConditionHeaderValue(Parse(input));
        public static implicit operator SystemRetryConditionHeaderValue(RetryConditionHeaderValue header) => header.GetValue();
        public static implicit operator RetryConditionHeaderValue(SystemRetryConditionHeaderValue header) => new RetryConditionHeaderValue(header);
    }
}