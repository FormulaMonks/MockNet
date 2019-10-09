using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MockNet.Http.Tests
{
    public class UtilsComparerTests
    {
        private IEnumerable<byte> GenerateByteArray(Func<int, int> fn)
        {
            var counter = 0;

            while (true)
            {
                yield return Convert.ToByte(fn(counter));
                counter++;
            }
        }

        [Fact]
        public void TestComparingTwoMatchingByteArrays()
        {
            var bytes = GenerateByteArray(x => x).Take(100).ToArray();

            var actual = Utils.Comparer.ByteArray(bytes, 0, bytes, 0, bytes.Length);

            Assert.True(actual);
        }

        [Fact]
        public void TestComparingTwoNonMatchingByteArrays()
        {
            var bytes1 = GenerateByteArray(x => x).Take(100).ToArray();
            var bytes2 = GenerateByteArray(x => x * 2).Take(100).ToArray();

            var actual = Utils.Comparer.ByteArray(bytes1, 0, bytes2, 0, bytes1.Length);

            Assert.False(actual);
        }

        [Fact]
        public async Task TestCompareStreamSameStreamsAsync()
        {
            var buffer = GenerateByteArray(x => x).Take(100).ToArray();
            var stream = new MemoryStream(buffer);

            var actual = await Utils.Comparer.StreamAsync(stream, stream);

            Assert.True(actual);
        }

        [Fact]
        public async Task TestCompareStreamEqualStreamsAsync()
        {
            var buffer1 = GenerateByteArray(x => x % 2).Take(10000).ToArray();
            var buffer2 = GenerateByteArray(x => x % 2).Take(10000).ToArray();
            var stream1 = new MemoryStream(buffer1);
            var stream2 = new MemoryStream(buffer2);

            var actual = await Utils.Comparer.StreamAsync(stream1, stream2);

            Assert.True(actual);
        }
    }
}