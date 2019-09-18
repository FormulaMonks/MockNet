using SystemAuthenticationHeaderValue = System.Net.Http.Headers.AuthenticationHeaderValue;

namespace MockClient
{
    public class AuthenticationHeaderValue : IHeaderValue<SystemAuthenticationHeaderValue>
    {
        public static AuthenticationHeaderValue Parse(string input) => SystemAuthenticationHeaderValue.Parse(input);

        private readonly SystemAuthenticationHeaderValue value;

        internal AuthenticationHeaderValue(SystemAuthenticationHeaderValue value)
        {
            this.value = value;
        }

        public SystemAuthenticationHeaderValue GetValue() => value;

        public override string ToString() => value.ToString();

        public static implicit operator string(AuthenticationHeaderValue header) => header.ToString();
        public static implicit operator AuthenticationHeaderValue(string input) => new AuthenticationHeaderValue(Parse(input));
        public static implicit operator SystemAuthenticationHeaderValue(AuthenticationHeaderValue header) => header.GetValue();
        public static implicit operator AuthenticationHeaderValue(SystemAuthenticationHeaderValue header) => new AuthenticationHeaderValue(header);
    }
}