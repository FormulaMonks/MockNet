using System;
using System.Text;
using SystemHttpContent = System.Net.Http.HttpContent;
using SystemStringContent = System.Net.Http.StringContent;

namespace Theorem.MockNet.Http
{
    public class ObjectContent : HttpContent
    {
        internal readonly object value;

        private readonly Type type;
        private readonly Encoding encoding;
        private readonly string mediaType;

        public ObjectContent(Type type, object content)
        {
            this.type = type;
            this.value = content;
            this.encoding = Encoding.UTF8;
            this.mediaType = "application/json";
        }

        protected override SystemHttpContent ToSystemHttpContent() => new SystemStringContent(Utils.Json.ToString(value), encoding, mediaType);

        #region Overrides
        public override string ToString()
        {
            return value.ToString();
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var content = obj switch
            {
                ObjectContent c => c.value,
                SystemStringContent c => Utils.Json.ToObject(c.ReadAsStringAsync().GetAwaiter().GetResult(), type),
                _ => obj,
            };

            return this.value.Equals(content);
        }
        #endregion

        public static bool operator ==(ObjectContent content, object value)
        {
            return content.value.Equals(value);
        }

        public static bool operator !=(ObjectContent content, object value) => !(content == value);

        public static bool operator ==(object value, ObjectContent content) => (content == value);

        public static bool operator !=(object value, ObjectContent content) => !(content == value);

        public static bool operator ==(ObjectContent s1, ObjectContent s2) => (s1 == s2.value);

        public static bool operator !=(ObjectContent s1, ObjectContent s2) => !(s1 == s2.value);
    }
}
