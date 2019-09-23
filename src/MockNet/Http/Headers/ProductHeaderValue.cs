using SystemProductHeaderValue = System.Net.Http.Headers.ProductHeaderValue;

namespace MockNet.Http
{
    public class ProductHeaderValue : IHeaderValue<SystemProductHeaderValue>
    {
        public static ProductHeaderValue Parse(string input) => SystemProductHeaderValue.Parse(input);

        private readonly SystemProductHeaderValue value;

        internal ProductHeaderValue(SystemProductHeaderValue value)
        {
            this.value = value;
        }

        public SystemProductHeaderValue GetValue() => value;

        public override string ToString() => value.ToString();

        public static implicit operator string(ProductHeaderValue header) => header.ToString();
        public static implicit operator ProductHeaderValue(string input) => new ProductHeaderValue(Parse(input));
        public static implicit operator SystemProductHeaderValue(ProductHeaderValue header) => header.value;
        public static implicit operator ProductHeaderValue(SystemProductHeaderValue header) => new ProductHeaderValue(header);
    }
}