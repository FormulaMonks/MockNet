using System;
using System.Collections;
using System.Collections.Generic;
using SystemHttpHeaders = System.Net.Http.Headers.HttpHeaders;

namespace Theorem.MockNet.Http
{
    public class HttpHeaders : IEnumerable<KeyValuePair<string, IEnumerable<string>>>, IEnumerable
    {
        protected SystemHttpHeaders store;

        internal HttpHeaders()
        {
        }

        public string this[string name]
        {
            get => string.Join(", ", GetValues(name));
            set => Add(name, value);
        }

        public void Add(string name, string value) => store.Add(name, value);

        public void Add(string name, IEnumerable<string> values) => store.Add(name, values);

        public void Clear() => store.Clear();

        public bool Contains(string name) => store.Contains(name);

        public IEnumerable<string> GetValues(string name) => store.GetValues(name);

        public bool Remove(string name) => store.Remove(name);

        public override string ToString() => store.ToString();

        public IEnumerator<KeyValuePair<string, IEnumerable<string>>> GetEnumerator() => store.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}