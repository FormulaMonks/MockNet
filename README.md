Is.Any() ->         Moq.It.IsAny<>()            A<>.IsNull()
Is.Match(x => ) ->  Moq.It.Is<>(x => )
Is.Equal() ->                                   A<>.IsEqualTo()
Is.Equal([]) ->                                 A<>.IsSameSequenceAs()
Is.SameAs() ->                                  A<>.IsSameAs()
Is.Empty() ->                                   A<>.IsEmpty()
Is.NotNull() ->     Moq.It.IsNotNull<>()        A<>.IsNotNull()
Is.In() ->          Moq.It.IsIn<T>()
Is.NotIn() ->       Moq.It.IsNotIn<T>()
Is.Contains([]) ->                              A<>.Contains()
Is.InRange() ->     Moq.It.IsInRange<T>()
Is.Regex() ->       Moq.It.IsRegex()


string
StringContent
----
ReadAsStringAsync()


byte[]
ByteArrayContent
---
ReadAsByteArrayAsync()

convert: x == bytes or x == That.Is(bytes) into MemoryCompare.Compare(x, bytes)

StreamContent
Stream
---
ReadAsStreamAsync()

convert: x == stream or x == That.Is(bytes) into StreamCompare.Compare(x, stream);



FormUrlEncodedContent
IDictionary<string, string>
IEnumerable<KeyValuePair<string, string>>
---
Utils.Json.ToDictionary<string, string>()

All others:
---
Utils.Json.ToObject<T>()




Left to do:

[] `medium` Expression Visitor to enable things like Is.XXX
[] `high` Exception messages
[] `high` Response Headers
[] `low` add better support to match on request uri
    - / == http[s]://.../
    - /test == http[s]://.../test
    - http[s]://.../path != http[-s]://.../path
[] `low` handle MultipartContent & MultipartFormDataContent
