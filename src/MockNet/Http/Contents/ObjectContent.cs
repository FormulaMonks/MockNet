using System;
using System.Text;
using SystemHttpContent = System.Net.Http.HttpContent;
using SystemStringContent = System.Net.Http.StringContent;

namespace Theorem.MockNet.Http
{
    public class ObjectContent : IHttpContent
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

        public SystemHttpContent ToHttpContent() => new SystemStringContent(Utils.Json.ToString(value), encoding, mediaType);

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
            if (obj is null)
            {
                return false;
            }

            if (obj.GetType() == type)
            {
                return this.value.Equals(obj);
            }

            {
                if (obj is ObjectContent content)
                {
                    return this.value.Equals(content.value);
                }
            }

            {
                if (obj is SystemStringContent content)
                {
                    var json = content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var value = Utils.Json.ToObject(json, type);

                    return this.value.Equals(value);
                }
            }

            return false;
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