using System;
using System.Text;
using SystemHttpContent = System.Net.Http.HttpContent;

namespace Theorem.MockNet.Http
{
    public class ObjectContent<T> : IHttpContent
    {
        private readonly ObjectContent content;

        public ObjectContent(T content)
        {
            this.content = new ObjectContent(typeof(T), content);
        }

        public SystemHttpContent ToHttpContent() => content.ToHttpContent();

        #region Overrides
        public override string ToString()
        {
            return content.ToString();
        }

        public override int GetHashCode()
        {
            return content.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return content.Equals(obj);
        }
        #endregion

        public static implicit operator T(ObjectContent<T> content) => (T)Convert.ChangeType(content.content.value, typeof(T));
        public static implicit operator ObjectContent<T>(T content) => new ObjectContent<T>(content);

        public static bool operator ==(ObjectContent<T> content, T value)
        {
            return content.content.Equals(value);
        }

        public static bool operator !=(ObjectContent<T> content, T value) => !(content == value);

        public static bool operator ==(T value, ObjectContent<T> content) => (content == value);

        public static bool operator !=(T value, ObjectContent<T> content) => !(content == value);

        public static bool operator ==(ObjectContent<T> s1, ObjectContent<T> s2) => (s1.content == s2.content);

        public static bool operator !=(ObjectContent<T> s1, ObjectContent<T> s2) => !(s1.content == s2.content);
    }
}