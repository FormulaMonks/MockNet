using System;
using System.Collections.Generic;
using System.Linq;
using SystemHttpRequestHeaders = System.Net.Http.Headers.HttpRequestHeaders;
using SystemMediaTypeWithQualityHeaderValue = System.Net.Http.Headers.MediaTypeWithQualityHeaderValue;
using SystemTransferCodingWithQualityHeaderValue = System.Net.Http.Headers.TransferCodingWithQualityHeaderValue;
using SystemNameValueWithParametersHeaderValue = System.Net.Http.Headers.NameValueWithParametersHeaderValue;
using SystemStringWithQualityHeaderValue = System.Net.Http.Headers.StringWithQualityHeaderValue;
using SystemProductInfoHeaderValue = System.Net.Http.Headers.ProductInfoHeaderValue;
using SystemProductHeaderValue = System.Net.Http.Headers.ProductHeaderValue;
using SystemTransferCodingHeaderValue = System.Net.Http.Headers.TransferCodingHeaderValue;
using SystemNameValueHeaderValue = System.Net.Http.Headers.NameValueHeaderValue;
using SystemViaHeaderValue = System.Net.Http.Headers.ViaHeaderValue;
using SystemEntityTagHeaderValue = System.Net.Http.Headers.EntityTagHeaderValue;
using SystemWarningHeaderValue = System.Net.Http.Headers.WarningHeaderValue;

namespace MockClient
{
    public sealed class HttpRequestHeaders
    {
        private readonly SystemHttpRequestHeaders store;

        internal HttpRequestHeaders(SystemHttpRequestHeaders store)
        {
            this.store = store ?? throw new ArgumentNullException(nameof(store));
        }

        public string this[string name]
        {
            get => throw new NotImplementedException();
            set => Add(name, value);
        }

        public void Add(string name, string value)
        {
            store.Add(name, value);
        }

        public void Add(string name, IEnumerable<string> values)
        {
            store.Add(name, values);
        }

