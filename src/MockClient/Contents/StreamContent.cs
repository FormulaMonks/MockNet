using System;
using System.IO;
using SystemHttpContent = System.Net.Http.HttpContent;
using SystemStreamContent = System.Net.Http.StreamContent;

namespace MockClient
{
    public class StreamContent : IHttpContent, IDisposable
    {
        private readonly Stream content;
        private readonly int bufferSize;

        public StreamContent(Stream content)
        {
            this.content = content;
        }

        public StreamContent(Stream content, int bufferSize)
        {
            this.content = content;
            this.bufferSize = bufferSize;
        }

        public SystemHttpContent ToHttpContent() => new SystemStreamContent(content, bufferSize);

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
            if (obj is null)
            {
                return false;
            }

            if (obj is Stream s)
            {
                return this == s;
            }

            {
                if (obj is StreamContent content)
                {
                    return this == content.content;
                }
            }

            {
                if (obj is SystemStreamContent content)
                {
                    var stream = content.ReadAsStreamAsync().GetAwaiter().GetResult();

                    return this == stream;
                }
            }

            return false;
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            content?.Dispose();
        }
        #endregion

        public static implicit operator Stream(StreamContent content) => content.content;
        public static implicit operator StreamContent(Stream content) => new StreamContent(content);
        public static implicit operator SystemStreamContent(StreamContent content) => content.ToHttpContent() as SystemStreamContent;

        public static bool operator ==(StreamContent header, Stream value)
        {
            // return StreamCompare.CompareAsync(header.content, value).GetAwaiter().GetResult();
            return false;
        }

        public static bool operator !=(StreamContent header, Stream value) => !(header == value);

        public static bool operator ==(Stream value, StreamContent header) => (header == value);

        public static bool operator !=(Stream value, StreamContent header)  => !(header == value);
    }
}