using SystemMediaTypeWithQualityHeaderValue = System.Net.Http.Headers.MediaTypeWithQualityHeaderValue;

namespace MockClient
{
    public class MediaTypeWithQualityHeaderValue : IHeaderValue<SystemMediaTypeWithQualityHeaderValue>
    {
        public static MediaTypeWithQualityHeaderValue Parse(string input) => SystemMediaTypeWithQualityHeaderValue.Parse(input);

        private readonly SystemMediaTypeWithQualityHeaderValue value;

        internal MediaTypeWithQualityHeaderValue(SystemMediaTypeWithQualityHeaderValue value)
        {
            this.value = value;
        }

        public SystemMediaTypeWithQualityHeaderValue GetValue() => value;

        public static implicit operator string(MediaTypeWithQualityHeaderValue header) => header.GetValue().ToString();
        public static implicit operator MediaTypeWithQualityHeaderValue(string input) => new MediaTypeWithQualityHeaderValue(Parse(input));
        public static implicit operator SystemMediaTypeWithQualityHeaderValue(MediaTypeWithQualityHeaderValue header) => header.GetValue();
        public static implicit operator MediaTypeWithQualityHeaderValue(SystemMediaTypeWithQualityHeaderValue header) => new MediaTypeWithQualityHeaderValue(header);
    }

}