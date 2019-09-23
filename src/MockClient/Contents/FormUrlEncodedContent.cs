using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemHttpContent = System.Net.Http.HttpContent;
using SystemFormUrlEncodedContent = System.Net.Http.FormUrlEncodedContent;
using System;

namespace MockClient
{
    public class FormUrlEncodedContent : IHttpContent, IDictionary<string, string>
    {
        private readonly IDictionary<string, string> content;

        public FormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            this.content = nameValueCollection.ToDictionary(x => x.Key, x => x.Value);
        }

        public SystemHttpContent ToHttpContent() => new SystemFormUrlEncodedContent(content);

        #region Overrides
        public override string ToString()
        {
            return ToHttpContent().ToString();
        }

        public override int GetHashCode()
        {
            return ToHttpContent().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj is IEnumerable<KeyValuePair<string, string>> e)
            {
                return equals(this, e);
            }

            {
                if (obj is FormUrlEncodedContent content)
                {
                    return equals(this, content.content);
                }
            }

            {
                if (obj is SystemFormUrlEncodedContent content)
                {
                    //var collection = Utils.Json.ToDictionary(content.ReadAsStringAsync().GetAwaiter().GetResult()) as Dictionary<string, string>;

                    //return equals(this, collection);
                }
            }

            return false;

            bool equals(FormUrlEncodedContent content, IEnumerable<KeyValuePair<string, string>> collection)
            {
                if (collection.Count() != content.Count)
                {
                    return false;
                }

                foreach (var kvp in collection)
                {
                    if (kvp.Value != this[kvp.Key])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
        #endregion

        #region IDictionary<string, string>
        public string this[string key] { get => content[key]; set => content[key] = value; }

        public ICollection<string> Keys => content.Keys;

        public ICollection<string> Values => content.Values;

        public int Count => content.Count;

        public bool IsReadOnly => content.IsReadOnly;

        public void Add(string key, string value) => content.Add(key, value);

        public void Add(KeyValuePair<string, string> item) => content.Add(item.Key, item.Value);

        public void Clear() => content.Clear();

        public bool Contains(KeyValuePair<string, string> item) => content.Contains(item);

        public bool ContainsKey(string key) => content.ContainsKey(key);

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex) => content.CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => content.GetEnumerator();

        public bool Remove(string key) => content.Remove(key);

        public bool Remove(KeyValuePair<string, string> item) => content.Remove(item);

        public bool TryGetValue(string key, out string value) => content.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => content.GetEnumerator();
        #endregion

        public static implicit operator Dictionary<string, string>(FormUrlEncodedContent content) => content.content as Dictionary<string, string>;
        public static implicit operator FormUrlEncodedContent(Dictionary<string, string> content) => new FormUrlEncodedContent(content);
        public static implicit operator SystemFormUrlEncodedContent(FormUrlEncodedContent content) => content.ToHttpContent() as SystemFormUrlEncodedContent;


        public static bool operator ==(FormUrlEncodedContent content, IEnumerable<KeyValuePair<string, string>> collection)
        {
            return content?.Equals(collection) ?? false;
        }

        public static bool operator !=(FormUrlEncodedContent content, IEnumerable<KeyValuePair<string, string>> collection) => !(content == collection);

        public static bool operator ==(IEnumerable<KeyValuePair<string, string>> collection, FormUrlEncodedContent content) => (content == collection);

        public static bool operator !=(IEnumerable<KeyValuePair<string, string>> collection, FormUrlEncodedContent content)  => !(content == collection);
    }
}