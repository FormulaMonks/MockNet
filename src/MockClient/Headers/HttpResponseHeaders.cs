using System;
using System.Collections.Generic;
using SystemHttpResponseMessage = System.Net.Http.HttpResponseMessage;
using SystemAuthenticationHeaderValue = System.Net.Http.Headers.AuthenticationHeaderValue;
using SystemViaHeaderValue = System.Net.Http.Headers.ViaHeaderValue;
using SystemWarningHeaderValue = System.Net.Http.Headers.WarningHeaderValue;
using SystemProductHeaderValue = System.Net.Http.Headers.ProductHeaderValue;
using SystemTransferCodingHeaderValue = System.Net.Http.Headers.TransferCodingHeaderValue;
using SystemProductInfoHeaderValue = System.Net.Http.Headers.ProductInfoHeaderValue;
using SystemNameValueHeaderValue = System.Net.Http.Headers.NameValueHeaderValue;

namespace MockClient
{
    public sealed class HttpResponseHeaders
    {
        // TODO: implement other HttpHeader methods

        private readonly SystemHttpResponseMessage message;

        public HttpResponseHeaders()
        {
            message = new SystemHttpResponseMessage();
        }

        public string this[string name]
        {
            get => throw new NotImplementedException();
            set => Add(name, value);
        }

        public void Add(string name, string value)
        {
            message.Headers.Add(name, value);
        }

        public void Add(string name, IEnumerable<string> values)
        {
            message.Headers.Add(name, values);
        }

        public HttpHeaderValueCollection<string> AcceptRanges => new HttpHeaderValueCollection<string>(message.Headers.AcceptRanges);

        public HttpHeaderValueCollection<ViaHeaderValue, SystemViaHeaderValue> Via => new HttpHeaderValueCollection<ViaHeaderValue, SystemViaHeaderValue>(message.Headers.Via, x => (ViaHeaderValue)x);

        public HttpHeaderValueCollection<string> Vary => new HttpHeaderValueCollection<string>(message.Headers.Vary);
        public HttpHeaderValueCollection<ProductHeaderValue, SystemProductHeaderValue> Upgrade => new HttpHeaderValueCollection<ProductHeaderValue, SystemProductHeaderValue>(message.Headers.Upgrade, x => (ProductHeaderValue)x);
        public bool? TransferEncodingChunked { get => message.Headers.TransferEncodingChunked; set => message.Headers.TransferEncodingChunked = value; }
        public HttpHeaderValueCollection<TransferCodingHeaderValue, SystemTransferCodingHeaderValue> TransferEncoding => new HttpHeaderValueCollection<TransferCodingHeaderValue, SystemTransferCodingHeaderValue>(message.Headers.TransferEncoding, x => (TransferCodingHeaderValue)x);
        public HttpHeaderValueCollection<string> Trailer => new HttpHeaderValueCollection<string>(message.Headers.Trailer);
        public HttpHeaderValueCollection<ProductInfoHeaderValue, SystemProductInfoHeaderValue> Server => new HttpHeaderValueCollection<ProductInfoHeaderValue, SystemProductInfoHeaderValue>(message.Headers.Server, x => (ProductInfoHeaderValue)x);
        public RetryConditionHeaderValue RetryAfter { get => message.Headers.RetryAfter; set => message.Headers.RetryAfter = value; }
        public HttpHeaderValueCollection<AuthenticationHeaderValue, SystemAuthenticationHeaderValue> ProxyAuthenticate => new HttpHeaderValueCollection<AuthenticationHeaderValue, SystemAuthenticationHeaderValue>(message.Headers.ProxyAuthenticate, x => (AuthenticationHeaderValue)x);
        public HttpHeaderValueCollection<NameValueHeaderValue, SystemNameValueHeaderValue> Pragma => new HttpHeaderValueCollection<NameValueHeaderValue, SystemNameValueHeaderValue>(message.Headers.Pragma, x => (NameValueHeaderValue)x);
        public Uri Location { get => message.Headers.Location; set => message.Headers.Location = value; }
        public EntityTagHeaderValue ETag { get => message.Headers.ETag; set => message.Headers.ETag = value; }
        public DateTimeOffset? Date { get => message.Headers.Date; set => message.Headers.Date = value; }
        public bool? ConnectionClose { get => message.Headers.ConnectionClose; set => message.Headers.ConnectionClose = value; }
        public HttpHeaderValueCollection<string> Connection => new HttpHeaderValueCollection<string>(message.Headers.Connection);
        public CacheControlHeaderValue CacheControl { get => message.Headers.CacheControl; set => message.Headers.CacheControl = value; }
        public TimeSpan? Age { get => message.Headers.Age; set => message.Headers.Age = value; }
        public HttpHeaderValueCollection<WarningHeaderValue, SystemWarningHeaderValue> Warning => new HttpHeaderValueCollection<WarningHeaderValue, SystemWarningHeaderValue>(message.Headers.Warning, x => (WarningHeaderValue)x);
        public HttpHeaderValueCollection<AuthenticationHeaderValue, SystemAuthenticationHeaderValue> WwwAuthenticate => new HttpHeaderValueCollection<AuthenticationHeaderValue, SystemAuthenticationHeaderValue>(message.Headers.WwwAuthenticate, x => (AuthenticationHeaderValue)x);

        public IEnumerator<KeyValuePair<string, IEnumerable<string>>> GetEnumerator() => message.Headers.GetEnumerator();
    }
}