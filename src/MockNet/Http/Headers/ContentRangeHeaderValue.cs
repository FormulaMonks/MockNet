using SystemContentRangeHeaderValue = System.Net.Http.Headers.ContentRangeHeaderValue;

namespace MockNet.Http
{
    public class ContentRangeHeaderValue : IHeaderValue<SystemContentRangeHeaderValue>
    {
        public static ContentRangeHeaderValue Parse(string input) => SystemContentRangeHeaderValue.Parse(input);

        private readonly SystemContentRangeHeaderValue value;

        internal ContentRangeHeaderValue(SystemContentRangeHeaderValue value)
        {
            this.value = value;
        }

        public SystemContentRangeHeaderValue GetValue() => value;

        public override string ToString() => value.ToString();

        public static implicit operator string(ContentRangeHeaderValue header) => header.ToString();
        public static implicit operator ContentRangeHeaderValue(string input) => new ContentRangeHeaderValue(Parse(input));
        public static implicit operator SystemContentRangeHeaderValue(ContentRangeHeaderValue header) => header.GetValue();
        public static implicit operator ContentRangeHeaderValue(SystemContentRangeHeaderValue header) => new ContentRangeHeaderValue(header);
    }
}