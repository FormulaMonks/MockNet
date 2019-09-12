using System;
using System.Linq;
using SystemHttpRequestHeaders = System.Net.Http.Headers.HttpRequestHeaders;

namespace MockClient
{
    public sealed class HttpRequestHeaders
    {
        private readonly SystemHttpRequestHeaders headers;

        internal HttpRequestHeaders(SystemHttpRequestHeaders requestHeaders)
        {
            this.headers = requestHeaders ?? throw new ArgumentNullException(nameof(requestHeaders));
        }

        private HttpRequestHeader<string> GetHeader<T>(System.Net.Http.Headers.HttpHeaderValueCollection<T> header) where T : class
        {
            return new HttpRequestHeader<string>(header.Select(x => x.ToString()), header.ToString());
        }

        public HttpRequestHeader<string> this[string name]
        {
            get
            {
                var values = headers.GetValues(name);
                return new HttpRequestHeader<string>(values, string.Join(", ", values));
            }
        }

        public HttpRequestHeader<string> Accept => GetHeader(headers.Accept);
        public HttpRequestHeader<string> UserAgent => GetHeader(headers.UserAgent);
        public HttpRequestHeader<string> Upgrade => GetHeader(headers.Upgrade);
        public bool? TransferEncodingChunked => this.headers.TransferEncodingChunked;
        public HttpRequestHeader<string> TransferEncoding => GetHeader(headers.TransferEncoding);
        public HttpRequestHeader<string> Trailer => GetHeader(headers.Trailer);
        public HttpRequestHeader<string> TE => GetHeader(headers.TE);
        public Uri Referrer => headers.Referrer;
        public string Range { get; set; }
        public string ProxyAuthorization { get; set; }
        public HttpRequestHeader<string> Pragma => GetHeader(headers.Pragma);
        public int? MaxForwards => headers.MaxForwards;
        public DateTimeOffset? IfUnmodifiedSince => headers.IfUnmodifiedSince;
        public string IfRange { get; set; }
        public HttpRequestHeader<string> Via => GetHeader(headers.Via);
        public HttpRequestHeader<string> IfNoneMatch => GetHeader(headers.IfNoneMatch);
        public HttpRequestHeader<string> IfMatch => GetHeader(headers.IfMatch);
        public string Host => headers.Host;
        public string From => headers.From;
        public bool? ExpectContinue => headers.ExpectContinue;
        public HttpRequestHeader<string> Expect => GetHeader(headers.Expect);
        public DateTimeOffset? Date => headers.Date;
        public bool? ConnectionClose => headers.ConnectionClose;
        public HttpRequestHeader<string> Connection => GetHeader(headers.Connection);
        public string CacheControl { get; set; }
        public string Authorization { get; set; }
        public HttpRequestHeader<string> AcceptLanguage => GetHeader(headers.AcceptLanguage);
        public HttpRequestHeader<string> AcceptEncoding => GetHeader(headers.AcceptEncoding);
        public HttpRequestHeader<string> AcceptCharset => GetHeader(headers.AcceptCharset);
        public DateTimeOffset? IfModifiedSince => headers.IfModifiedSince;
        public HttpRequestHeader<string> Warning => GetHeader(headers.Warning);
    }
}