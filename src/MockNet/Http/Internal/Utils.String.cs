using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Theorem.MockNet.Http
{
    internal static partial class Utils
    {
        public static class String
        {
            public static string Capitalize(string s)
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    return s;
                }

                return char.ToUpper(s[0]) + s.Substring(1).ToLower();
            }
        }
    }
}