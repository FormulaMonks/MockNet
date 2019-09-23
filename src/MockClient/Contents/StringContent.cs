using System;
using System.Text;
using SystemHttpContent = System.Net.Http.HttpContent;
using SystemStringContent = System.Net.Http.StringContent;

namespace MockClient
{
    public class StringContent : IHttpContent
    {
        private readonly string value;
        private readonly Encoding encoding;
        private readonly string mediaType;


        public StringContent(string content)
        {
            this.value = content;
        }

        public StringContent(string content, Encoding encoding)
        {
            this.value = content;
            this.encoding = encoding;
        }

        public StringContent(string content, Encoding encoding, string mediaType)
        {
            this.value = content;
            this.encoding = encoding;
            this.mediaType = mediaType;
        }

        public SystemHttpContent ToHttpContent() => new SystemStringContent(value, encoding, mediaType);

        #region Overrides
        public override string ToString()
        {
            return value;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj is String s)
            {
                return this == s;
            }

            {
                if (obj is StringContent content)
                {
                    return this == content.value;
                }
            }

            {
                if (obj is SystemStringContent content)
                {
                    var value = content.ReadAsStringAsync().GetAwaiter().GetResult();

                    return this == value;
                }
            }

            return false;
        }
        #endregion

        public static implicit operator string(StringContent content) => content.value;
        public static implicit operator StringContent(string content) => new StringContent(content);
        public static implicit operator SystemStringContent(StringContent content) => content.ToHttpContent() as SystemStringContent;

        public static bool operator ==(StringContent content, string value)
        {
            return content?.value == value;
        }

        public static bool operator !=(StringContent content, string value) => !(content == value);

        public static bool operator ==(string value, StringContent content) => (content == value);

        public static bool operator !=(string value, StringContent content)  => !(content == value);
    }
}