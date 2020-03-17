using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Theorem.MockNet.Http
{
    internal static partial class Utils
    {
        internal static class Comparer
        {
            public static bool ByteArray(byte[] range1, int offset1, byte[] range2, int offset2, int count)
            {
                var span1 = range1.AsSpan(offset1, count);
                var span2 = range2.AsSpan(offset2, count);

                return span1.SequenceEqual(span2);
            }

            public static async Task<bool> StreamAsync(Stream stream1, Stream stream2, CancellationToken cancel = default)
            {
                int BufferSize = 4096;
                byte[] _buffer1 = new byte[BufferSize];
                byte[] _buffer2 = new byte[BufferSize];

                if (stream1 == stream2)
                {
                    return true;
                }

                if (stream1.CanSeek && stream2.CanSeek)
                {
                    if (stream1.Length != stream2.Length)
                    {
                        return false;
                    }
                }

                long offset1 = 0;
                long offset2 = 0;

                while (true)
                {
                    var task1 = stream1.ReadAsync(_buffer1, 0, BufferSize, cancel);
                    var task2 = stream2.ReadAsync(_buffer2, 0, BufferSize, cancel);
                    var bytesRead = await Task.WhenAll(task1, task2);
                    var bytesRead1 = bytesRead[0];
                    var bytesRead2 = bytesRead[1];

                    if (bytesRead1 == 0 && bytesRead2 == 0)
                    {
                        break;
                    }

                    // Compare however much we were able to read from *both* arrays
                    int sharedCount = Math.Min(bytesRead1, bytesRead2);
                    if (!ByteArray(_buffer1, 0, _buffer2, 0, sharedCount))
                    {
                        return false;
                    }

                    if (bytesRead1 != bytesRead2)
                    {
                        // Instead of duplicating the code for reading fewer bytes from file1 than file2
                        // for fewer bytes from file2 than file1, abstract that detail away.
                        var lessCount = 0;
                        var (lessRead, moreRead, moreCount, lessStream, moreStream) =
                            bytesRead1 < bytesRead2
                                ? (_buffer1, _buffer2, bytesRead2 - sharedCount, stream1, stream2)
                                : (_buffer2, _buffer1, bytesRead1 - sharedCount, stream2, stream1);

                        while (moreCount > 0)
                        {
                            // Try reading more from `lessRead`
                            lessCount = await lessStream.ReadAsync(lessRead, 0, moreCount, cancel);

                            if (lessCount == 0)
                            {
                                // One stream was exhausted before the other
                                return false;
                            }

                            if (!ByteArray(lessRead, 0, moreRead, sharedCount, lessCount))
                            {
                                return false;
                            }

                            moreCount -= lessCount;
                            sharedCount += lessCount;
                        }
                    }

                    offset1 += sharedCount;
                    offset2 += sharedCount;
                }

                return true;
            }
        }
    }
}