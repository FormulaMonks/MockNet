using System;

namespace Theorem.MockNet.Http
{
    [Flags]
    internal enum ExceptionReasonTypes
    {
        NoStup = 1,
        UnmatchedRequestUri = 2,
        UnmatchedHeaders = 4,
        UnmatchedContent = 8,
        UnmatchedResult = 16,
        MatchedMoreThanNRequests = 32,
        NoMatchingRequests = 64,
        NoResponse = 128,
    }
}