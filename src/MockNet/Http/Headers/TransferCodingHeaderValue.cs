using SystemTransferCodingHeaderValue = System.Net.Http.Headers.TransferCodingHeaderValue;

namespace Theorem.MockNet.Http
{
    public class TransferCodingHeaderValue : IHeaderValue<SystemTransferCodingHeaderValue>
    {
        public static TransferCodingHeaderValue Parse(string input) => SystemTransferCodingHeaderValue.Parse(input);

        private readonly SystemTransferCodingHeaderValue value;

        internal TransferCodingHeaderValue(SystemTransferCodingHeaderValue value)
        {
            this.value = value;
        }

        public SystemTransferCodingHeaderValue GetValue() => value;

        public override string ToString() => value.ToString();

        public static implicit operator string(TransferCodingHeaderValue header) => header.ToString();
        public static implicit operator TransferCodingHeaderValue(string input) => new TransferCodingHeaderValue(Parse(input));
        public static implicit operator SystemTransferCodingHeaderValue(TransferCodingHeaderValue header) => header.GetValue();
        public static implicit operator TransferCodingHeaderValue(SystemTransferCodingHeaderValue header) => new TransferCodingHeaderValue(header);
    }
}