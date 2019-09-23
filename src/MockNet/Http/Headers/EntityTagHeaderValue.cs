using SystemEntityTagHeaderValue = System.Net.Http.Headers.EntityTagHeaderValue;

namespace MockNet.Http
{
    public class EntityTagHeaderValue : IHeaderValue<SystemEntityTagHeaderValue>
    {
        public static EntityTagHeaderValue Parse(string input) => SystemEntityTagHeaderValue.Parse(input);

        private readonly SystemEntityTagHeaderValue value;

        internal EntityTagHeaderValue(SystemEntityTagHeaderValue value)
        {
            this.value = value;
        }

        public SystemEntityTagHeaderValue GetValue() => value;

        public override string ToString() => value.ToString();

        public static implicit operator string(EntityTagHeaderValue header) => header.ToString();
        public static implicit operator EntityTagHeaderValue(string input) => new EntityTagHeaderValue(Parse(input));
        public static implicit operator SystemEntityTagHeaderValue(EntityTagHeaderValue header) => header.GetValue();
        public static implicit operator EntityTagHeaderValue(SystemEntityTagHeaderValue header) => new EntityTagHeaderValue(header);
    }
}