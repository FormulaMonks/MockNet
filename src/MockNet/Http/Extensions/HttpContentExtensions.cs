using System;
using System.Threading.Tasks;
using SystemHttpContent = System.Net.Http.HttpContent;

namespace MockNet.Http
{
    internal static class HttpContentExtensions
    {
        public static async Task<object> ToDictionaryAsync(this SystemHttpContent source)
        {
            if (source is null)
            {
                return null;
            }

            return Utils.Json.ToDictionary(await source.ReadAsStringAsync());
        }
        public static async Task<object> ToFormUrlEncodedContentAsync(this SystemHttpContent source)
        {
            if (source is null)
            {
                return null;
            }

            return new FormUrlEncodedContent(Utils.Json.ToDictionary(await source.ReadAsStringAsync()));
        }

        public static async Task<object> ToObjectAsync(this SystemHttpContent source, Type type)
        {
            if (source is null)
            {
                return null;
            }

            return Utils.Json.ToObject(await source.ReadAsStringAsync(), type);
        }

        public static async Task<object> ToStringAsync(this SystemHttpContent source)
        {
            if (source is null)
            {
                return null;
            }

            return await source.ReadAsStringAsync();
        }

        public static async Task<object> ToStringContentAsync(this SystemHttpContent source)
        {
            if (source is null)
            {
                return null;
            }

            return new StringContent(await source.ReadAsStringAsync());
        }

        public static async Task<object> ToStreamAsync(this SystemHttpContent source)
        {
            if (source is null)
            {
                return null;
            }

            return await source.ReadAsStreamAsync();
        }

        public static async Task<object> ToStreamContentAsync(this SystemHttpContent source)
        {
            if (source is null)
            {
                return null;
            }

            return new StreamContent(await source.ReadAsStreamAsync());
        }

        public static async Task<object> ToByteArrayAsync(this SystemHttpContent source)
        {
            if (source is null)
            {
                return null;
            }

            return await source.ReadAsByteArrayAsync();
        }

        public static async Task<object> ToByteArrayContentAsync(this SystemHttpContent source)
        {
            if (source is null)
            {
                return null;
            }

            return new ByteArrayContent(await source.ReadAsByteArrayAsync());
        }
    }
}