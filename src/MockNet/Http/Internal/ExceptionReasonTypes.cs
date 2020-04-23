using System;

namespace Theorem.MockNet.Http
{
    [Flags]
    internal enum ExceptionReasonTypes
    {
        NoSetup = 1,
        UnmatchedHttpMethod = 2,
        UnmatchedRequestUri = 4,
        UnmatchedHeaders = 8,
        UnmatchedContent = 16,
        UnmatchedResult = 32,
        MatchedMoreThanNRequests = 64,
        NoMatchingRequests = 128,
        NoResponse = 256,
    }
}
