using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MockClient
{
    internal static partial class Utils
    {
        internal static class Json
        {
            public static object ToObject(string json, Type type)
            {
                return JsonConvert.DeserializeObject(json, type);
            }

            public static Dictionary<string, string> ToDictionary(string json)
            {
                if (string.IsNullOrWhiteSpace(json))
                    return null;

                return ToObject(JToken.Parse(json)) as Dictionary<string, string>;
            }

            static object ToObject(JToken token)
            {
                if (token == null) return null;

                switch (token.Type)
                {
                    case JTokenType.Object:
                        return token.Children<JProperty>()
                                    .ToDictionary(prop => prop.Name,
                                                  prop => ToObject(prop.Value));

                    case JTokenType.Array:
                        return token.Select(ToObject).ToList();

                    default:
                        return ((JValue)token).Value;
                }
            }
        }
    }
}