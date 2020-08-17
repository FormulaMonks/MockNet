using System;
using SystemHttpContentHeaders = System.Net.Http.Headers.HttpContentHeaders;

namespace Theorem.MockNet.Http
{
    public class HttpContentHeaders : HttpHeaders
    {
        private readonly SystemHttpContentHeaders contentHeadersStore;

        internal HttpContentHeaders(SystemHttpContentHeaders contentHeadersStore)
        {
            this.contentHeadersStore = contentHeadersStore;
            store = contentHeadersStore;
        }

        internal HttpContentHeaders() : this(new System.Net.Http.ByteArrayContent(new byte[] { }).Headers)
        {
        }

        public HttpHeaderValueCollection<string> Allow =>
            new HttpHeaderValueCollection<string>(
                contentHeadersStore.Allow as System.Net.Http.Headers.HttpHeaderValueCollection<string>);

        public ContentDispositionHeaderValue ContentDisposition
        {
            get => contentHeadersStore.ContentDisposition;
            set => contentHeadersStore.ContentDisposition = value;
        }

        public HttpHeaderValueCollection<string> ContentEncoding => new HttpHeaderValueCollection<string>(
            contentHeadersStore.ContentEncoding as System.Net.Http.Headers.HttpHeaderValueCollection<string>);

        public HttpHeaderValueCollection<string> ContentLanguage => new HttpHeaderValueCollection<string>(
            contentHeadersStore.ContentLanguage as System.Net.Http.Headers.HttpHeaderValueCollection<string>);

        public long? ContentLength
        {
            get => contentHeadersStore.ContentLength;
            set => contentHeadersStore.ContentLength = value;
        }

        public Uri ContentLocation
        {
            get => contentHeadersStore.ContentLocation;
            set => contentHeadersStore.ContentLocation = value;
        }

        public byte[] ContentMD5
        {
            get => contentHeadersStore.ContentMD5;
            set => contentHeadersStore.ContentMD5 = value;
        }

        public ContentRangeHeaderValue ContentRange
        {
            get => contentHeadersStore.ContentRange;
            set => contentHeadersStore.ContentRange = value;
        }

        public MediaTypeHeaderValue ContentType
        {
            get => contentHeadersStore.ContentType;
            set => contentHeadersStore.ContentType = value;
        }

        public DateTimeOffset? Expires
        {
            get => contentHeadersStore.Expires;
            set => contentHeadersStore.Expires = value;
        }

        public DateTimeOffset? LastModified
        {
            get => contentHeadersStore.LastModified;
            set => contentHeadersStore.LastModified = value;
        }
    }
}