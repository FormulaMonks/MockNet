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
    public class ObjectContentOfTTests
    {
        [Fact]
        public void GenericContentVerifyIntMatchesContent()
        {
            var expected = 1;

            var content = new ObjectContent<int>(expected);

            AssertContent(expected, content);
        }

        [Fact]
        public void GenericContentVerifyDateTimeMatchesContent()
        {
            var expected = DateTime.Now;

            var content = new ObjectContent<DateTime>(expected);

            AssertContent(expected, content);
        }

        [Fact]
        public void GenericContentVerifyPOCOMatchesContent()
        {
            var expected = DateTime.Now;

            var content = new ObjectContent<DateTime>(expected);

            AssertContent(expected, content);
        }

        private void AssertContent<T>(T expected, ObjectContent<T> content)
        {
            Assert.Equal(expected.ToString(), content.ToString());
            Assert.Equal(expected.GetHashCode(), content.GetHashCode());
            Assert.True(content.Equals(expected));
            Assert.True(expected == content);
            Assert.False(expected != content);
            Assert.True(content == expected);
            Assert.False(content != expected);
        }
    }
}
