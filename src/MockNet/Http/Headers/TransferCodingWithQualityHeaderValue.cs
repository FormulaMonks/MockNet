using SystemTransferCodingWithQualityHeaderValue = System.Net.Http.Headers.TransferCodingWithQualityHeaderValue;

namespace Theorem.MockNet.Http
{
    public class TransferCodingWithQualityHeaderValue : IHeaderValue<SystemTransferCodingWithQualityHeaderValue>
    {
        public static TransferCodingWithQualityHeaderValue Parse(string input) => SystemTransferCodingWithQualityHeaderValue.Parse(input);

        private readonly SystemTransferCodingWithQualityHeaderValue value;

        internal TransferCodingWithQualityHeaderValue(SystemTransferCodingWithQualityHeaderValue value)
        {
            this.value = value;
        }

        public SystemTransferCodingWithQualityHeaderValue GetValue() => value;

        public override string ToString() => value.ToString();

        public static implicit operator string(TransferCodingWithQualityHeaderValue header) => header.ToString();
        public static implicit operator TransferCodingWithQualityHeaderValue(string input) => new TransferCodingWithQualityHeaderValue(Parse(input));
        public static implicit operator SystemTransferCodingWithQualityHeaderValue(TransferCodingWithQualityHeaderValue header) => header.GetValue();
        public static implicit operator TransferCodingWithQualityHeaderValue(SystemTransferCodingWithQualityHeaderValue header) => new TransferCodingWithQualityHeaderValue(header);
    }

}