        public HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue, SystemMediaTypeWithQualityHeaderValue> Accept => new HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue, SystemMediaTypeWithQualityHeaderValue>(store.Accept, x => (MediaTypeWithQualityHeaderValue)x);
        public HttpHeaderValueCollection<ProductInfoHeaderValue, SystemProductInfoHeaderValue> UserAgent => new HttpHeaderValueCollection<ProductInfoHeaderValue, SystemProductInfoHeaderValue>(store.UserAgent, x => (ProductInfoHeaderValue)x);
        public HttpHeaderValueCollection<ProductHeaderValue, SystemProductHeaderValue> Upgrade => new HttpHeaderValueCollection<ProductHeaderValue, SystemProductHeaderValue>(store.Upgrade, x => (ProductHeaderValue)x);
        public bool? TransferEncodingChunked { get => store.TransferEncodingChunked; set => store.TransferEncodingChunked = value; }
        public HttpHeaderValueCollection<TransferCodingHeaderValue, SystemTransferCodingHeaderValue> TransferEncoding => new HttpHeaderValueCollection<TransferCodingHeaderValue, SystemTransferCodingHeaderValue>(store.TransferEncoding, x => (TransferCodingHeaderValue)x);
        public HttpHeaderValueCollection<string> Trailer => new HttpHeaderValueCollection<string>(store.Trailer);
        public HttpHeaderValueCollection<TransferCodingWithQualityHeaderValue, SystemTransferCodingWithQualityHeaderValue> TE => new HttpHeaderValueCollection<TransferCodingWithQualityHeaderValue, SystemTransferCodingWithQualityHeaderValue>(store.TE, x => (TransferCodingWithQualityHeaderValue)x);
        public Uri Referrer { get => store.Referrer; set => store.Referrer = value; }
        public RangeHeaderValue Range { get => store.Range; set => store.Range = value; }
        public AuthenticationHeaderValue ProxyAuthorization { get => store.ProxyAuthorization; set => store.ProxyAuthorization = value; }
        public HttpHeaderValueCollection<NameValueHeaderValue, SystemNameValueHeaderValue> Pragma => new HttpHeaderValueCollection<NameValueHeaderValue, SystemNameValueHeaderValue>(store.Pragma, x => (NameValueHeaderValue)x);
        public int? MaxForwards { get => store.MaxForwards; set => store.MaxForwards = value; }
        public DateTimeOffset? IfUnmodifiedSince { get => store.IfUnmodifiedSince; set => store.IfUnmodifiedSince = value; }
        public RangeConditionHeaderValue IfRange { get => store.IfRange; set => store.IfRange = value; }
        public HttpHeaderValueCollection<ViaHeaderValue, SystemViaHeaderValue> Via => new HttpHeaderValueCollection<ViaHeaderValue, SystemViaHeaderValue>(store.Via, x => (ViaHeaderValue)x);
        public HttpHeaderValueCollection<EntityTagHeaderValue, SystemEntityTagHeaderValue> IfNoneMatch => new HttpHeaderValueCollection<EntityTagHeaderValue, SystemEntityTagHeaderValue>(store.IfNoneMatch, x => (EntityTagHeaderValue)x);
        public HttpHeaderValueCollection<EntityTagHeaderValue, SystemEntityTagHeaderValue> IfMatch => new HttpHeaderValueCollection<EntityTagHeaderValue, SystemEntityTagHeaderValue>(store.IfMatch, x => (EntityTagHeaderValue)x);
        public string Host { get => store.Host; set => store.Host = value; }
        public string From { get => store.From; set => store.From = value; }
        public bool? ExpectContinue { get => store.ExpectContinue; set => store.ExpectContinue = value; }
        public HttpHeaderValueCollection<NameValueWithParametersHeaderValue, SystemNameValueWithParametersHeaderValue> Expect => new HttpHeaderValueCollection<NameValueWithParametersHeaderValue, SystemNameValueWithParametersHeaderValue>(store.Expect, x => (NameValueWithParametersHeaderValue)x);
        public DateTimeOffset? Date { get => store.Date; set => store.Date = value; }
        public bool? ConnectionClose { get => store.ConnectionClose; set => store.ConnectionClose = value; }
        public HttpHeaderValueCollection<string> Connection => new HttpHeaderValueCollection<string>(store.Connection);
        public CacheControlHeaderValue CacheControl { get => store.CacheControl; set => store.CacheControl = value; }
        public AuthenticationHeaderValue Authorization { get => store.Authorization; set => store.Authorization = value; }
        public HttpHeaderValueCollection<StringWithQualityHeaderValue, SystemStringWithQualityHeaderValue> AcceptLanguage => new HttpHeaderValueCollection<StringWithQualityHeaderValue, SystemStringWithQualityHeaderValue>(store.AcceptLanguage, x => (StringWithQualityHeaderValue)x);
        public HttpHeaderValueCollection<StringWithQualityHeaderValue, SystemStringWithQualityHeaderValue> AcceptEncoding => new HttpHeaderValueCollection<StringWithQualityHeaderValue, SystemStringWithQualityHeaderValue>(store.AcceptEncoding, x => (StringWithQualityHeaderValue)x);
        public HttpHeaderValueCollection<StringWithQualityHeaderValue, SystemStringWithQualityHeaderValue> AcceptCharset => new HttpHeaderValueCollection<StringWithQualityHeaderValue, SystemStringWithQualityHeaderValue>(store.AcceptCharset, x => (StringWithQualityHeaderValue)x);
        public DateTimeOffset? IfModifiedSince { get; set; }
        public HttpHeaderValueCollection<WarningHeaderValue, SystemWarningHeaderValue> Warning => new HttpHeaderValueCollection<WarningHeaderValue, SystemWarningHeaderValue>(store.Warning, x => (WarningHeaderValue)x);

        public IEnumerator<KeyValuePair<string, IEnumerable<string>>> GetEnumerator() => store.GetEnumerator();
    }
}