using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Xunit;

namespace Theorem.MockNet.Http.Tests
{
    public class IsExpressionVisitorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("application/json")]
        public void TestIsExpressionVisitorForAny(string parameter)
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
        public void TestIsExpressionVisitorForNotNull(string parameter, bool expected)
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
        public void TestIsExpressionVisitorForEqual()
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
        public void TestIsExpressionVisitorForSameAs()
        {
            var expected = new Uri("http://localhost");

            Expression<Func<Uri, bool>> expr = x => x == Is.SameAs(expected);

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => SameAs(x, value(Theorem.MockNet.Http.Tests.IsExpressionVisitorTests+<>c__DisplayClass3_0).expected)", result.ToString());

            var actual = Invoke(result, expected);

            Assert.True(actual is true);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("not empty", false)]
        public void TestIsExpressionVisitorForEmptyString(string parameter, bool expected)
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
        public void TestIsExpressionVisitorForEmptyEnumerable(int? count, bool expected)
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
        public void TestIsExpressionVisitorForInValue(string value, bool expected)
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
        [InlineData("in", false)]
        [InlineData("in,out", true)]
        public void TestIsExpressionVisitorForInArray(string value, bool expected)
        {
            Expression<Func<List<string>, bool>> expr = x => x == Is.In("in", "out");

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => In(x, new [] {\"in\", \"out\"})", result.ToString());

            List<string> parameter = null;

            if (value != null)
            {
                parameter = new List<string>();

                if (value != "")
                {
                    parameter.AddRange(value.Split(new[] { ',' }));
                }
            }

            var actual = Invoke(result, parameter);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", true)]
        [InlineData("not in", true)]
        [InlineData("in", false)]
        public void TestIsExpressionVisitorForNotInValue(string value, bool expected)
        {
            Expression<Func<List<string>, bool>> expr = x => x == Is.NotIn("in");

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => NotIn(x, \"in\")", result.ToString());

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
        [InlineData("", true)]
        [InlineData("not in", true)]
        [InlineData("in", true)]
        [InlineData("in,out", false)]
        public void TestIsExpressionVisitorForNotInArray(string value, bool expected)
        {
            Expression<Func<List<string>, bool>> expr = x => x == Is.NotIn("in", "out");

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => NotIn(x, new [] {\"in\", \"out\"})", result.ToString());

            List<string> parameter = null;

            if (value != null)
            {
                parameter = new List<string>();

                if (value != "")
                {
                    parameter.AddRange(value.Split(new[] { ',' }));
                }
            }

            var actual = Invoke(result, parameter);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("not in", false)]
        [InlineData("in", false)]
        [InlineData("out,in", false)]
        [InlineData("in,out", true)]
        public void TestIsExpressionVisitorForSequence(string value, bool expected)
        {
            Expression<Func<List<string>, bool>> expr = x => x == Is.Sequence("in", "out");

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => Sequence(x, new [] {\"in\", \"out\"})", result.ToString());

            List<string> parameter = null;

            if (value != null)
            {
                parameter = new List<string>();

                if (value != "")
                {
                    parameter.AddRange(value.Split(new[] { ',' }));
                }
            }

            var actual = Invoke(result, parameter);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("in", false)]
        [InlineData("123", true)]
        public void TestIsExpressionVisitorForMatch(string value, bool expected)
        {
            Expression<Func<string, bool>> expr = x => x == Is.Match(@"\d+");

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => Match(x, \"\\d+\")", result.ToString());

            var actual = Invoke(result, value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("123", false)]
        [InlineData("in", true)]
        [InlineData("IN", true)]
        public void TestIsExpressionVisitorForMatchWithOptions(string value, bool expected)
        {
            Expression<Func<string, bool>> expr = x => x == Is.Match("[a-z]+", RegexOptions.IgnoreCase);

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => Match(x, \"[a-z]+\", IgnoreCase)", result.ToString());

            var actual = Invoke(result, value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData(-1, false)]
        [InlineData(11, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(10, true)]
        public void TestIsExpressionVisitorForInRange(int? value, bool expected)
        {
            Expression<Func<int, bool>> expr = x => x == Is.InRange(1, 10);

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => InRange(x, 1, 10)", result.ToString());

            var actual = Invoke(result, value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData(-1, false)]
        [InlineData(11, false)]
        [InlineData(1, false)]
        [InlineData(2, true)]
        [InlineData(10, false)]
        public void TestIsExpressionVisitorForInRangeWithRangeType(int? value, bool expected)
        {
            Expression<Func<int, bool>> expr = x => x == Is.InRange(1, 10, RangeType.Exclusive);

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => InRange(x, 1, 10, Exclusive)", result.ToString());

            var actual = Invoke(result, value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("application/json")]
        public void TestIsExpressionVisitorForNotEqualToAny(string parameter)
        {
            Expression<Func<MediaTypeHeaderValue, bool>> expr = x => x != Is.Any<string>();

            var visitor = new IsExpressionVisitor();
            var result = visitor.Visit(expr);

            Assert.Equal("x => Not(Any(x))", result.ToString());

            MediaTypeHeaderValue header = null;

            if (parameter != null && parameter != "")
            {
                header = parameter;
            }

            var actual = Invoke(result, header);

            Assert.True(actual is false);
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