using SystemProductInfoHeaderValue = System.Net.Http.Headers.ProductInfoHeaderValue;

namespace MockClient
{
    public class ProductInfoHeaderValue : IHeaderValue<SystemProductInfoHeaderValue>
    {
        public static ProductInfoHeaderValue Parse(string input) => SystemProductInfoHeaderValue.Parse(input);

        private readonly SystemProductInfoHeaderValue value;

        internal ProductInfoHeaderValue(SystemProductInfoHeaderValue value)
        {
            this.value = value;
        }

        public SystemProductInfoHeaderValue GetValue() => value;

        public override string ToString() => value.ToString();

        public static implicit operator string(ProductInfoHeaderValue header) => header.ToString();
        public static implicit operator ProductInfoHeaderValue(string input) => new ProductInfoHeaderValue(Parse(input));
        public static implicit operator SystemProductInfoHeaderValue(ProductInfoHeaderValue header) => header.GetValue();
        public static implicit operator ProductInfoHeaderValue(SystemProductInfoHeaderValue header) => new ProductInfoHeaderValue(header);
    }
}