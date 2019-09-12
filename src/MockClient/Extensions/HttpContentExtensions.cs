using System;
using System.Threading.Tasks;
using SystemHttpContent = System.Net.Http.HttpContent;

namespace MockClient
{
    internal static class HttpContentExtensions
    {
        public static async Task<object> ToDictionaryAsync(this SystemHttpContent source)
        {
            return Utils.Json.ToDictionary(await source.ReadAsStringAsync());
        }
        public static async Task<object> ToFormUrlEncodedContentAsync(this SystemHttpContent source)
        {
            return new FormUrlEncodedContent(Utils.Json.ToDictionary(await source.ReadAsStringAsync()));
        }

        public static async Task<object> ToObjectAsync(this SystemHttpContent source, Type type)
        {
            return Utils.Json.ToObject(await source.ReadAsStringAsync(), type);
        }

        public static async Task<object> ToStringAsync(this SystemHttpContent source)
        {
            return await source.ReadAsStringAsync();
        }

        public static async Task<object> ToStringContentAsync(this SystemHttpContent source)
        {
            return new StringContent(await source.ReadAsStringAsync());
        }

        public static async Task<object> ToStreamAsync(this SystemHttpContent source)
        {
            return await source.ReadAsStreamAsync();
        }

        public static async Task<object> ToStreamContentAsync(this SystemHttpContent source)
        {
            return new StreamContent(await source.ReadAsStreamAsync());
        }

        public static async Task<object> ToByteArrayAsync(this SystemHttpContent source)
        {
            return await source.ReadAsByteArrayAsync();
        }

        public static async Task<object> ToByteArrayContentAsync(this SystemHttpContent source)
        {
            return new ByteArrayContent(await source.ReadAsByteArrayAsync());
        }
    }
}