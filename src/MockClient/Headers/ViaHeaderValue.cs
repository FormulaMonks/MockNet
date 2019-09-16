using SystemViaHeaderValue = System.Net.Http.Headers.ViaHeaderValue;

namespace MockClient
{
    public class ViaHeaderValue : IHeaderValue<SystemViaHeaderValue>
    {
        public static ViaHeaderValue Parse(string input) => SystemViaHeaderValue.Parse(input);

        private readonly SystemViaHeaderValue value;

        internal ViaHeaderValue(SystemViaHeaderValue value)
        {
            this.value = value;
        }

        public SystemViaHeaderValue GetValue() => value;

        public static implicit operator string(ViaHeaderValue header) => header.GetValue().ToString();
        public static implicit operator ViaHeaderValue(string input) => new ViaHeaderValue(Parse(input));
        public static implicit operator SystemViaHeaderValue(ViaHeaderValue header) => header.GetValue();
        public static implicit operator ViaHeaderValue(SystemViaHeaderValue header) => new ViaHeaderValue(header);
    }
}