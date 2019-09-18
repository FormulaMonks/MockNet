using System;
using System.Collections.Generic;
using SystemHttpResponseMessage = System.Net.Http.HttpResponseMessage;
using SystemHttpResponseHeaders = System.Net.Http.Headers.HttpResponseHeaders;
using SystemAuthenticationHeaderValue = System.Net.Http.Headers.AuthenticationHeaderValue;
using SystemViaHeaderValue = System.Net.Http.Headers.ViaHeaderValue;
using SystemWarningHeaderValue = System.Net.Http.Headers.WarningHeaderValue;
using SystemProductHeaderValue = System.Net.Http.Headers.ProductHeaderValue;
using SystemTransferCodingHeaderValue = System.Net.Http.Headers.TransferCodingHeaderValue;
using SystemProductInfoHeaderValue = System.Net.Http.Headers.ProductInfoHeaderValue;
using SystemNameValueHeaderValue = System.Net.Http.Headers.NameValueHeaderValue;
using System.Collections;

namespace MockClient
{
    public sealed class HttpResponseHeaders : HttpHeaders
    {
        public HttpResponseHeaders()
        {
            this.store = new SystemHttpResponseMessage().Headers;
        }

        internal SystemHttpResponseHeaders Store => store as SystemHttpResponseHeaders;

        public HttpHeaderValueCollection<string> AcceptRanges => new HttpHeaderValueCollection<string>(Store.AcceptRanges);

        public HttpHeaderValueCollection<ViaHeaderValue, SystemViaHeaderValue> Via => new HttpHeaderValueCollection<ViaHeaderValue, SystemViaHeaderValue>(Store.Via, x => (ViaHeaderValue)x);

        public HttpHeaderValueCollection<string> Vary => new HttpHeaderValueCollection<string>(Store.Vary);
        public HttpHeaderValueCollection<ProductHeaderValue, SystemProductHeaderValue> Upgrade => new HttpHeaderValueCollection<ProductHeaderValue, SystemProductHeaderValue>(Store.Upgrade, x => (ProductHeaderValue)x);
        public bool? TransferEncodingChunked { get => Store.TransferEncodingChunked; set => Store.TransferEncodingChunked = value; }
        public HttpHeaderValueCollection<TransferCodingHeaderValue, SystemTransferCodingHeaderValue> TransferEncoding => new HttpHeaderValueCollection<TransferCodingHeaderValue, SystemTransferCodingHeaderValue>(Store.TransferEncoding, x => (TransferCodingHeaderValue)x);
        public HttpHeaderValueCollection<string> Trailer => new HttpHeaderValueCollection<string>(Store.Trailer);
        public HttpHeaderValueCollection<ProductInfoHeaderValue, SystemProductInfoHeaderValue> Server => new HttpHeaderValueCollection<ProductInfoHeaderValue, SystemProductInfoHeaderValue>(Store.Server, x => (ProductInfoHeaderValue)x);
        public RetryConditionHeaderValue RetryAfter { get => Store.RetryAfter; set => Store.RetryAfter = value; }
        public HttpHeaderValueCollection<AuthenticationHeaderValue, SystemAuthenticationHeaderValue> ProxyAuthenticate => new HttpHeaderValueCollection<AuthenticationHeaderValue, SystemAuthenticationHeaderValue>(Store.ProxyAuthenticate, x => (AuthenticationHeaderValue)x);
        public HttpHeaderValueCollection<NameValueHeaderValue, SystemNameValueHeaderValue> Pragma => new HttpHeaderValueCollection<NameValueHeaderValue, SystemNameValueHeaderValue>(Store.Pragma, x => (NameValueHeaderValue)x);
        public Uri Location { get => Store.Location; set => Store.Location = value; }
        public EntityTagHeaderValue ETag { get => Store.ETag; set => Store.ETag = value; }
        public DateTimeOffset? Date { get => Store.Date; set => Store.Date = value; }
        public bool? ConnectionClose { get => Store.ConnectionClose; set => Store.ConnectionClose = value; }
        public HttpHeaderValueCollection<string> Connection => new HttpHeaderValueCollection<string>(Store.Connection);
        public CacheControlHeaderValue CacheControl { get => Store.CacheControl; set => Store.CacheControl = value; }
        public TimeSpan? Age { get => Store.Age; set => Store.Age = value; }
        public HttpHeaderValueCollection<WarningHeaderValue, SystemWarningHeaderValue> Warning => new HttpHeaderValueCollection<WarningHeaderValue, SystemWarningHeaderValue>(Store.Warning, x => (WarningHeaderValue)x);
        public HttpHeaderValueCollection<AuthenticationHeaderValue, SystemAuthenticationHeaderValue> WwwAuthenticate => new HttpHeaderValueCollection<AuthenticationHeaderValue, SystemAuthenticationHeaderValue>(Store.WwwAuthenticate, x => (AuthenticationHeaderValue)x);
    }
}