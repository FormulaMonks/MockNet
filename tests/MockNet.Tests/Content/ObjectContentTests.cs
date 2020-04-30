using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using SystemStringContent = System.Net.Http.StringContent;

namespace Theorem.MockNet.Http.Tests.Content
{
    public class ObjectContentTests
    {
        public static IEnumerable<object[]> GenericContentTestData()
        {
            yield return new object[] { "expected" };
            yield return new object[] { 1 };
            yield return new object[] { 1.2 };
            yield return new object[] { 1L };
            yield return new object[] { DateTime.Now };
            yield return new object[] { new Employee("John Doe", DateTime.Now, 40 ) };
        }

        [Theory]
        [MemberData(nameof(GenericContentTestData))]
        public void GenericContentVerifyObjectMatchedContent(object expected)
        {
            var content = new ObjectContent(expected.GetType(), expected);

            Assert.Equal(expected.ToString(), content.ToString());
            Assert.Equal(expected.GetHashCode(), content.GetHashCode());
            Assert.True(content.Equals(expected));
            Assert.True(expected == content);
            Assert.False(expected != content);
            Assert.True(content == expected);
            Assert.False(content != expected);
        }

        [Theory]
        [MemberData(nameof(GenericContentTestData))]
        public void GenericContentVerifyContentMatchedContent(object expected)
        {
            var content1 = new ObjectContent(expected.GetType(), expected);
            var content2 = new ObjectContent(expected.GetType(), expected);

            Assert.True(content1 == content2);
            Assert.False(content1 != content2);
            Assert.True(content1.Equals(content2));
        }

        [Theory]
        [MemberData(nameof(GenericContentTestData))]
        public void GenericContentVerifyContentMatchesAgainstDefaultSystemStringContent(object expected)
        {
            var content = new ObjectContent(expected.GetType(), expected);

            var stringContent = new SystemStringContent(JsonConvert.SerializeObject(expected), Encoding.UTF8, "application/json");

            Assert.True(content.Equals(stringContent));
        }

        [Theory]
        [MemberData(nameof(GenericContentTestData))]
        public void GenericContentsSystemHttpContentType(object expected)
        {
            var content = new ObjectContent(expected.GetType(), expected).ToHttpContent();

            Assert.Equal(typeof(SystemStringContent), content.GetType());
            Assert.Equal("application/json", content.Headers.ContentType.MediaType);
            Assert.Equal("utf-8", content.Headers.ContentType.CharSet);
        }

        [Theory]
        [MemberData(nameof(GenericContentTestData))]
        public async Task GenericContentsSystemHttpContentBodyTypeAsync(object expected)
        {
            var content = new ObjectContent(expected.GetType(), expected).ToHttpContent();

            var actual = await content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(expected), actual);
            Assert.Equal(expected, JsonConvert.DeserializeObject(actual, expected.GetType()));
        }

        [Fact]
        public void GenericContentEqualsNullObjectReturnsFalse()
        {
            var content = new ObjectContent(typeof(string), "expected");

            Assert.False(content.Equals(null));
        }

    }
}
