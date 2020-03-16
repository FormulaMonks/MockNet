using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Theorem.MockNet.Http
{
    public sealed class HttpHeaderValueCollection<T> : ICollection<T>
        where T : class
    {
        private readonly System.Net.Http.Headers.HttpHeaderValueCollection<T> store;

        internal HttpHeaderValueCollection(System.Net.Http.Headers.HttpHeaderValueCollection<T> store)
        {
            this.store = store;
        }

        public int Count => store.Count;

        public bool IsReadOnly => store.IsReadOnly;

        public void Add(T item) => store.Add(item);

        public void Clear() => store.Clear();

        public bool Contains(T item) => store.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => throw new NotImplementedException();

        public IEnumerator<T> GetEnumerator() => store.GetEnumerator();

        public bool Remove(T item) => store.Remove(item);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => store.ToString();

        public static implicit operator string(HttpHeaderValueCollection<T> header) => header.ToString();
        public static implicit operator System.Net.Http.Headers.HttpHeaderValueCollection<T>(HttpHeaderValueCollection<T> header) => header.store;
        public static implicit operator HttpHeaderValueCollection<T>(System.Net.Http.Headers.HttpHeaderValueCollection<T> collection) => new HttpHeaderValueCollection<T>(collection);
    }

    public sealed class HttpHeaderValueCollection<T, S> : ICollection<T>
        where T : IHeaderValue<S>
        where S : class
    {
        private readonly System.Net.Http.Headers.HttpHeaderValueCollection<S> store;
        private readonly Func<S, T> convertor;

        internal HttpHeaderValueCollection(System.Net.Http.Headers.HttpHeaderValueCollection<S> store, Func<S, T> convertor)
        {
            this.store = store;
            this.convertor = convertor;
        }

        public int Count => store.Count;

        public bool IsReadOnly => store.IsReadOnly;

        public void Add(T item) => store.Add(item.GetValue());

        public void Clear() => store.Clear();

        public bool Contains(T item) => store.Contains(item.GetValue());

        public void CopyTo(T[] array, int arrayIndex) => throw new NotImplementedException();

        public IEnumerator<T> GetEnumerator() => store.Select(convertor).GetEnumerator();

        public bool Remove(T item) => store.Remove(item.GetValue());

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => store.ToString();

        public static implicit operator string(HttpHeaderValueCollection<T, S> header) => header.ToString();
        public static implicit operator System.Net.Http.Headers.HttpHeaderValueCollection<S>(HttpHeaderValueCollection<T, S> header) => header.store;
        public static implicit operator HttpHeaderValueCollection<T, S>(System.Net.Http.Headers.HttpHeaderValueCollection<S> collection) => new HttpHeaderValueCollection<T, S>(collection, x => default);
    }
}