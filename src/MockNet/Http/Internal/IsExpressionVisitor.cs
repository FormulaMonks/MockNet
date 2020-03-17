using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Theorem.MockNet.Http
{
    internal class IsExpressionVisitor : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.Right is MethodCallExpression call)
            {
                if (make.ContainsKey(call.Method.Name))
                {
                    return make[call.Method.Name](call, node);
                }
            }

            return node;
        }

        private static Dictionary<string, Func<MethodCallExpression, BinaryExpression, Expression>> make = new Dictionary<string, Func<MethodCallExpression, BinaryExpression, Expression>>()
        {
            [nameof(Is.Any)] = (c, n) => Make(c.Method, n.Left, Not(n.NodeType)),
            [nameof(Is.NotNull)] = (c, n) => Make(c.Method, n.Left, Not(n.NodeType)),
            [nameof(Is.Empty)] = (c, n) => Make(c.Method, n.Left, Not(n.NodeType)),
            [nameof(Is.SameAs)] = (c, n) => Make(c.Method, n.Left, c.Arguments, Not(n.NodeType)),
            [nameof(Is.Equal)] = (c, n) => Make(c.Method, n.Left, c.Arguments, Not(n.NodeType)),
            [nameof(Is.In)] = (c, n) => Make(c.Method, n.Left, c.Arguments, Not(n.NodeType)),
            [nameof(Is.NotIn)] = (c, n) => Make(c.Method, n.Left, c.Arguments, Not(n.NodeType)),
            [nameof(Is.Sequence)] = (c, n) => Make(c.Method, n.Left, c.Arguments, Not(n.NodeType)),
            [nameof(Is.Match)] = (c, n) => Make(c.Method, n.Left, c.Arguments, Not(n.NodeType)),
            [nameof(Is.InRange)] = (c, n) => Make(c.Method, n.Left, c.Arguments, Not(n.NodeType)),
        };

        private static Func<Expression, Expression> Not(ExpressionType nodeType)
        {
            if (nodeType == ExpressionType.NotEqual)
            {
                return x => Expression.Not(x);
            }

            return x => x;
        }

        private static Dictionary<string, Func<MethodInfo, Type[], Type[]>> generics = new Dictionary<string, Func<MethodInfo, Type[], Type[]>>()
        {
            ["*"] = (c, ts) => c.GetGenericArguments(),
            [nameof(Is.NotNull)] = (c, ts) => ts,
            [nameof(Is.Any)] = (c, ts) => ts,
        };

        private static (MethodInfo Method, ParameterInfo FirstParameter, ParameterInfo SecondParameter, ParameterInfo ThirdParameter, ParameterInfo FourthParameter)[] methods =
            typeof(IsInternal)
                .GetMethods()
                .Select(x => new
                {
                    Method = x,
                    Parameters = x.GetParameters()
                })
                .Select(x => (x.Method, x.Parameters.FirstOrDefault(), x.Parameters.SecondOrDefault(), x.Parameters.NthOrDefault(3), x.Parameters.NthOrDefault(4)))
                .ToArray();

        private static Expression Make(MethodInfo call, Expression parameter, Func<Expression, Expression> not)
        {
            if (parameter.NodeType == ExpressionType.Convert && parameter is UnaryExpression unary)
            {
                parameter = unary.Operand;
            }

            var method = GetMethod(call, new[] { parameter.Type });

            return not(Expression.Call(method, parameter));
        }

        private static Expression Make(MethodInfo call, Expression parameter, ReadOnlyCollection<Expression> expressions, Func<Expression, Expression> not)
        {
            var args = expressions.ToList();

            args.Insert(0, parameter);

            var method = GetMethod(call, new[] { parameter.Type });

            return not(Expression.Call(method, args.ToArray()));
        }

        private static MethodInfo GetMethod(MethodInfo call, Type[] typeArguments)
        {
            var callParameters = call.GetParameters();

            var ms = methods
                .Where(x => x.Method.Name == call.Name && x.Method.IsGenericMethod == call.IsGenericMethod);

            if (callParameters.Count() == 3)
            {
                ms = ms.Where(x => x.FourthParameter is ParameterInfo);
            }
            else if (callParameters.Count() == 2)
            {
                ms = ms.Where(x => x.ThirdParameter is ParameterInfo);
            }
            else if (callParameters.FirstOrDefault()?.ParameterType.IsArray ?? false)
            {
                ms = ms.Where(x => x.SecondParameter is ParameterInfo && x.SecondParameter.ParameterType.IsArray == true);
            }

            var method = ms.Select(x => x.Method).FirstOrDefault();

            // return the generic version of the method
            if (call.IsGenericMethod)
            {
                return method.MakeGenericMethod(GetGenericTypes(call, typeArguments));
            }

            return method;
        }

        private static Type[] GetGenericTypes(MethodInfo call, Type[] typeArguments)
        {
            var key = "*";

            if (generics.ContainsKey(call.Name))
            {
                key = call.Name;
            }

            return generics[key](call, typeArguments);
        }
    }
}