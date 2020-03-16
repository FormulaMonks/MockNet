using SystemMediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace Theorem.MockNet.Http
{
    public class MediaTypeHeaderValue : IHeaderValue<SystemMediaTypeHeaderValue>
    {
        public static MediaTypeHeaderValue Parse(string input) => SystemMediaTypeHeaderValue.Parse(input);

        private readonly SystemMediaTypeHeaderValue value;

        internal MediaTypeHeaderValue(SystemMediaTypeHeaderValue value)
        {
            this.value = value;
        }

        public SystemMediaTypeHeaderValue GetValue() => value;

        public override string ToString() => value.ToString();

        public static implicit operator string(MediaTypeHeaderValue header) => header.ToString();
        public static implicit operator MediaTypeHeaderValue(string input) => new MediaTypeHeaderValue(Parse(input));
        public static implicit operator SystemMediaTypeHeaderValue(MediaTypeHeaderValue header) => header.GetValue();
        public static implicit operator MediaTypeHeaderValue(SystemMediaTypeHeaderValue header) => new MediaTypeHeaderValue(header);
    }
}