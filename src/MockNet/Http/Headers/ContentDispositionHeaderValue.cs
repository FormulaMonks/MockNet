using SystemContentDispositionHeaderValue = System.Net.Http.Headers.ContentDispositionHeaderValue;

namespace MockNet.Http
{
    public class ContentDispositionHeaderValue : IHeaderValue<SystemContentDispositionHeaderValue>
    {
        public static ContentDispositionHeaderValue Parse(string input) => SystemContentDispositionHeaderValue.Parse(input);

        private readonly SystemContentDispositionHeaderValue value;

        internal ContentDispositionHeaderValue(SystemContentDispositionHeaderValue value)
        {
            this.value = value;
        }

        public SystemContentDispositionHeaderValue GetValue() => value;

        public override string ToString() => value.ToString();

        public static implicit operator string(ContentDispositionHeaderValue header) => header.ToString();
        public static implicit operator ContentDispositionHeaderValue(string input) => new ContentDispositionHeaderValue(Parse(input));
        public static implicit operator SystemContentDispositionHeaderValue(ContentDispositionHeaderValue header) => header.GetValue();
        public static implicit operator ContentDispositionHeaderValue(SystemContentDispositionHeaderValue header) => new ContentDispositionHeaderValue(header);
    }
}