using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace MockNet.Http.Tests
{
    public class IsVisitorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("application/json")]
        public void TestVisitorIsAny(string parameter)
        {
            Expression<Func<MediaTypeHeaderValue, bool>> expr = x => x == Is.Any<string>();

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => Any(x)", result.ToString());

            MediaTypeHeaderValue header = null;

            if (parameter != null && parameter != "")
            {
                header = parameter;
            }

            var actual = Invoke(result, header);

            Assert.True(actual is true);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("application/json", true)]
        public void TestVisitorIsNotNull(string parameter, bool expected)
        {
            Expression<Func<MediaTypeHeaderValue, bool>> expr = x => x == Is.NotNull<string>();

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => NotNull(x)", result.ToString());

            MediaTypeHeaderValue header = null;

            if (parameter != null && parameter != "")
            {
                header = parameter;
            }

            var actual = Invoke(result, header);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestVisitorIsEqualString()
        {
            Expression<Func<MediaTypeHeaderValue, bool>> expr = x => x == Is.Equal("application/json");

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => Equal(Convert(x, String), \"application/json\")", result.ToString());

            MediaTypeHeaderValue header = "application/json";

            var actual = Invoke(result, header);

            Assert.True(actual is true);
        }

        [Fact]
        public void TestVisitorIsSameAs()
        {
            var expected = new Uri("http://localhost");

            Expression<Func<Uri, bool>> expr = x => x == Is.SameAs(expected);

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => SameAs(x, value(MockNet.Http.Tests.Test+<>c__DisplayClass20_0).expected)", result.ToString());

            var actual = Invoke(result, expected);

            Assert.True(actual is true);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("not empty", false)]
        public void TestVisitorIsEmptyString(string parameter, bool expected)
        {
            Expression<Func<string, bool>> expr = x => x == Is.Empty();

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => Empty(x)", result.ToString());

            var actual = Invoke(result, parameter);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData(0, true)]
        [InlineData(1, false)]
        public void TestVisitorIsEmptyEnumerable(int? count, bool expected)
        {
            Expression<Func<List<string>, bool>> expr = x => x == Is.Empty<string>();

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => Empty(x)", result.ToString());

            List<string> parameter = null;

            if ((count ?? -1) >= 0)
            {
                parameter = new List<string>();

                if (count > 0)
                {
                    parameter.Add("value");
                }
            }

            var actual = Invoke(result, parameter);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("not in", false)]
        [InlineData("in", true)]
        public void TestVisitorIsInEnumerableValue(string value, bool expected)
        {
            Expression<Func<List<string>, bool>> expr = x => x == Is.In("in");

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => In(x, \"in\")", result.ToString());

            List<string> parameter = null;

            if (value != null)
            {
                parameter = new List<string>();

                if (value != "")
                {
                    parameter.Add(value);
                }
            }

            var actual = Invoke(result, parameter);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("not in", false)]
        [InlineData("in", true)]
        public void TestVisitorIsInEnumerableEnumerable(string value, bool expected)
        {
            Expression<Func<List<string>, bool>> expr = x => x == Is.In("in", "out");

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => In(x, \"in\")", result.ToString());

            List<string> parameter = null;

            if (value != null)
            {
                parameter = new List<string>();

                if (value != "")
                {
                    parameter.Add(value);
                }
            }

            var actual = Invoke(result, parameter);

            Assert.Equal(expected, actual);
        }

        private object Invoke(Expression expression, params object[] parameters)
        {
            if (expression is LambdaExpression func)
            {
                return func.Compile().DynamicInvoke(parameters);
            }

            return null;
        }
    }
}