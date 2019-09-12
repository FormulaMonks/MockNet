using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MockClient
{
    public sealed class HttpRequestHeader<T> : IEnumerable<T>
    {
        private readonly T[] values;
        private readonly T value;

        public HttpRequestHeader(T[] values, T value)
        {
            this.values = values;
            this.value = value;
        }

        public HttpRequestHeader(IEnumerable<T> values, T value) : this(values.ToArray(), value) { }

        public HttpRequestHeader(T value) : this(new T[] { value }, value) { }

        public override int GetHashCode()
        {
            return values.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var header = obj as HttpRequestHeader<T>;

            if (header is null)
            {
                return false;
            }

            return values.Equals(header.values); // TODO: validate the values inside of the enumerable match.
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var value in values)
            {
                yield return value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static bool operator ==(HttpRequestHeader<T> header, T value)
        {
            return header.Contains(value);
        }

        public static bool operator ==(T value, HttpRequestHeader<T> header) => (header == value);

        public static bool operator !=(HttpRequestHeader<T> header, T value) => !(header == value);

        public static bool operator !=(T value, HttpRequestHeader<T> header) => !(header == value);

        public static bool operator ==(HttpRequestHeader<T> h1, HttpRequestHeader<T> h2)
        {
            return !h1.Except(h2).Any() && !h2.Except(h1).Any();
        }

        public static bool operator !=(HttpRequestHeader<T> h1, HttpRequestHeader<T> h2) => !(h1 == h2);
    }
}